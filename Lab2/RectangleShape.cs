using System;
using System.Security.Cryptography.Xml;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab2
{
    public class RectangleShape : Shape
    {
        public RectangleShape(double x1, double y1, double x2, double y2) : base(x1, y1, x2, y2)
        {

        }

        public override void Draw(Canvas canvas)
        {
            System.Windows.Shapes.Rectangle myRect = new System.Windows.Shapes.Rectangle();

            myRect.Stroke = Brushes.Red;
            myRect.StrokeThickness = 2;

            myRect.Fill = Brushes.Transparent;

            myRect.Width = Math.Abs(x2 - x1);
            myRect.Height = Math.Abs(y2 - y1);

            Canvas.SetLeft(myRect, Math.Min(x1, x2));
            Canvas.SetTop(myRect, Math.Min(y1, y2));

            canvas.Children.Add(myRect);
        }
    }
}
