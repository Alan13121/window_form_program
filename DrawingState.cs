using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw2
{
    internal class DrawingState : IState
    {
        PointF ul_point, lr_point;
        bool isPressed;
        Shape hintShape;
        GeneralState pointState;
        int type = -1;
        //int ID = 1;
        
        public DrawingState(GeneralState pointState)
        {
            this.pointState = pointState;
            //Console.WriteLine("DrawingState Generated!");
        }
        public void Initialize(Model m)
        {
            isPressed = false;
            hintShape = null;
            //Console.WriteLine("DrawingState Initialized!");
        }
        public void OnPaint(Model m, IDrawable g)
        {
            foreach (Shape aShape in m.GetShapes())
            {
                aShape.DrawShape(g);
            }
            if (isPressed)
            {
                hintShape.Normalize();
                hintShape.DrawShape(g);
            }
        }
        public void MouseDown(Model m, PointF point)
        {
            //Console.WriteLine("DS MouseDown");
            isPressed = true;
            ul_point = lr_point = point;
            hintShape.X = point.X;
            hintShape.Y = point.Y;
            hintShape.Width = 0;
            hintShape.Height = 0;
            hintShape.Text = "";
        }
        public void MouseMove(Model m, PointF point)
        {
            if (isPressed && type != -1)
            {
                hintShape.Width = Math.Abs(point.X - ul_point.X);
                hintShape.Height = Math.Abs(point.Y - ul_point.Y);
                hintShape.X = Math.Min(point.X, ul_point.X);
                hintShape.Y = Math.Min(point.Y, ul_point.Y);
            }
        }
        public void MouseUp(Model m, PointF point)
        {
            if (isPressed && type != -1)
            {
                isPressed = false;
                hintShape.Normalize();
                hintShape.Text = RandomStringBuilder.GenerateRandomString();
                if (hintShape.Width > 0 && hintShape.Height > 0)
                    m.enter_new_shape(hintShape);
                m.ChangeToGeneralState();
                type = -1;
            }
        }
        public void KeyDown(Model m, int keyValue)
        {
        }
        public void KeyUp(Model m, int keyValue)
        {
        }
        public void SetShapeType(Model m, int shapeType, int ID)
        {
            hintShape = new Shape();
            type = shapeType;
            switch (type)
            {
                case 0:
                    hintShape.ShapeName = "Start";
                    break;
                case 1:
                    hintShape.ShapeName = "Terminator";
                    break;
                case 2:
                    hintShape.ShapeName = "Process";
                    break;
                case 3:
                    hintShape.ShapeName = "Decision";
                    break;
                default:
                    break;
            }
            hintShape.ID = ID;
        }
        public void DeleteShape(Model m, int ID)
        {
            m.remove_shape(ID);
        }
    }
}
