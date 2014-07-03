using Tao.OpenGl;
using System;
using Msm.Geometry;
using Tao.FreeGlut;
namespace EasyStone.Engine
{
    class Renderer
    {
        private static Renderer instance;
        public static Renderer Instance
        {
            get
            {
                if (instance == null)
                    instance = new Renderer();
                return instance;
            }
        }

        private Renderer()
        {
            Gl.glClearColor(1.0f, 1.0f, 1.0f, 1.0f);
        }

        public void BeginScene()
        {
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            
            Gl.glScalef(0.05f, 0.05f, 1);
        }

        public IRectangleSurface CreateRectangleSurface()
        {
            return new PrimitiveSurface(2, 4, 0, Gl.GL_QUADS);
        }

        public IRectangleSurface CreateTexturedRectangleSurface(int texture)
        {
            return new PrimitiveSurface(2, 4, texture, Gl.GL_QUADS);
        }

        public ILowLewelSurface CreateLowLewelSurface(int primitiveType)
        {
            return new PrimitiveSurface(2, 4, 0, primitiveType);
        }

        public IRectangleSurface CreateConnectedRectangleSurface()
        {
            return new PrimitiveSurface(2, 4, 0, Gl.GL_QUAD_STRIP);
        }

        public IPolygonSurface CreatePolygonSurface()
        {
            return new PolygonSurface();
        }

        public void CommitSurface(ISurface surface)
        {
            ISurfaceImplementation impl = (surface as ISurfaceImplementation);
            impl.Draw();
            impl.Clear();
        }

        public void BlitText(float x, float y, IntPtr font, string text, Color4 color)
        {
            Gl.glColor4ub(color.R, color.G, color.B, color.A);
            Gl.glRasterPos2f(x, y);

            Glut.glutBitmapString(font, text);
        }

        public void EndScene()
        {
            Glut.glutSwapBuffers();
        }
    }
}
