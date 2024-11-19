using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw2
{
    public interface IDrawable
    {
        void DrawEllipse(float X1, float Y1, float X2, float Y2, string Text);
        void DrawRectangle(float X1, float Y1, float X2, float Y2, string Text);
        void DrawOval(float X1, float Y1, float X2, float Y2, string Text);
        void DrawPolygon(float X1, float Y1, float X2, float Y2, string Text);
    }
}

