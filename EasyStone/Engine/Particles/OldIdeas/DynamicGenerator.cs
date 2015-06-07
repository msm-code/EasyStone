using System.Collections.Generic;
using Msm.Geometry;
namespace EasyStone.Engine.Particles
{
    class DynamicGenerator : ParticleGenerator
    {
        ParticleSystemDescriptor descriptor;
        float particlesPerSecond;
        Angle direction;
        float directionVariation;
        Vector2 systemVelocity;
        Vector2 systemPosition;

        public DynamicGenerator(ParticleSystemDescriptor descriptor, 
            Angle direction, float directionVariation, int particlesPerSecond, Vector2 velocity)
        {
            this.descriptor = descriptor;
            this.particlesPerSecond = particlesPerSecond;
            this.direction = direction;
            this.directionVariation = directionVariation;
            this.systemVelocity = velocity;
            this.systemPosition = descriptor.Position;
        }

        public override List<Particle> GenerateFirstParticles()
        {
            return new List<Particle>();
        }

        public override List<Particle> UpdateAndGenerateNextParticles(float delta)
        {
            systemPosition += systemVelocity * delta;

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

                particles.Add(new Particle(systemPosition, velocity, start, end, life));
            }

            return particles;
        }
    }
}
