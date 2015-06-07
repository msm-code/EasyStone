using System.Collections.Generic;
using Msm.Geometry;
using System;
namespace EasyStone.Engine.Particles
{
    class RawParticleStreamGenerator : ParticleStreamGenerator
    {
        private ParticleSystemDescriptor descriptor;
        private Random r;

        public RawParticleStreamGenerator(ParticleSystemDescriptor descriptor)
        {
            this.descriptor = descriptor;
            this.r = new Random();
        }

        public List<Particle> Generate(int count, float totalTime)
        {
            List<Particle> particles = new List<Particle>();

            for (int i = 0; i < count; i++)
            {
                Angle rotation = Angle.FromDegrees(r.Next());
                Vector2 velocity = Vector2.FromRotationAndLength(rotation, descriptor.MinVelocity +
                    (float)r.NextDouble() * descriptor.VelocityVariation);
                Color4 start = descriptor.StartColor;
                Color4 end = descriptor.EndColor;
                float life = descriptor.MinLifetime + descriptor.LifetimeVariation * (float)r.NextDouble();

                particles.Add(new Particle(descriptor.Position, velocity, start, end, life));
            }

            return particles;
        }
    }
}
