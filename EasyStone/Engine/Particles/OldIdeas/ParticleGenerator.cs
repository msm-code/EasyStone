using System.Collections.Generic;
using System;
using Msm.Geometry;

namespace EasyStone.Engine.Particles
{
    abstract class ParticleGenerator
    {
        protected Random rgen;

        public ParticleGenerator()
        {
            rgen = new Random();
        }

        public abstract List<Particle> GenerateFirstParticles();
        public abstract List<Particle> UpdateAndGenerateNextParticles(float delta);
    }
}
