using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Msm.Geometry;

namespace EasyStone.Engine.Particles
{
    class ControllableGenerator : ParticleStreamGenerator
    {
        private ParticleStreamGenerator underlying;
        private Vector2 systemPosition;
        private Angle rotation;
        private float angleVariation;

        public ControllableGenerator(ParticleStreamGenerator underlying, float angleVariation)
        {
            this.underlying = underlying;
            this.angleVariation = angleVariation;
        }

        public void SetPosition(Vector2 value)
        {
            this.systemPosition = value;
        }

        public void SetRotation(Angle value)
        {
            this.rotation = value;
        }

        public List<Particle> Generate(int count, float totalTime)
        {
            List<Particle> oryginalStream = underlying.Generate(count, totalTime);

            Angle minRotation = Angle.FromDegrees(rotation.Degree - angleVariation / 2);
            Angle maxRotation = Angle.FromDegrees(rotation.Degree + angleVariation / 2);

            foreach (Particle oryginal in oryginalStream)
            {
                oryginal.Position += systemPosition;

                Angle angleDelta = oryginal.Velocity.Rotation - minRotation;
                angleDelta.Degree = angleDelta.Degree % angleVariation;

                oryginal.Velocity.Rotation = minRotation + angleDelta;
            }

            return oryginalStream;
        }
    }
}
