using System.Collections.Generic;

namespace EasyStone.Engine.Particles
{
    class ExplosionStrategy : ParticleGenerator
    {
        ParticleStreamGenerator generator;

        private int maxParticles;

        public ExplosionStrategy(ParticleStreamGenerator generator, int maxParticles)
        {
            this.generator = generator;
            this.maxParticles = maxParticles;
        }

        public List<Particle> GenerateFirstParticles()
        {
            return generator.Generate(maxParticles, 0);
        }

        public List<Particle> UpdateGeneratorAndGetNewParticles(float delta)
        {
            return new List<Particle>();
        }
    }
}
