using System;

namespace Msm.Geometry
{
    public struct Rectangle// : Shape
    {
        public Rectangle(float x, float y, float width, float height)
            :this()
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public float X
        { get; set; }

        public float Y
        { get; set; }

        public float Width
        { get; set; }

        public float Height
        { get; set; }

        public Vector2 Center
        { get { return new Vector2(X + (Width / 2), Y + (Height / 2)); } }

        public Rectangle BoundingRectangle
        { get { return this; } }
    }
}
