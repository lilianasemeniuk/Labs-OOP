using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Lab2
{
    public class LineShape : Shape
    {
        public LineShape(double x1, double y1, double x2, double y2) : base(x1, y1, x2, y2)
        {
        }


        public override void Draw(Canvas canvas)
        {
            System.Windows.Shapes.Line myLine = new System.Windows.Shapes.Line();
            myLine.X1 = x1;
            myLine.Y1 = y1;
            myLine.X2 = x2;
            myLine.Y2 = y2;

            myLine.Stroke = System.Windows.Media.Brushes.Red;
            myLine.StrokeThickness = 2;

            canvas.Children.Add(myLine);
        }
    }
}
