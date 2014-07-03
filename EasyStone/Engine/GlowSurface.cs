using Tao.OpenGl;
using Msm.Geometry;
namespace EasyStone.Engine
{
    enum BloomDirection
    {
        Vertical,
        Horisontal
    }

    class GlowSurface
    {
        FramebufferSurface[] pass0;
        FramebufferSurface[] pass1;

        float bloomSize;
        int kernelSize;
        int filterCount;

        float[] kernel;
        float[] background;

        int filterProgram;
        int combineProgram;

        int bufferWidth;
        int bufferHeight;

        ISurfaceImplementation underlying;

        public GlowSurface(float[] kernel, int filterCount, float bloomSize, int bufferWidth, int bufferHeight)
        {
            this.kernelSize = kernel.Length;
            this.kernel = kernel;
            this.filterCount = filterCount;
            this.bloomSize = bloomSize;

            this.bufferHeight = bufferHeight;
            this.bufferWidth = bufferWidth;

            this.background = new float[] { 1, 1, 1 };

            CreatePrograms();
            CreateFramebuffers(bufferWidth, bufferHeight);
        }

        private void CreatePrograms()
        {
            int passv = ShaderRepository.Instance.GetShaderId("passv");
            int blitf = ShaderRepository.Instance.GetShaderId("blitf");
            int combine4f = ShaderRepository.Instance.GetShaderId("combine4f");
            int row3f = ShaderRepository.Instance.GetShaderId("row3f");

            filterProgram = ShaderRepository.CompileProgram(passv, row3f);
            combineProgram = ShaderRepository.CompileProgram(passv, combine4f);
        }

        public void BindUnderlying(ISurfaceImplementation underlying)
        {
            this.underlying = underlying;
        }

        private void CreateFramebuffers(int bufferWidth, int bufferHeight)
        {
            pass0 = new FramebufferSurface[filterCount];
            pass1 = new FramebufferSurface[filterCount];

            float sum = 0;
            for (int i = 0; i < kernelSize; i++)
            {sum += kernel[i];            }
            for (int i = 0; i < kernelSize; i++)
            { kernel[i] /= sum; }

            int width = bufferWidth;
            int height = bufferHeight;
            for (int i = 0; i < filterCount; i++)
            { 
                pass0[i] = new FramebufferSurface(width, height, true);
                pass1[i] = new FramebufferSurface(width, height, false);
                width = width >> 1;
                height = height >> 1;
            }
        }

        private void Blur(FramebufferSurface[] surfacesIn, FramebufferSurface[] surfacesOut, int count, BloomDirection direction)
        {
            int loc;

            Gl.glUseProgram(filterProgram);

            loc = Gl.glGetUniformLocation(filterProgram, new string[] { "source" });
            Gl.glUniform1i(loc, 0);

            loc = Gl.glGetUniformLocation(filterProgram, new string[] { "coefficients" });
            Gl.glUniform1fv(loc, kernelSize, kernel);

            loc = Gl.glGetUniformLocation(filterProgram, new string[] { "offsetx" });
            Gl.glUniform1f(loc, 0);

            loc = Gl.glGetUniformLocation(filterProgram, new string[] { "offsety" });
            Gl.glUniform1f(loc, 0);

            if (direction == BloomDirection.Horisontal)
            { loc = Gl.glGetUniformLocation(filterProgram, new string[] { "offsetx" }); }

            for (int i = 0; i < count; i++)
            {
                float offset = bloomSize / surfacesIn[i].Width;
                Gl.glUniform1f(loc, offset);

                int texture = surfacesIn[i].Texture;
                IRectangleSurface rectangleSurface = Renderer.Instance.CreateTexturedRectangleSurface(texture);
                surfacesOut[i].BindTo(rectangleSurface);

                rectangleSurface.DrawRectangle(new Vector2(-1, -1), new Vector2(2, 2), new Color4(255, 255, 255));
            }
        }

        public void Draw()
        {
            int loc;

            pass0[0].BindTo(underlying);

            underlying.Draw();

            int texture = pass0[0].Texture;
            for (int i = 1; i < filterCount; i++)
            {
                IRectangleSurface rectangleSurface = Renderer.Instance.CreateTexturedRectangleSurface(texture);
                pass0[i].BindTo(rectangleSurface);
                rectangleSurface.DrawRectangle(new Vector2(-1, -1), new Vector2(2, 2), new Color4(255, 255, 255));
            }

            Blur(pass0, pass1, filterCount, BloomDirection.Horisontal);
            Blur(pass1, pass0, filterCount, BloomDirection.Vertical);

            Gl.glUseProgram(combineProgram);
            loc = Gl.glGetUniformLocation(combineProgram, new string[] { "bkgd" });
            Gl.glUniform4fv(loc, 1, background);

            for (int i = 0; i < filterCount; i++)
            {
                Gl.glActiveTexture(Gl.GL_TEXTURE0 + i);
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, pass0[i].Texture);
                Gl.glEnable(Gl.GL_TEXTURE_2D);

                loc = Gl.glGetUniformLocation(combineProgram, new string[] { string.Format("Pass{0}", i) });
                Gl.glUniform1i(loc, i);
            }

            IRectangleSurface finalSurface = Renderer.Instance.CreateTexturedRectangleSurface(pass0[filterCount - 1].Texture);
            finalSurface.DrawRectangle(new Vector2(0, 0), new Vector2(2, 2), new Color4(255, 255, 255));

            Gl.glUseProgram(0);

            for (int i = 0; i < filterCount; i++)
            {
                Gl.glActiveTexture(Gl.GL_TEXTURE0 + i);
                Gl.glDisable(Gl.GL_TEXTURE_2D);
            }
            Gl.glActiveTexture(Gl.GL_TEXTURE0);
        }
    }
}
