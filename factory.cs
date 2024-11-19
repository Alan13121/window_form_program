using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace hw2
{
    public class ShapeFactory
    {
        public Shape CreateShape(string shapeType, int ID, string text, int X, int Y, int Height, int Width)
        {
            Shape shape;
            switch (shapeType)
            {
                case "Start":
                    shape = new Start();
                    break;
                case "Terminator":
                    shape = new Terminator();
                    break;
                case "Process":
                    shape = new Process();
                    break;
                case "Decision":
                    shape = new Decision();
                    break;
                default:
                    throw new ArgumentException("Invalid shape type");
            }

            shape.ID = ID;
            shape.text = text;
            shape.X = X;
            shape.Y = Y;
            shape.Height = Height;
            shape.Width = Width;
            shape.X1 = X;
            shape.Y1 = Y;
            shape.X2 = X + Width;
            shape.Y2 = Y + Height;

            return shape;
        }

        public void DrawShape(IDrawable shape, float X1, float Y1, float X2, float Y2, string text, string ShapeName)
        {
            

            if (ShapeName == "Start")
                shape.DrawEllipse(X1, Y1, X2, Y2, text);
            else if (ShapeName == "Terminator")
                shape.DrawOval(X1, Y1, X2, Y2, text);
            else if (ShapeName == "Process")
                shape.DrawRectangle(X1, Y1, X2, Y2, text);
            else if (ShapeName == "Decision")
                shape.DrawPolygon(X1, Y1, X2, Y2, text);
        }
    }
}
