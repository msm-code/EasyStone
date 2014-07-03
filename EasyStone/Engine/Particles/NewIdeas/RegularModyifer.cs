using System.Collections.Generic;
using System.Linq;
using Msm.Geometry;

namespace EasyStone.Engine.Particles
{
    class RegularModyifer : ParticleStreamGenerator
    {
        ParticleStreamGenerator underlying;
        ParticleSystemDescriptor descriptor;

        public RegularModyifer(ParticleStreamGenerator underlying, ParticleSystemDescriptor descriptor)
        {
            this.underlying = underlying;
            this.descriptor = descriptor;
        }

        public List<Particle> Generate(int count, float totalTime)
        {
            Particle origin = new Particle(descriptor.Position, Vector2.Zero, descriptor.StartColor, descriptor.EndColor, descriptor.MinLifetime + descriptor.LifetimeVariation);
            List<Particle> oryginalStream = new List<Particle>();
            oryginalStream.Add(origin);
            oryginalStream.AddRange(underlying.Generate(count, totalTime));

            oryginalStream.Sort((x1, x2) => { return (x1.Velocity.Rotation.Radians < x2.Velocity.Rotation.Radians) ? -1 : 1; });

            return oryginalStream;
        }
    }
}
