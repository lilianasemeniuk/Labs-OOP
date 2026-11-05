using System;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Lab4
{
    public class LineWithCirclesShape : Shape
    {

    
        public LineWithCirclesShape(double x1, double y1, double x2, double y2) : base(x1, y1, x2, y2)
        {
        }

        public override void Draw(Canvas canvas)
        {
            Line mainLine = new Line();
            mainLine.X1 = x1;
            mainLine.Y1 = y1;
            mainLine.X2 = x2;
            mainLine.Y2 = y2;
            mainLine.Stroke = StrokeColor;
            mainLine.StrokeThickness = 2;

            if (this.IsDashed)
            {
                mainLine.StrokeDashArray = new DoubleCollection { 4, 4 };
            }
            canvas.Children.Add(mainLine);

            Ellipse circle1 = new Ellipse();
            circle1.Width = 10;
            circle1.Height = 10;
            circle1.Stroke = StrokeColor;
            circle1.StrokeThickness = 2;
            circle1.Fill = Brushes.White;
            Canvas.SetLeft(circle1, x1 - 5);
            Canvas.SetTop(circle1, y1 - 5);
            canvas.Children.Add(circle1);

            Ellipse circle2 = new Ellipse();
            circle2.Width = 10;
            circle2.Height = 10;
            circle2.Stroke = StrokeColor;
            circle2.StrokeThickness = 2;
            circle2.Fill = Brushes.White;
            Canvas.SetLeft(circle2, x2 - 5);
            Canvas.SetTop(circle2, y2 - 5);
            canvas.Children.Add(circle2);
        }
    }
}
