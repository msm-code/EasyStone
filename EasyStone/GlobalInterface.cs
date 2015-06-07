using System.Collections.Generic;
using Msm.Geometry;
using Tao.FreeGlut;
using System;
using EasyStone.Engine;
namespace EasyStone
{
    class InfoBox
    {
        public string Text {get;set;}
        public float LifeTime {get;set;}

        public InfoBox(string text)
        {
            this.Text = text;
            this.LifeTime = 0;
        }
    }

    class GlobalInterface
    {
        private const float MaxLifetime = 5.0f;
        static float offset;

        static List<InfoBox> boxes;

        public static void AddInfoBox(string text)
        {
            if (boxes == null)
                boxes = new List<InfoBox>();

            boxes.Add(new InfoBox(text));
        }

        public static void Update(float delta)
        {
            if (boxes == null)
                boxes = new List<InfoBox>();

            for(int i = 0; i < boxes.Count; i++)
            {
                boxes[i].LifeTime += delta;
                if (boxes[i].LifeTime > MaxLifetime)
                {
                    boxes.RemoveAt(i);
                    offset += 50;
                }
            }

            if (offset > 0)
                offset -= delta * 120; 
        }

        public static void Redraw()
        {
            IRectangleSurface surface = Renderer.Instance.CreateRectangleSurface();

            for (int i = 0; i < boxes.Count; i++)
            {
                byte alpha = (byte)Math.Min(128, (int)((MaxLifetime - boxes[i].LifeTime) * 100));
                Vector2 position = Camera.ScreenToGl(10, 60 + i * 50 + (int)offset);

                surface.DrawRectangle(position, new Vector2(12f, 2f), new Color4(230, 230, 230, alpha));
            }

            Renderer.Instance.CommitSurface(surface);

            for (int i = 0; i < boxes.Count; i++)
            {
                byte alpha = (byte)Math.Min(255, (int)((MaxLifetime - boxes[i].LifeTime) * 100));

                Vector2 position = Camera.ScreenToGl(15, 50 + i * 50 + (int)offset);

                Renderer.Instance.BlitText(position.X, position.Y, Glut.GLUT_BITMAP_HELVETICA_18, boxes[i].Text, new Color4(100, 200, 250, alpha));
            }
        }
    }
}
