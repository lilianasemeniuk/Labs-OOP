using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab4
{
    public abstract class Shape
    {
        public double x1;
        public double y1;
        public double x2;
        public double y2;

        public bool IsDashed = false;

        public Shape(double x1, double y1, double x2, double y2)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        public virtual void Update(double newX1, double newY1, double newX2, double newY2)
        {
            this.x1 = newX1;
            this.y1 = newY1;
            this.x2 = newX2;
            this.y2 = newY2;
        }

        public Brush StrokeColor
        {
            get;
            set;
        } = Brushes.Black;
        public abstract void Draw(System.Windows.Controls.Canvas canvas);
    }

}