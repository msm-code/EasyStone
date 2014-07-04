using System.Collections.Generic;

namespace MEngine.Particles
{
    class ParticleSystem
    {
        List<Particle> particles;
        Vector2 systemPosition;

        IParticleEmitter emitter;
        ParticleUpdater updater;
        IParticleRenderer renderer;

        public ParticleSystem(Vector2 position, IParticleEmitter emitter, IParticleRenderer renderer)
        {
            this.particles = new List<Particle>();
            this.systemPosition = position;

            this.emitter = emitter;
            this.updater = new ParticleUpdater();
            this.renderer = renderer;
        }

        public void Update(float delta)
        {
            updater.Update(particles, delta);
        }

        public void Render()
        {
            renderer.Redraw(particles);
        }
    }
}
