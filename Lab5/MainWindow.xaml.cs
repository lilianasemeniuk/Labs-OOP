using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

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
                "lineShape" => "Line",
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

            //Бонус 1. Виділення рядка в таблиці при виборі фігури на полотні
            tableWindow.OnRowSelected += (index) =>
            {
                selectedShapeIndex = index;
                RedrawCanvas();
            };

            //Бонус 2. Видалення рядка з таблиці 
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
    }
}