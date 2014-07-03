using System.Collections.Generic;
using Msm.Geometry;
using System;

namespace EasyStone.Engine.Particles
{
    class FullRandomGenerator : ParticleGenerator
    {
        private ParticleSystemDescriptor descriptor;
        private int count;

        public FullRandomGenerator(ParticleSystemDescriptor descriptor, int maxCount)
        {
            this.descriptor = descriptor;
            this.count = maxCount;
        }

        public override List<Particle> GenerateFirstParticles()
        {
            List<Particle> particles = new List<Particle>();

            for (int i = 0; i < count; i++)
            {
                Angle rotation = Angle.FromDegrees(rgen.Next());
                Vector2 velocity = Vector2.FromRotationAndLength(rotation, descriptor.MinVelocity +
                    (float)rgen.NextDouble() * descriptor.VelocityVariation);
                Color4 start = descriptor.StartColor;
                Color4 end = descriptor.EndColor;
                float life = descriptor.MinLifetime + descriptor.LifetimeVariation * (float)rgen.NextDouble();

                particles.Add(new Particle(descriptor.Position, velocity, start, end, life));
            }

            return particles;
        }

        public override List<Particle> UpdateAndGenerateNextParticles(float systemLifetime)
        {
            return new List<Particle>();
        }
    }
}
