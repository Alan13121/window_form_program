using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw2
{
    public interface IDrawable
    {
        void DrawEllipse(float X, float Y, float Width, float Height, string Text);
        void DrawRectangle(float X, float Y, float Width, float Height, string Text);
        void DrawOval(float X, float Y, float Width, float Height, string Text);
        void DrawPolygon(float X, float Y, float Width, float Height, string Text);
        void DrawBoundingBox(float X, float Y, float Width, float Height, string Text, PointF OrangeDot);
        void DrawText(float X, float Y, float Width, float Height, string Text, PointF OrangeDot);
    }
}

