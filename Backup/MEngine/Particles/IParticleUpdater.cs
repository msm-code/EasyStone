using System.Collections.Generic;
namespace MEngine.Particles
{
    interface IParticleUpdater
    {
        void Update(List<Particle> particles, float delta);
    }
}
