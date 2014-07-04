using Msm.Geometry;
using Tao.FreeGlut;
using Tao.OpenGl;

namespace EasyStone
{
    static class Camera
    {
        private const float Scale = 20.0f;

        public static Vector2 ScreenToGl(int x, int y)
        {
            int[] viewport = new int[4];
            double[] modelview = new double[16];
            double[] projection = new double[16];
            float winX, winY, winZ;
            double posX, posY, posZ;
            Gl.glGetDoublev(Gl.GL_MODELVIEW_MATRIX, modelview);
            Gl.glGetDoublev(Gl.GL_PROJECTION_MATRIX, projection);
            Gl.glGetIntegerv(Gl.GL_VIEWPORT, viewport);

            winX = (float)x;
            winY = (float)viewport[3] - (float)y;
            Gl.glReadPixels(x, (int)winY, 1, 1, Gl.GL_DEPTH_COMPONENT, Gl.GL_FLOAT, out winZ);

            Glu.gluUnProject(winX, winY, winZ, modelview, projection, viewport, out posX, out posY, out posZ);

            return new Vector2((float)posX, (float)posY);
        }
    }
}
