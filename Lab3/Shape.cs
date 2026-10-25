using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab2
{
    public abstract class Shape
    {
        public double x1;
        public double y1;
        public double x2;
        public double y2;

        public Shape(double x1, double y1, double x2, double y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        public void Update(double newX, double newY)
        {
            this.x2 = newX;
            this.y2 = newY;
        }

        public Brush StrokeColor
        {
            get;
            set;
        } = Brushes.Blue;
        public abstract void Draw(System.Windows.Controls.Canvas canvas);
    }
}
