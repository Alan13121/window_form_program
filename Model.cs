using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using hw2;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Drawing;

namespace hw2
{
    internal class Model
    {
        
        // Base class Shape
        Shapes shapes = new Shapes();
        public event ModelChangedEventHandler _modelChanged;
        public delegate void ModelChangedEventHandler();
        int ID = 1;
        int type = -1;
        float x;
        float y;
        float height;
        float width;
        bool isPressed = false;
        bool isdrawing = false;
        enum kindsOfShape
        {
            Start = 0,
            Terminator,
            Process,
            Decision
        }
        
        Shape _hint = new Shape();

        public List<Shape> GetShapes()
        {
            return shapes.get_list();
        }
        public List<Shape> enter_new_shape(string[] new_shape)
        {
            
            shapes.Add_shape(new_shape[0], new_shape[1], int.Parse(new_shape[2]), int.Parse(new_shape[3]), int.Parse(new_shape[4]), int.Parse(new_shape[5]));
            return shapes.get_list();
        }

        public void remove_shape(int ID)
        {
            shapes.remove_shape(ID);
        }
        public void Draw(IDrawable graphic)
        {
            List<Shape> Shapes_list = shapes.get_list();
            foreach (Shape aShape in Shapes_list)
            {
               
                aShape.DrawShape(graphic);
            }
            if (isPressed)
                _hint.DrawShape(graphic);
        }
        public void PointerPressed(float x, float y)
        {
            _hint.X1 = x;
            _hint.Y1 = y;
            _hint.text = "";
            isPressed = true;
        }
        void NotifyModelChanged()
        {
            if (_modelChanged != null)
                _modelChanged();
        }
        public void PointerMoved(float x, float y)
        {
            if (isPressed && type != -1)
            {
                _hint.X2 = x;
                _hint.Y2 = y;
                NotifyModelChanged();
            }
        }
        public void PointerReleased(float x, float y)
        {
            if (isPressed && type != -1)
            {
                isPressed = false;
                Shape shape = new Shape();
                shape.ShapeName = _hint.ShapeName;
                shape.text = RandomStringBuilder.GenerateRandomString();
                shape.X1 = _hint.X1;
                shape.Y1 = _hint.Y1;
                shape.X2 = x;
                shape.Y2 = y;
                shape.X = Math.Min(shape.X1, shape.X2);
                shape.Y = Math.Min(shape.Y1, shape.Y2);
                shape.Width = Math.Abs(shape.X2 - shape.X1);
                shape.Height = Math.Abs(shape.Y2 - shape.Y1);

                //Console.WriteLine(shape.ShapeName + ","+ shape.ID + ","+ shape.text + ","+ shape.X1 + ","+ shape.Y1 + ","+ shape.X2 + ","+ shape.Y2  + ","+ shape.X + ","+ shape.Y + "," + shape.Width + ","+ shape.Height );
                shapes.Add_shape(shape);
                
                type = -1;
                NotifyModelChanged();
            }
        }
        public void SetType(int Type)
        {
            type = Type;
            if (Type == 0)
            {
                _hint.ShapeName = "Start";
            }
            else if (Type == 1)
            {
                _hint.ShapeName = "Terminator";
            }
            else if (Type == 2)
            {
                _hint.ShapeName = "Process";
            }
            else if (Type == 3)
            {
                _hint.ShapeName = "Decision";
            }
            else
            {
                Console.WriteLine("type wrong");
            }

        }

    }
    









}

//