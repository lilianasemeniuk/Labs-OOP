using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Linq.Expressions;

namespace Lab5
{
    public partial class MainWindow : Window
    {
        private Shape[] shapesArray = new Shape[122];
        private int shapeCount = 0;
        private string currentMode = "Line";

        private MyEditor editor;
        private MyTableWindow tableWindow;
        private int selectedShapeIndex = -1;

        public MainWindow()
        {
            InitializeComponent();
            editor = MyEditor.GetInstance();
            editor.SetCanvas(MyCanvas);

            editor.OnShapeCreated += (name, x1, y1, x2, y2) =>
            {
                tableWindow?.AddRow(GetDisplayName(name), x1, y1, x2, y2);
            };
        }

        private string GetDisplayName(string className)
        {
            return className switch
            {
                "PointShape" => "Point",
                "LineShape" => "Line",
                "RectangleShape" => "Rectangle",
                "EllipseShape" => "Ellipse",
                "LineWithCirclesShape" => "Line with Circles",
                "CubeShape" => "Cube",
                _ => className
            };

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.Tag != null)
            {
                currentMode = element.Tag.ToString();
            }
        }

        private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(MyCanvas);
            editor.OnMouseDown(p, currentMode);
        }

        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(MyCanvas);
            editor.OnMouseMove(p, shapesArray, shapeCount);
        }

        private void MyCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            editor.OnMouseUp(shapesArray, ref shapeCount);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            MyCanvas.Children.Clear();
            shapeCount = 0;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ObjectMenu_SubmenuOpened(object sender, RoutedEventArgs e)
        {

        }

        private void ToggleTableMenu_Click(object sender, RoutedEventArgs e)
        {
            if (tableWindow != null && tableWindow.IsVisible)
            {
                tableWindow.Close();
                tableWindow = null;
                selectedShapeIndex = -1;
                RedrawCanvas();
                return;
            }

            tableWindow = new MyTableWindow();
            tableWindow.Owner = this;

            for (int i = 0; i < shapeCount; i++)
            {
                var s = shapesArray[i];
                tableWindow.AddRow(GetDisplayName(s.GetType().Name), s.x1, s.y1, s.x2, s.y2);
            }

            tableWindow.OnRowSelected += (index) =>
            {
                selectedShapeIndex = index;
                RedrawCanvas();
            };

            tableWindow.OnRowDeleted += (index) =>
            {
                if (index >= 0 && index < shapeCount)
                {
                    for (int i = index; i < shapeCount - 1; i++)
                    {
                        shapesArray[i] = shapesArray[i + 1];
                    }
                    shapesArray[shapeCount - 1] = null;
                    shapeCount--;
                    selectedShapeIndex = -1;
                    RedrawCanvas();
                }
            };

            tableWindow.Closed += (s, args) =>
            {
                tableWindow = null;
                selectedShapeIndex = -1;
                RedrawCanvas();
            };

            tableWindow.Show();

        }

        private void RedrawCanvas()
        {
            MyCanvas.Children.Clear();
            for (int i = 0; i < shapeCount; i++)
            {
                var originalBrush = shapesArray[i].StrokeColor;
                if (i == selectedShapeIndex)
                {
                    shapesArray[i].StrokeColor = System.Windows.Media.Brushes.Red;
                }
                shapesArray[i].Draw(MyCanvas);
                shapesArray[i].StrokeColor = originalBrush;
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tableWindow?.Close();
        }

        private void SaveCsvMenu_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                DefaultExt = "csv",
                FileName = "shapes.csv"
            };

            if (saveDialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveDialog.FileName))
                {
                    writer.WriteLine("Name;x1;y1;x2;y2");
                    for (int i = 0; i < shapeCount; i++)
                    {
                        var s = shapesArray[i];
                        string name = GetDisplayName(s.GetType().Name);
                        writer.WriteLine($"{name};{(int)Math.Round(s.x1)};{(int)Math.Round(s.y1)};{(int)Math.Round(s.x2)};{(int)Math.Round(s.y2)}");
                    }
                }

                MessageBox.Show("Objects successfully saved to CSV!", "Save CSV", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void LoadCsvMenu_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
            };

            if (openDialog.ShowDialog() == true)
            {
                try
                {
                    string[] lines = File.ReadAllLines(openDialog.FileName);

                    for (int i = 0; i < shapeCount; i++)
                    {
                        shapesArray[i] = null;
                    }
                    shapeCount = 0;
                    selectedShapeIndex = -1;
                    tableWindow?.ClearTable();

                    for (int i = 1; i < lines.Length; i++)
                    {
                        if (string.IsNullOrWhiteSpace(lines[i])) continue;

                        string[] parts = lines[i].Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length < 5) continue;

                        string name = parts[0].Trim();
                        double x1 = double.Parse(parts[1].Trim());
                        double y1 = double.Parse(parts[2].Trim());
                        double x2 = double.Parse(parts[3].Trim());
                        double y2 = double.Parse(parts[4].Trim());

                        Shape newShape = CreateShapeByName(name, x1, y1, x2, y2);
                        if (newShape != null && shapeCount < shapesArray.Length)
                        {
                            shapesArray[shapeCount++] = newShape;
                            tableWindow?.AddRow(name, x1, y1, x2, y2);
                        }
                    }

                    RedrawCanvas();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading CSV file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private Shape CreateShapeByName(string name, double x1, double y1, double x2, double y2)
        {
            return name switch
            {
                "Point" or "PointShape" => new PointShape(x1, y1, x2, y2),
                "Line" or "LineShape" => new LineShape(x1, y1, x2, y2),
                "Rectangle" or "RectangleShape" => new RectangleShape(x1, y1, x2, y2),
                "Ellipse" or "EllipseShape" => new EllipseShape(x1, y1, x2, y2),
                "Line with circles" or "LineWithCirclesShape" => new LineWithCirclesShape(x1, y1, x2, y2),
                "Cube" or "CubeShape" => new CubeShape(x1, y1, x2, y2),
                _ => null
            };
        }
    }
}
    