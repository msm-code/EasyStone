using System.Collections.Generic;

namespace EasyStone.Engine.Particles
{
    abstract class ParticleRenderer
    {
        public abstract void Render(List<Particle> particles);
    }
}
