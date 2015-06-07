using System.Collections.Generic;
using Msm.Geometry;
using System.Linq;

namespace EasyStone.Engine.Particles
{
    class FountainModyifer : ParticleStreamGenerator
    {
        private ParticleStreamGenerator underlying;
        private Angle rotation;
        private float angleVariation;

        public FountainModyifer(ParticleStreamGenerator underlying, Angle rotation, float angleVariation)
        {
            this.underlying = underlying;
            this.rotation = rotation;
            this.angleVariation = angleVariation;
        }

        public List<Particle> Generate(int count, float totalTime)
        {
            List<Particle> oryginalStream = underlying.Generate(count, totalTime);

            Angle minRotation = Angle.FromDegrees(rotation.Degree - angleVariation / 2);
            Angle maxRotation = Angle.FromDegrees(rotation.Degree + angleVariation / 2);

            foreach (Particle oryginal in oryginalStream)
            {
                Angle angleDelta = oryginal.Velocity.Rotation - minRotation;
                angleDelta.Degree = angleDelta.Degree % angleVariation;

                oryginal.Velocity.Rotation = minRotation + angleDelta;
            }

            return oryginalStream;
        }
    }
}
