using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw2
{
    internal class GeneralState : IState
    {
        List<Shape> selectedShapes = new List<Shape>();
        const int CTRL_KEY = 17;
        bool isCtrlKeyDown;
        bool isMouseDown;
        PointF mouseDownPosition = new PointF();
        public void Initialize(Model m)
        {
            selectedShapes.Clear();
            isCtrlKeyDown = false;
        }
        public void MouseDown(Model m, PointF point)
        {
            mouseDownPosition = point;
            isMouseDown = true;
            //Console.WriteLine("PS MouseDown");
            foreach (Shape shape in Enumerable.Reverse(m.GetShapes()))
            {
                //Console.WriteLine(shape.ShapeName);
                if (shape.IsPointInShape(point))
                {
                    //Console.WriteLine("isCtrlDown:" + isCtrlKeyDown);
                    if (isCtrlKeyDown)
                    {
                        selectedShapes.Add(shape);
                    }
                    else
                    {
                        selectedShapes.Clear();
                        selectedShapes.Add(shape);
                        //Console.WriteLine("select:" + shape.ShapeName);
                    }
                    return;
                }
            }
            if (!isCtrlKeyDown)
            {
                selectedShapes.Clear();
            }
        }
        public void AddSelectedShape(Shape shape)
        {
            if (!selectedShapes.Contains(shape))
                selectedShapes.Add(shape);
        }

        public void MouseMove(Model m, PointF point)
        {
            if (mouseDownPosition.X > 0 && mouseDownPosition.Y > 0 && isMouseDown)
            {
                //Console.WriteLine("selectedShapeList num:" + selectedShapes.Count());
                foreach (Shape shape in selectedShapes)
                {
                    shape.X += point.X - mouseDownPosition.X;
                    shape.Y += point.Y - mouseDownPosition.Y;
                }
                mouseDownPosition = point;
            }
        }

        public void MouseUp(Model m, PointF point)
        {
            if (isMouseDown)
            {
                mouseDownPosition.X = -1;
                mouseDownPosition.Y = -1;
                isMouseDown = false;
            }
        }

        public void OnPaint(Model m, IDrawable g)
        {
            foreach (Shape shape in m.GetShapes())
                shape.DrawShape(g);
            foreach (Shape shape in selectedShapes)
                shape.DrawBoundingBox(g);
        }

        public void KeyDown(Model m, int keyValue)
        {
            //Console.WriteLine("PS KeyDown");
            if (keyValue == CTRL_KEY)
                isCtrlKeyDown = true;
        }

        public void KeyUp(Model m, int keyValue)
        {
            //Console.WriteLine("PS KeyUp");
            if (keyValue == CTRL_KEY)
                isCtrlKeyDown = false;
        }
        public void SetShapeType(Model m, int shapeType, int ID)
        {
            //do nothing
        }
        public void DeleteShape(Model m, int ID)
        {
            Shape shapeToRemove = m.GetShapes().FirstOrDefault(x => x.ID == ID);
            //Console.WriteLine("ShapeInfo:" + shapeToRemove.ShapeName);
            m.remove_shape(ID);
            selectedShapes.Remove(shapeToRemove);
        }
    }
}
