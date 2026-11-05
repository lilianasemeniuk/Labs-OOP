using System.Windows;
using System.Windows.Controls;

namespace Lab4
{
    public class MyEditor
    {
        private Shape currentShape;
        private Point startPoint;
        private bool isDrawing = false;
        private Canvas canvas;

        public MyEditor(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void OnMouseDown(Point point, string currentMode)
        {
            isDrawing = true;
            startPoint = point;

            switch (currentMode)
            {
                case "Point": 
                    currentShape = new PointShape(0, 0, 0, 0); 
                    break;
                case "Line": 
                    currentShape = new LineShape(0, 0, 0, 0); 
                    break;
                case "Rectangle": 
                    currentShape = new RectangleShape(0, 0, 0, 0); 
                    break;
                case "Ellipse": 
                    currentShape = new EllipseShape(0, 0, 0, 0); 
                    break;
                case "LineWithCircles":
                    currentShape = new LineWithCirclesShape(0, 0, 0, 0);
                    break;
                case "Cube":
                    currentShape = new CubeShape(0, 0, 0, 0);
                    break;
                default: 
                    currentShape = new PointShape(0, 0, 0, 0); 
                    break;
            }

            currentShape.Update(startPoint.X, startPoint.Y, startPoint.X, startPoint.Y);
            currentShape.IsDashed = true;
            currentShape.StrokeColor = System.Windows.Media.Brushes.Blue;
            currentShape.Draw(canvas);
            canvas.CaptureMouse();
        }

        public void OnMouseMove(Point currentPoint, Shape[] shapesArray, int shapeCount)
        {
            if (!isDrawing || currentShape == null) return;

            currentShape.Update(startPoint.X, startPoint.Y, currentPoint.X, currentPoint.Y);

            canvas.Children.Clear();
            for (int i = 0; i < shapeCount; i++)
            {
                shapesArray[i].Draw(canvas);
            }
            currentShape.Draw(canvas);

        }

        public void OnMouseUp(Shape[] shapesArray, ref int shapeCount)
        {
            if (!isDrawing || currentShape == null) return;

            isDrawing = false;
            canvas.ReleaseMouseCapture();

            currentShape.IsDashed = false;
            currentShape.StrokeColor = System.Windows.Media.Brushes.Black;

            canvas.Children.Clear();
            for(int i = 0; i < shapeCount; i++)
            {
                shapesArray[i].Draw(canvas);
            }
            currentShape.Draw(canvas);

            if (shapeCount < shapesArray.Length)
            {
                shapesArray[shapeCount] = currentShape;
                shapeCount++;
            }

            currentShape = null;
        }
    }
}
