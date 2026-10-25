using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDrawing = false;
        private System.Windows.Point startPoint;
        private Shape currentShape;
        private System.Collections.Generic.List<Shape> shapesList = new System.Collections.Generic.List<Shape>();
        private string currentMode = "Line";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Point";
            currentMode = "Point";
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Title = "Line";
            currentMode = "Line";
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.Title = "Rectangle";
            currentMode = "Rectangle";
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            this.Title = "Ellipse";
            currentMode = "Ellipse";
        }

        private void MyCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
            startPoint = e.GetPosition(MyCanvas);

            if (currentMode == "Line")
            {
                currentShape = new LineShape(startPoint.X, startPoint.Y, startPoint.X, startPoint.Y);
            }
            else if (currentMode == "Rectangle")
            {
                currentShape = new RectangleShape(startPoint.X, startPoint.Y, startPoint.X, startPoint.Y);
            }
            else if (currentMode == "Ellipse")
            {
                currentShape = new EllipseShape(startPoint.X, startPoint.Y, startPoint.X, startPoint.Y);
            }
            else
            {
                currentShape = new PointShape(startPoint.X, startPoint.Y, startPoint.X, startPoint.Y);
            }

            currentShape.Draw(MyCanvas);

            MyCanvas.CaptureMouse();
        }

        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing == false) return;
            System.Windows.Point currentPoint = e.GetPosition(MyCanvas);
            currentShape.Update(currentPoint.X, currentPoint.Y);
            MyCanvas.Children.Clear();

            foreach (Shape savedShape in shapesList)
            {
                savedShape.Draw(MyCanvas);
            }

            currentShape.Draw(MyCanvas);
        }

        private void MyCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isDrawing && currentShape != null)
            {
                isDrawing = false;

                currentShape.x2 = e.GetPosition(MyCanvas).X;
                currentShape.y2 = e.GetPosition(MyCanvas).Y;

                currentShape.StrokeColor = Brushes.Black;
                MyCanvas.Children.Clear();

                foreach (var shape in shapesList)
                {
                    shape.Draw(MyCanvas);
                }

                currentShape.Draw(MyCanvas);
                shapesList.Add(currentShape);

                MyCanvas.ReleaseMouseCapture();
            }


        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            MyCanvas.Children.Clear();
            shapesList.Clear();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            string aboutText = ("Лабораторна робота #2 (Варіант 21)\n" +
                "Виконала: Семенюк Ліліана Валентинівна\n " +
                "Інтерфейс та функціонал: \n" +
                "Програма дозволяє користувачу малювати на полотні різні " +
                "фігури (точки, лінії, прямокутники та еліпси) за допомогою миші." +
                " Користувач може вибирати тип фігури з меню та малювати її, натискаючи та" +
                " перетягуючи мишу на полотні. Програма також дозволяє" +
                " очищати полотно та виходити з програми.");

            System.Windows.MessageBox.Show(aboutText, "Про програму");


        }
    }
}