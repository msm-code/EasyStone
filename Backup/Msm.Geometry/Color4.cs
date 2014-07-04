namespace Msm.Geometry
{
    public struct Color4
    {
        public Color4(byte r, byte g, byte b)
            :this()
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = 255;
        }

        public Color4(byte r, byte g, byte b, byte a)
            :this(r, g, b)
        {
            this.A = a;
        }

        public byte R
        { get; set; }

        public byte G
        { get; set; }

        public byte B
        { get; set; }

        public byte A
        { get; set; }

        public static Color4 Blue
        { get { return new Color4(0, 0, 255); } }

        public static Color4 Gray
        { get { return new Color4(100, 100, 100); } }

        public static Color4 Black
        { get { return new Color4(0, 0, 0); } }

        public static Color4 Red
        { get { return new Color4(155, 0, 0); } }

        public static Color4 operator *(Color4 c1, float scale)
        {
            return new Color4(
              (byte)(c1.R * scale),
              (byte)(c1.G * scale),
              (byte)(c1.B * scale),
              (byte)(c1.A * scale));
        }

        public Color4 Scale(float scale)
        {
            return new Color4(
              (byte)(R * scale),
              (byte)(G * scale),
              (byte)(B * scale),
              (byte)(A * scale));
        }

        public static Color4 Blend(Color4 start, Color4 end, float scale)
        {
            float scale2 = 1 - scale;

            return new Color4(
                (byte)(start.R * scale + end.R * scale2),
                (byte)(start.G * scale + end.G * scale2),
                (byte)(start.B * scale + end.B * scale2),
                (byte)(start.A * scale + end.A * scale2));
        }
    }
}
