using System.Collections.Generic;
using System.Linq;

namespace EasyStone.Engine.Particles
{
    class LimitedModyifer : ParticleStreamGenerator
    {
        ParticleStreamGenerator underlying;
        int particlesLeft;

        public LimitedModyifer(ParticleStreamGenerator underlying, int maxParticles)
        {
            this.underlying = underlying;
            this.particlesLeft = maxParticles;
        }

        public List<Particle> Generate(int count, float totalTime)
        {
            if (particlesLeft == 0)
                return new List<Particle>();

            List<Particle> generated = underlying.Generate(count,totalTime);

            particlesLeft -= count;

            if (particlesLeft < 0)
                return generated.Take(generated.Count + particlesLeft).ToList();
            else
                return generated;
        }
    }
}
