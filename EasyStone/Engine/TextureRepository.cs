using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using Tao.OpenGl;
namespace EasyStone.Engine
{
    class TextureRepository
    {
        private static TextureRepository instance;

        public static TextureRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new TextureRepository();
                return instance;
            }
        }

        public TextureRepository()
        {
            this.textures = new Dictionary<string, int>();
        }

        Dictionary<string, int> textures;

        public unsafe void LoadTexture(string path, string id)
        {
            Bitmap image = new Bitmap(path);

            System.Drawing.Rectangle imageBounds = new System.Drawing.Rectangle(0, 0, image.Width, image.Height);
            BitmapData data = new BitmapData();
            image.LockBits(imageBounds, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb, data);

            byte* dataPtr = (byte*)data.Scan0.ToPointer();
            int width = data.Width;
            int height = data.Height;

            int texture;
            Gl.glGenTextures(1, out texture);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texture);
            Glu.gluBuild2DMipmaps(Gl.GL_TEXTURE_2D, 4, width, height,
                   Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, dataPtr);

            image.UnlockBits(data);

            this.textures[id] = texture;
        }

        public int GetTexture(string id)
        {
            return textures[id];
        }
    }
}
