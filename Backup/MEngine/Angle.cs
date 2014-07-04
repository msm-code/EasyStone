using System;

namespace MEngine
{
    [Serializable]
    public struct Angle
    {
        // representation of degree
        private double radians;

        private Angle(double radians)
            : this()
        {
            while (radians < 0)
                radians = radians += AngularMath.MaxRadians;

            this.radians = radians % AngularMath.MaxRadians;
        }

        public static Angle FromRadians(double radians)
        {
            return new Angle(radians);
        }

        public static Angle FromDegrees(double degrees)
        {
            double radians = AngularMath.DegToRad(degrees);
            return Angle.FromRadians(radians);
        }

        public double Radians
        {
            get { return radians; }
            set { radians = value; }
        }

        public double Degree
        {
            get { return AngularMath.RadToDeg(radians); }
            set { radians = AngularMath.DegToRad(value); }
        }

        public static Angle operator +(Angle a1, Angle a2)
        { return new Angle(a1.radians + a2.radians); }

        public static Angle operator -(Angle a1, Angle a2)
        { return new Angle(a1.radians - a2.radians); }

        public static Angle operator -(Angle a1)
        { return new Angle(a1.radians - AngularMath.MaxRadians / 2.0); }

        public static Angle Zero
        { get { return new Angle(); } }

        public override bool Equals(object obj)
        {
            if (obj is Angle)
            {
                Angle ang = (Angle)obj;
                return this.radians == ang.radians;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("({0} degrees)", Degree);
        }
    }
}
