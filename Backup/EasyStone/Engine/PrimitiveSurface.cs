using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Msm.Geometry;

namespace EasyStone.Engine
{
    class PrimitiveSurface : ISurfaceImplementation, IRectangleSurface, ITriangleSurface, ILowLewelSurface
    {
        NativeBuffer<float> verticles;
        NativeBuffer<byte> colors;
        NativeBuffer<float> textures;
        int texture;

        int vertexCoordinates;
        int colorChannels;

        int primitiveType;

        public PrimitiveSurface(int vertexCoordinates, int colorChannels, int texture, int primitiveType)
        {
            this.vertexCoordinates = vertexCoordinates;
            if (vertexCoordinates != 0)
                verticles = new NativeBuffer<float>();

            this.colorChannels = colorChannels;
            if (colorChannels != 0)
                colors = new NativeBuffer<byte>();

            this.texture = texture;
            if (texture != 0)
                textures = new NativeBuffer<float>();

            this.primitiveType = primitiveType;
        }

        public void Draw()
        {
            if (verticles != null)
            {
                Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
                Gl.glVertexPointer(vertexCoordinates, Gl.GL_FLOAT, 0, verticles.Data);
            }
            if (colors != null)
            {
                Gl.glEnableClientState(Gl.GL_COLOR_ARRAY);
                Gl.glColorPointer(colorChannels, Gl.GL_UNSIGNED_BYTE, 0, colors.Data);
            }
            if (textures != null)
            {
                Gl.glEnable(Gl.GL_TEXTURE_2D);
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);

                Gl.glEnableClientState(Gl.GL_TEXTURE_COORD_ARRAY);
                Gl.glTexCoordPointer(2, Gl.GL_FLOAT, 0, textures.Data);
            }

            Gl.glDrawArrays(primitiveType, 0, verticles.Count / vertexCoordinates);

            Gl.glDisable(Gl.GL_TEXTURE_2D);
            Gl.glDisableClientState(Gl.GL_TEXTURE_COORD_ARRAY);
            Gl.glDisableClientState(Gl.GL_COLOR_ARRAY);
            Gl.glDisableClientState(Gl.GL_VERTEX_ARRAY);
        }

        public void Clear()
        {
            if (verticles != null)
                verticles.Clear();
            if (colors != null)
                colors.Clear();
        }

        #region Specific Shapes
        void IRectangleSurface.DrawRectangle(Vector2 position, Vector2 size, Color4 color)
        {
            verticles.Add(position.X, position.Y);
            verticles.Add(position.X + size.X, position.Y);
            verticles.Add(position.X + size.X, position.Y + size.Y);
            verticles.Add(position.X, position.Y + size.Y);

            for (int i = 0; i < 4; i++)
                colors.Add(color.R, color.G, color.B, color.A);

            if (textures != null)
            {
                textures.Add(0, 0);
                textures.Add(1, 0);
                textures.Add(1, 1);
                textures.Add(0, 1);
            }
        }

        public void DrawTriangle(Vector2 p1, Vector2 p2, Vector2 p3, Color4 c1, Color4 c2, Color4 c3)
        {
            verticles.Add(p1.X, p1.Y);
            verticles.Add(p2.X, p2.Y);
            verticles.Add(p3.X, p3.Y);

            colors.Add(c1.R, c1.G, c1.B, c1.A);
            colors.Add(c2.R, c2.G, c2.B, c2.A);
            colors.Add(c3.R, c3.G, c3.B, c3.A);
        }

        public void SetNextVertexPosition(Vector2 position, Color4 color)
        {
            verticles.Add(position.X, position.Y);

            colors.Add(color.R, color.G, color.B, color.A);
        }
        #endregion
    }
}
