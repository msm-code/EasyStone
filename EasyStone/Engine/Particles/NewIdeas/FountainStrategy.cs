using System.Collections.Generic;

namespace EasyStone.Engine.Particles
{
    class FountainStrategy : ParticleGenerator    
    {
        ParticleStreamGenerator generator;

        private int particlesPerSecond;
        private float totalTime;
        private float totalDelta;

        public FountainStrategy(ParticleStreamGenerator generator, int particlesPerSecond)
        {
            this.generator = generator;
            this.particlesPerSecond = particlesPerSecond;
            this.totalTime = 0;
            this.totalDelta = 0;
        }

        public List<Particle> GenerateFirstParticles()
        {
            return new List<Particle>();
        }

        public List<Particle> UpdateGeneratorAndGetNewParticles(float delta)
        {
            this.totalTime += delta;
            this.totalDelta += delta;

            int count = (int)(totalDelta * particlesPerSecond);
            totalDelta %= (1.0f / particlesPerSecond);
            return generator.Generate(count, totalTime);
        }
    }
}
