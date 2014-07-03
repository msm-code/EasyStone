/*using Tao.OpenGl;
namespace EasyStone.Engine
{
    class RenderToTextureSurface : ISurfaceImplementation
    {
        private ISurfaceImplementation underlying;
        private int framebufferId;
        private int renderbufferId;
        private int textureId;

        private int width;
        private int height;

        public RenderToTextureSurface(ISurface underlying, int width, int height)
        {
            this.underlying = underlying as ISurfaceImplementation;

            this.width = width;
            this.height = height;

            try
            {
                Gl.glGenFramebuffersEXT(1, out framebufferId);
                Gl.glGenRenderbuffersEXT(1, out renderbufferId);
            }
            catch (System.Exception e)
            {
            }
            Gl.glGenTextures(1, out textureId);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureId);
        }

        public void Draw()
        {
            Gl.glBindFramebufferEXT(Gl.GL_FRAMEBUFFER_EXT, framebufferId);
            Gl.glFramebufferTexture2DEXT(
                Gl.GL_FRAMEBUFFER_EXT, Gl.GL_COLOR_ATTACHMENT0_EXT, Gl.GL_TEXTURE_2D, textureId, 0);
            Gl.glBindRenderbufferEXT(Gl.GL_RENDERBUFFER_EXT, renderbufferId);

            Gl.glRenderbufferStorageEXT(Gl.GL_RENDERBUFFER_EXT, Gl.GL_DEPTH_COMPONENT24, width, height);

            Gl.glFramebufferRenderbufferEXT(Gl.GL_FRAMEBUFFER_EXT, Gl.GL_DEPTH_ATTACHMENT_EXT, Gl.GL_RENDERBUFFER_EXT, renderbufferId);

            underlying.Draw();

            Gl.glBindFramebufferEXT(Gl.GL_FRAMEBUFFER_EXT, 0);
            Gl.glBindRenderbufferEXT(Gl.GL_RENDERBUFFER_EXT, 0);
        }

        public void Clear()
        {
            underlying.Clear();
        }

        public int TextureId
        { get { return textureId; } }
    }
}
*/