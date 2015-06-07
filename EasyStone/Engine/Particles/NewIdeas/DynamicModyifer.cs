using System.Collections.Generic;
using Msm.Geometry;
namespace EasyStone.Engine.Particles
{
    class DynamicModyifer : ParticleStreamGenerator
    {
        private ParticleStreamGenerator underlying;
        private Vector2 systemVelocity;

        public DynamicModyifer(ParticleStreamGenerator underlying, Vector2 systemVelocity)
        {
            this.underlying = underlying;
            this.systemVelocity = systemVelocity;
        }

        public List<Particle> Generate(int count, float totalTime)
        {
            List<Particle> oryginalStream = underlying.Generate(count, totalTime);

            foreach (Particle oryginal in oryginalStream)
            {
                oryginal.Position += systemVelocity * totalTime;
            }

            return oryginalStream;
        }
    }
}
