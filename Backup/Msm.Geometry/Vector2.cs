using System;

namespace Msm.Geometry
{
    [Serializable]
    public struct Vector2
    {
        public Vector2(float x, float y)
            :this()
        {
            this.X = x;
            this.Y = y;
        }

        public float X
        { get; set; }

        public float Y
        { get; set; }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        { return new Vector2(v1.X + v2.X, v1.Y + v2.Y); }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        { return new Vector2(v1.X - v2.X, v1.Y - v2.Y); }

        public static Vector2 operator -(Vector2 v1)
        { return new Vector2(0 - v1.X, 0 - v1.Y); }

        public static Vector2 operator *(Vector2 v1, float times)
        { return new Vector2(v1.X * times, v1.Y * times); }

        public static Vector2 operator /(Vector2 v1, float times)
        { return new Vector2(v1.X / times, v1.Y / times); }

        public static bool operator ==(Vector2 v1, Vector2 v2)
        { return (v1.X == v2.X && v1.Y == v2.Y); }

        public static bool operator !=(Vector2 v1, Vector2 v2)
        { return (v1.X != v2.X || v1.Y != v2.Y); }

        public override int GetHashCode()
        { return base.GetHashCode(); }

        public override bool Equals(object obj)
        {
            if (obj is Vector2)
            {
                Vector2 vec = (Vector2)obj;
                return vec.X == this.X && vec.Y == this.Y;
            }
            return false;
        }
        

        public float Length
        {
            get { return (float)Math.Sqrt(X * X + Y * Y); }
            set
            {
                float scale = value / Length;
                this.X *= scale;
                this.Y *= scale;
            }
        }

        public Angle Rotation
        {
            get
            {
                if (this == Zero)
                {
                    //throw new InvalidOperationException("Vector.Zero has no rotation");
                    return Angle.Zero;
                }
                return AngleBetween(this, new Vector2(0, -1));
            }
            set { this = Vector2.FromRotationAndLength(value, this.Length); }
        }

        public Vector2 Rotated(Angle angle)
        {
            if (this != Vector2.Zero)
                this.Rotation += angle;
            return this;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", X, Y);
        }

        public static readonly Vector2 Zero = new Vector2(0, 0);

        public static Vector2 FromRotationAndLength(Angle rotation, float length)
        {
            float newX = (float)-(Math.Sin(rotation.Radians) * -length);
            float newY = (float)-(Math.Cos(rotation.Radians) * length);

            return new Vector2(newX, newY);
        }

        public float DotProduct(Vector2 v)
        {
            float sum = 0;
            sum += this.X * v.X;
            sum += this.Y * v.Y;
            return sum;
        }

        public float Magnitude
        {
            get { return (float)Math.Sqrt(X * X + Y * Y); }
        }

        public void Normalize()
        {
            float magnitude = Magnitude;
            X = X / magnitude;
            Y = Y / magnitude;
        }

        public static Angle AngleBetween(Vector2 v1, Vector2 v2)
        {
            double radians = Math.Acos(v1.DotProduct(v2) / (v1.Length * v2.Length));

            if (v1.X < v2.X)
                radians = (2 * Math.PI - radians);

            return Angle.FromRadians(radians);
        }

        public void Normalise()
        {
            float length = this.Length;
            if (length == 0.0f)						// Prevents Divide By 0 Error By Providing
                length = 1.0f;						// An Acceptable Value For Vectors To Close To 0.

            X /= length;
            Y /= length;
        }
    }
}
