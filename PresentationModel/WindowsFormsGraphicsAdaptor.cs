using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw2.PresentationModel
{

    internal class WindowsFormsGraphicsAdaptor : IDrawable
    {
        Graphics _graphics;
        Font font = new Font("Arial", 16, FontStyle.Bold);
        Brush brush = Brushes.Black;
        public WindowsFormsGraphicsAdaptor(Graphics graphics)
        {
            _graphics = graphics;
        }
        public void DrawEllipse(float X1, float Y1, float X2, float Y2, string Text)
        {
            float width = Math.Abs((float)X2 - (float)X1);
            float height = Math.Abs((float)Y2 - (float)Y1);
            float minX = Math.Min(X1, X2);
            float minY = Math.Min(Y1, Y2);
            _graphics.DrawEllipse(Pens.Black, minX, minY, width, height);
            _graphics.DrawString(Text, font, brush, new PointF((X1 + X2) / 2, (Y1 + Y2) / 2));
        }
        public void DrawRectangle(float X1, float Y1, float X2, float Y2, string Text)
        {
            float width = Math.Abs((float)X2 - (float)X1);
            float height = Math.Abs((float)Y2 - (float)Y1);
            float minX = Math.Min(X1, X2);
            float minY = Math.Min(Y1, Y2);
            _graphics.DrawRectangle(Pens.Black, minX, minY, width, height);
            _graphics.DrawString(Text, font, brush, new PointF((X1 + X2) / 2, (Y1 + Y2) / 2));
        }
        public void DrawOval(float X1, float Y1, float X2, float Y2, string Text)
        {
            float left = Math.Min(X1, X2);
            float right = Math.Max(X1, X2);
            float top = Math.Min(Y1, Y2);
            float bottom = Math.Max(Y1, Y2);

            float width = right - left;
            float height = bottom - top;

            PointF leftTopPoint = new PointF(left, top);
            PointF rightTopPoint = new PointF(right, top);
            PointF leftBottomPoint = new PointF(left, bottom);
            PointF rightBottomPoint = new PointF(right, bottom);

            // Draw straight lines for the top and bottom of the oval
            _graphics.DrawLine(Pens.Black, leftTopPoint, rightTopPoint);
            _graphics.DrawLine(Pens.Black, leftBottomPoint, rightBottomPoint);

            // Draw arcs for the left and right sides of the oval
            RectangleF leftArcRect = new RectangleF(left - (height / 2), top, height, height);
            RectangleF rightArcRect = new RectangleF(right - (height / 2), top, height, height);

            if (leftArcRect.Width > 0 && leftArcRect.Height > 0)
            {
                _graphics.DrawArc(Pens.Black, leftArcRect, 90, 180);
            }
            if (rightArcRect.Width > 0 && rightArcRect.Height > 0)
            {
                _graphics.DrawArc(Pens.Black, rightArcRect, -90, 180);
            }


            // Draw text in the center
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            _graphics.DrawString(Text, font, brush, new PointF((X1 + X2) / 2, (Y1 + Y2) / 2), format);
        }

        public void DrawPolygon(float X1, float Y1, float X2, float Y2, string Text)
        {
            PointF[] diamondes = new PointF[]
            {
                new PointF((X1+X2)/2, Math.Max(Y1, Y2)),
                new PointF(Math.Max(X1,X2), (Y1+Y2)/2),
                new PointF((X1+X2)/2, Math.Min(Y1, Y2)),
                new PointF(Math.Min(X1, X2), (Y1+Y2)/2)
            };
            _graphics.DrawPolygon(Pens.Black, diamondes);
            _graphics.DrawString(Text, font, brush, new PointF((X1 + X2) / 2, (Y1 + Y2) / 2));
        }

    }

}
