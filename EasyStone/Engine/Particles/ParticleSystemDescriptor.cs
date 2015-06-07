using Msm.Geometry;

namespace EasyStone.Engine.Particles
{
    struct ParticleSystemDescriptor
    {
        public Vector2 Position { get; set; }
        public float MinVelocity { get; set; }
        public float VelocityVariation { get; set; }
        public float MinLifetime { get; set; }
        public float LifetimeVariation { get; set; }

        public Color4 StartColor { get; set; }
        public Color4 EndColor { get; set; }
    }
}
