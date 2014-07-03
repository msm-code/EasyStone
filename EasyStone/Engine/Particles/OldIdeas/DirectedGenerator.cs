using Msm.Geometry;
using System.Collections.Generic;

namespace EasyStone.Engine.Particles
{
    class DirectedGenerator : ParticleGenerator
    {
        ParticleSystemDescriptor descriptor;
        float particlesPerSecond;
        Angle direction;
        float directionVariation;

        public DirectedGenerator(ParticleSystemDescriptor descriptor, 
            Angle direction, float directionVariation, int particlesPerSecond)
        {
            this.descriptor = descriptor;
            this.particlesPerSecond = particlesPerSecond;
            this.direction = direction;
            this.directionVariation = directionVariation;
        }

        public override List<Particle> GenerateFirstParticles()
        {
            return new List<Particle>();
        }

        public override List<Particle> UpdateAndGenerateNextParticles(float delta)
        {
            List<Particle> particles = new List<Particle>();

            int count = (int)(delta * particlesPerSecond);

            for (int i = 0; i < count; i++)
            {
                Angle rotation = direction + Angle.FromDegrees(rgen.NextDouble() * directionVariation - directionVariation / 2);
                Vector2 velocity = Vector2.FromRotationAndLength(rotation, descriptor.MinVelocity +
                    (float)rgen.NextDouble() * descriptor.VelocityVariation);
                Color4 start = descriptor.StartColor;
                Color4 end = descriptor.EndColor;
                float life = descriptor.MinLifetime + descriptor.LifetimeVariation * (float)rgen.NextDouble();

                particles.Add(new Particle(descriptor.Position, velocity, start, end, life));
            }

            return particles;
        }
    }
}
