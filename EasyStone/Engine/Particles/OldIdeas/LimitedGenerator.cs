using System.Collections.Generic;
using System.Linq;

namespace EasyStone.Engine.Particles
{
    class LimitedGenerator : ParticleGenerator
    {
        ParticleGenerator underlying;
        int particlesLeft;

        public LimitedGenerator(ParticleGenerator underlying, int generateUpToParticles)
        {
            this.underlying = underlying;
            this.particlesLeft = generateUpToParticles;
        }

        public override List<Particle> GenerateFirstParticles()
        {
            List<Particle> generated = underlying.GenerateFirstParticles();

            return CheckCountOfParticlesLeftAndReturn(generated);
        }

        public override List<Particle> UpdateAndGenerateNextParticles(float delta)
        {
            if (particlesLeft == 0)
                return new List<Particle>();

            List<Particle> generated = underlying.UpdateAndGenerateNextParticles(delta);

            return CheckCountOfParticlesLeftAndReturn(generated);
        }

        private List<Particle> CheckCountOfParticlesLeftAndReturn(List<Particle> generated)
        {
            particlesLeft -= generated.Count;

            if (particlesLeft < 0)
                return generated.Take(generated.Count + particlesLeft).ToList();
            else
                return generated;
        }
    }
}
