using System.Collections.Generic;

namespace MEngine.Particles
{
    interface IParticleRenderer
    {
        void Redraw(List<Particle> particles);
    }
}
