using Msm.Geometry;
using System.Collections.Generic;
using Tao.OpenGl;

namespace EasyStone.Engine
{
    class PolygonSurface : IPolygonSurface, ISurfaceImplementation
    {
        List<Polygon> polygons;
        List<Color4> colors;
        List<int> textures;

        public PolygonSurface()
        {
            polygons = new List<Polygon>();
            colors = new List<Color4>();
            textures = new List<int>();
        }

        public void DrawPolygon(Polygon shape, Color4 color)
        {
            polygons.Add(shape);
            colors.Add(color);
            textures.Add(0);
        }

        public void DrawPolygon(Polygon shape, Color4 color, int texture)
        {
            polygons.Add(shape);
            colors.Add(color);
            textures.Add(texture);
        }

        public void Draw()
        {
            for (int i = 0; i < polygons.Count; i++)
            {
                Polygon poly = polygons[i];
                Color4 col = colors[i];
                int texture = textures[i];

                if (texture != 0)
                {
                    Gl.glEnable(Gl.GL_TEXTURE_2D);
                    Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);
                }

                Gl.glColor4ub(col.R, col.G, col.B, col.A);

                Gl.glPushMatrix();

                Gl.glTranslatef(poly.Position.X, poly.Position.Y, 0);
                Gl.glScalef(poly.Size, poly.Size, 0);
                Gl.glRotatef(poly.Rotation, 0, 0, 1);
                Gl.glCallList(poly.Id);

                Gl.glPopMatrix();

                if (texture != 0)
                {
                    Gl.glDisable(Gl.GL_TEXTURE_2D);
                }
            }
        }

        public void Clear()
        {
            polygons.Clear();
            colors.Clear();
        }
    }
}
