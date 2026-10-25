using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab2
{
    public class PointShape : Shape
    {
        public PointShape(double x1, double y1, double x2, double y2) : base(x1, y1, x2, y2)
        {
        }

        public override void Draw(Canvas canvas)
        {
            System.Windows.Shapes.Ellipse myPoint = new System.Windows.Shapes.Ellipse();
            myPoint.Fill = StrokeColor;
            myPoint.Width = 5;
            myPoint.Height = 5;

            Canvas.SetLeft(myPoint, x1 - 2);
            Canvas.SetTop(myPoint, y1 - 2);

            canvas.Children.Add(myPoint);
        }
    }
}


