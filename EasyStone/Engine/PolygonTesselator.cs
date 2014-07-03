using Tao.OpenGl;
using System;

namespace EasyStone.Engine
{
    class PolygonTesselator
    {
        private void BeginCallback(int which)
        {
            Gl.glBegin(which);
        }

        private void EndCallback()
        {
            Gl.glEnd();
        }

        private unsafe void VertexCallback(IntPtr data)
        {
            double* d = (double*)data.ToPointer();

            double x = d[0];
            double y = d[1];
            double z = d[2];
            Gl.glVertex3d(d[0], d[1], d[2]);
        }

        private unsafe void TexturedVertexCallback(IntPtr data)
        {
            double* d = (double*)data.ToPointer();

            double x = d[0];
            double y = d[1];
            double z = d[2];
            double tex1 = d[3];
            double tex2 = d[4];

            Gl.glTexCoord2d(tex1, tex2);
            Gl.glVertex3d(d[0], d[1], d[2]);
        }        

        public int Tesselate(double[][] data, int verticles)
        {
            return TesselationTemplate((tesselator) =>
                {
                    for (int i = 0; i < verticles; i++)
                    { Glu.gluTessVertex(tesselator, data[i], data[i]); }
                });
        }

        public int TesselateTextured(double[][] verticles, double[][] textures, int vertexCount)
        {
            return TesselationTemplate((tesselator) =>
                {
                    for (int i = 0; i < vertexCount; i++)
                    {
                        double[] data = new double[] { verticles[i][0], verticles[i][1], verticles[i][2],
                    textures[i][0], textures[i][1]};
                        Glu.gluTessVertex(tesselator, verticles[i], data);
                    }
                });
        }

        private int TesselationTemplate(Action<Glu.GLUtesselator> tesselationCode)
        {
            int listId = Gl.glGenLists(1);

            Glu.GLUtesselator tesselator = Glu.gluNewTess();

            Glu.gluTessCallback(tesselator, Glu.GLU_TESS_BEGIN, (Glu.TessBeginCallback)BeginCallback);
            Glu.gluTessCallback(tesselator, Glu.GLU_TESS_END, (Glu.TessEndCallback)EndCallback);
            Glu.gluTessCallback(tesselator, Glu.GLU_TESS_VERTEX, (Glu.TessVertexCallback)TexturedVertexCallback);

            Gl.glNewList(listId, Gl.GL_COMPILE);

            Glu.gluTessBeginPolygon(tesselator, (IntPtr)0);
            Glu.gluTessBeginContour(tesselator);

            tesselationCode(tesselator);

            Glu.gluTessEndContour(tesselator);
            Glu.gluTessEndPolygon(tesselator);

            Gl.glEndList();

            Glu.gluDeleteTess(tesselator);

            return listId;
        }
    }
}
