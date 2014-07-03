using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace EasyStone.Engine
{
    class FramebufferSurface : IHighLevelSurface
    {
        int width;
        int height;
        int texture;
        int fbo;

        int filter = Gl.GL_LINEAR;

        public FramebufferSurface(int width, int height, bool linear)
        {
            this.width = width;
            this.height = height;

            this.filter = linear ? Gl.GL_LINEAR : Gl.GL_NEAREST;

            Gl.glGenTextures(1, out texture);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);
            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, width, height, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, (byte[])null);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, filter);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, filter);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, 0);

            Gl.glGenFramebuffersEXT(1, out fbo);
            Gl.glBindFramebufferEXT(Gl.GL_FRAMEBUFFER_EXT, fbo);
            Gl.glFramebufferTexture2DEXT(Gl.GL_FRAMEBUFFER_EXT, Gl.GL_COLOR_ATTACHMENT0_EXT, Gl.GL_TEXTURE_2D, texture, 0);
            Gl.glBindFramebufferEXT(Gl.GL_FRAMEBUFFER_EXT, 0);
        }



        public int Width
        { get { return width; } }

        public int Height
        { get { return height; } }

        public int Texture
        { get { return texture; } }

        public void BindTo(ISurface surface)
        {

        }
    }
}
