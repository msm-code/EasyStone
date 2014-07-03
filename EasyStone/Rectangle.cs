using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Msm.Geometry;

namespace EasyStone
{
    class Rectangle
    {
        public Rectangle(float left, float top, float width, float height)
        {
            this.Left = left;
            this.Top = top;
            this.Width = width;
            this.Height = height;
        }

        public float Left { get; set; }
        public float Top { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Right { get { return Left + Width; } }
        public float Bottom { get { return Top + Height; } }

        public Vector2 Center
        { get { return new Vector2(Left + Width / 2f, Top + Height / 2f); } }

        public Vector2 Collision(Rectangle other)
        {
            if (this.Left < other.Right && this.Right > other.Left &&
                 this.Top < other.Bottom && this.Bottom > other.Top)
            { return new Vector2(1f, 0); } // OMG

            return Vector2.Zero;
        }
    }
}
