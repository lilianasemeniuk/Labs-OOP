using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Lab2
{
    public abstract class Shape
    {
        protected double x1;
        protected double y1;
        protected double x2;
        protected double y2;

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

        public abstract void Draw(System.Windows.Controls.Canvas canvas);
    }

   
}
