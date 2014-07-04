using System.Collections.Generic;
using Msm.Geometry;

namespace EasyStone.Engine.Particles
{
    class SmoothRenderer : ParticleRenderer
    {
        public override void Render(List<Particle> particles)
        {
            ILowLewelSurface surface = Renderer.Instance.CreateLowLewelSurface(Tao.OpenGl.Gl.GL_TRIANGLE_FAN);

            foreach (Particle p in particles)
            {
                surface.SetNextVertexPosition(p.Position, p.Color);
            }

            if (particles.Count > 2)
                surface.SetNextVertexPosition(particles[1].Position, particles[1].Color);

            Renderer.Instance.CommitSurface(surface);
        }
    }
}
