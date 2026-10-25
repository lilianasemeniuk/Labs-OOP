using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab2
{
    public class EllipseShape : Shape
    {
        public EllipseShape(double x1, double y1, double x2, double y2) : base(x1, y1, x2, y2)
        {

        }

        public override void Draw(Canvas canvas)
        {
            System.Windows.Shapes.Ellipse myEllipse = new System.Windows.Shapes.Ellipse();

            myEllipse.Stroke = StrokeColor;
            myEllipse.StrokeThickness = 2;
            myEllipse.Fill = Brushes.Transparent;

            myEllipse.Width = Math.Abs(x2 - x1);
            myEllipse.Height = Math.Abs(y2 - y1);

            Canvas.SetLeft(myEllipse, Math.Min(x1, x2));
            Canvas.SetTop(myEllipse, Math.Min(y1, y2));

            canvas.Children.Add(myEllipse);

        }
    }
}