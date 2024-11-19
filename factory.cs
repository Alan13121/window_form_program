using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

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
            shape.Text = text;
            shape.X = X;
            shape.Y = Y;
            shape.Width = Width;
            shape.Height = Height;
            return shape;
        }

        public void DrawShape(IDrawable shape, float X, float Y, float Width , float Height, string text, string ShapeName)
        {
            if (ShapeName == "Start")
                shape.DrawEllipse(X, Y, Width, Height, text);
            else if (ShapeName == "Terminator")
                shape.DrawOval(X, Y, Width, Height, text);
            else if (ShapeName == "Process")
                shape.DrawRectangle(X, Y, Width, Height, text);
            else if (ShapeName == "Decision")
                shape.DrawPolygon(X, Y, Width, Height, text);
        }
    }
}
