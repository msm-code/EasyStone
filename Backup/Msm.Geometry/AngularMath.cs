using System;

namespace Msm.Geometry
{
    public static class AngularMath
    {
        public static double DegToRad(double deg)
        {
            return (double)(Math.PI * deg / 180.0f);
        }

        public static double RadToDeg(double rad)
        {
            return (double)(rad * 180 / Math.PI);
        }

        public const double MaxDegrees = 360;

        public const double MaxRadians = 2 * Math.PI;
    }
}
