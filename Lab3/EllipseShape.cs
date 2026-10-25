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
            myEllipse.Fill = Brushes.White;

            double rx = Math.Abs(x2 - x1);
            double ry = Math.Abs(y2 - y1);

            myEllipse.Width = rx * 2;
            myEllipse.Height = ry * 2;

            Canvas.SetLeft(myEllipse, x1 - rx);
            Canvas.SetTop(myEllipse, y1 - ry);

            canvas.Children.Add(myEllipse);

        }
    }
}