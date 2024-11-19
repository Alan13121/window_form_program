using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace hw2
{
    
    public class Shapes
    {
        
        List<Shape> Shapes_list = new List<Shape>();
        ShapeFactory fatory = new ShapeFactory();
        int ID_Count = 1;
        public void Add_shape(string shapeType, string text, int X, int Y, int Height, int Width)
        {
            Shapes_list.Add(fatory.CreateShape(shapeType, ID_Count, text, X, Y, Height, Width));
            ID_Count++;
        }
        public void Add_shape(Shape new_shape)
        {
            new_shape.ID = ID_Count;
            Shapes_list.Add(new_shape);
            ID_Count++;
        }
        public void remove_shape(int ID)
        {
            Shapes_list.RemoveAll(s => s.ID == ID);
        }
        public List<Shape> get_list()
        {
            return Shapes_list;
        }

    }
    public class Shape
    {

        public string ShapeName { get; set; }

        public int ID { get; set; }
        public string text { get; set; }

        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public float X1 { get; set; }
        public float Y1 { get; set; }
        public float X2 { get; set; }
        public float Y2 { get; set; }

        
        public void DrawShape(IDrawable shape)
        {
            ShapeFactory fatory = new ShapeFactory();
            fatory.DrawShape(shape,X1, Y1, X2, Y2, text, ShapeName);
            
        }
    }

    public class Start : Shape
    {
        public Start()
        {
            ShapeName = "Start";
        }

    }
    public class Terminator : Shape
    {
        public Terminator()
        {
            ShapeName = "Terminator";
        }
    }

    public class Process : Shape
    {
        public Process()
        {
            ShapeName = "Process";
        }
    }
    public class Decision : Shape
    {
        public Decision()
        {
            ShapeName = "Decision";
        }
    }
    
}
