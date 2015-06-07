using System.Collections.Generic;
using Msm.Geometry;

namespace EasyStone.Engine.Particles
{
    class PointRenderer : ParticleRenderer
    {
        public override void Render(List<Particle> particles)
        {
            IRectangleSurface surface = Renderer.Instance.CreateRectangleSurface();

            foreach (Particle p in particles)
            {
                surface.DrawRectangle(p.Position - new Vector2(0.35f, 0.35f), new Vector2(0.7f, 0.7f), p.Color);
            }

            Renderer.Instance.CommitSurface(surface);
        }
    }
}
