using System;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Lab4
{
    public class CubeShape : Shape
    {
        public CubeShape(double x1, double y1, double x2, double y2) : base(x1, y1, x2, y2)
        {
        }

        public override void Draw(Canvas canvas)
        {
            double minX = Math.Min(x1, x2);
            double minY = Math.Min(y1, y2);
            double width = Math.Abs(x2 - x1);
            double height = Math.Abs(y2 - y1);

            double offset = Math.Min(width, height) * 0.3;

            DoubleCollection dashArray = this.IsDashed ? new DoubleCollection { 4, 4 } : null;

            Rectangle backRect = new Rectangle();
            backRect.Width = width;
            backRect.Height = height;
            Canvas.SetLeft(backRect, minX + offset);
            Canvas.SetTop(backRect, minY - offset);
            backRect.Stroke = StrokeColor;
            backRect.StrokeThickness = 2;
            backRect.StrokeDashArray = dashArray;
            canvas.Children.Add(backRect);

            DrawCubeLine(canvas, minX, minY, minX + offset, minY - offset, dashArray);                                 
            DrawCubeLine(canvas, minX + width, minY, minX + width + offset, minY - offset, dashArray);                 
            DrawCubeLine(canvas, minX, minY + height, minX + offset, minY + height - offset, dashArray);               
            DrawCubeLine(canvas, minX + width, minY + height, minX + width + offset, minY + height - offset, dashArray); 

            Rectangle frontRect = new Rectangle();
            frontRect.Width = width;
            frontRect.Height = height;
            Canvas.SetLeft(frontRect, minX);
            Canvas.SetTop(frontRect, minY);
            frontRect.Stroke = StrokeColor;
            frontRect.StrokeThickness = 2;
            frontRect.Fill = Brushes.Transparent;
            frontRect.StrokeDashArray = dashArray;
            canvas.Children.Add(frontRect);
        }

        private void DrawCubeLine(Canvas canvas, double x1, double y1, double x2, double y2, DoubleCollection dash)
        {
            Line line = new Line();
            line.X1 = x1;
            line.Y1 = y1;
            line.X2 = x2;
            line.Y2 = y2;
            line.Stroke = StrokeColor;
            line.StrokeThickness = 2;
            line.StrokeDashArray = dash;
            canvas.Children.Add(line);
        }
    }
}
