using System.Collections.Generic;
using System;
using Msm.Geometry;

namespace EasyStone.Engine.Particles
{
    class RegularGenerator : ParticleGenerator
    {
        ParticleSystemDescriptor descriptor;
        Random r;
        bool fixFirst;
        int count;

        public RegularGenerator(ParticleSystemDescriptor descriptor, int maxCount, bool fixFirst)
        {
            this.descriptor = descriptor;
            this.r = new Random();
            this.fixFirst = fixFirst;
            this.count = maxCount;
        }

        public override List<Particle> GenerateFirstParticles()
        {
            List<Particle> particles = new List<Particle>();

            if (fixFirst)
            {
                particles.Add(new Particle(descriptor.Position, Vector2.Zero,
                    descriptor.StartColor, descriptor.EndColor,
                    descriptor.MinLifetime + descriptor.LifetimeVariation));
            }

            for (int i = 0; i < count; i++)
            {
                Angle rotation = Angle.FromDegrees((i / (float)count) * 360);
                Vector2 velocity = Vector2.FromRotationAndLength(rotation, descriptor.MinVelocity +
                    (float)r.NextDouble() * descriptor.VelocityVariation);
                Color4 start = descriptor.StartColor;
                Color4 end = descriptor.EndColor;
                float life = descriptor.MinLifetime + descriptor.LifetimeVariation * (float)r.NextDouble();

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
