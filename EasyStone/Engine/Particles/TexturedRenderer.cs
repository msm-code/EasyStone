using System.Collections.Generic;
using Msm.Geometry;

namespace EasyStone.Engine.Particles
{
    class TexturedRenderer : ParticleRenderer
    {
        int texture;
        float initialSize;
        float finalSize;

        public TexturedRenderer(int texture, float initialSize, float finalSize)
        {
            this.texture = texture;
            this.initialSize = initialSize;
            this.finalSize = finalSize;
        }

        public override void Render(List<Particle> particles)
        {
            IRectangleSurface surface = Renderer.Instance.CreateTexturedRectangleSurface(texture);

            foreach (Particle particle in particles)
            {
                float size = initialSize + (1 - particle.LifeFactor) * (finalSize - initialSize);
                Vector2 sizev = new Vector2(size, size);
                surface.DrawRectangle(particle.Position - sizev / 2, sizev, particle.Color);
            }

            Renderer.Instance.CommitSurface(surface);
        }
    }
}
