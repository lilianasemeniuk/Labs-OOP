using System.Windows;
using System.Windows.Input;

namespace Lab4
{
    public partial class MainWindow : Window
    {
        private Shape[] shapesArray = new Shape[122];
        private int shapeCount = 0;
        private string currentMode = "Line";

        private MyEditor editor;

        public MainWindow()
        {
            InitializeComponent();
            editor = new MyEditor(MyCanvas);
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
    }
}