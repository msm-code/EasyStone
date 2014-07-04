using System.Collections.Generic;
using Msm.Geometry;
using System;

namespace EasyStone.Engine.Particles
{
    class SerpentRenderer : ParticleRenderer
    {
        public override void Render(List<Particle> particles)
        {
            //IRectangleSurface surface = Renderer.Instance.CreateConnectedRectangleSurface();
            ILowLewelSurface surface = Renderer.Instance.CreateLowLewelSurface(Tao.OpenGl.Gl.GL_QUAD_STRIP);

            for (int i = 1; i < particles.Count; i++)
            {
                Particle last = particles[i - 1];
                Particle curr = particles[i];

                Angle rotation = (last.Position- curr.Position).Rotation;
                Vector2 offset = Vector2.FromRotationAndLength(rotation + Angle.FromDegrees(90), 0.2f);

                surface.SetNextVertexPosition(curr.Position + offset, curr.Color);
                surface.SetNextVertexPosition(curr.Position- offset, curr.Color);
            }

            Renderer.Instance.CommitSurface(surface);
        }
    }
}
