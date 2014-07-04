using System.Collections.Generic;
namespace MEngine.Particles
{
    interface IParticleEmitter
    {
        List<Particle> Emit(int count);
    }
}
