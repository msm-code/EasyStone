using System;
using EasyStone.Engine;
using Tao.FreeGlut;
using Msm.Geometry;
using EasyStone.Menus;
using EasyStone.Engine.Particles;
using System.Collections.Generic;
namespace EasyStone
{
    class SplashScreen : IGameState
    {
        string text;
        Color4 mainColor;
        float totalLife;
        List<GraphicsEffect> effects;
        Random rand;

        public SplashScreen(string text, Color4 mainColor)
        {
            this.text = text;
            this.mainColor = mainColor;
            this.effects = new List<GraphicsEffect>();
            this.rand = new Random();
        }

        public void Update(float delta)
        {
            if ((int)(totalLife * 10) < (int)((totalLife + delta) * 10))
            {
                effects.Add(CreateRandomFirework());
            }

            foreach (var effect in effects)
            {
                effect.Update(delta);
            }

            totalLife += delta;
        }

        GraphicsEffect CreateRandomFirework()
        {
            ParticleSystemDescriptor descriptor = new ParticleSystemDescriptor();

            var size = Renderer.Instance.ScreenSize;
            var pos = new Vector2(
                (float)(rand.NextDouble() - 0.5f) * size.X,
                (float)(rand.NextDouble() - 0.5f) * size.Y);
            var endColor = new Color4(
                (byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255), 0);
            descriptor.Position = pos;
            descriptor.StartColor = mainColor;
            descriptor.EndColor = endColor;
            descriptor.MinVelocity = (float)rand.NextDouble() * 2.0f;
            descriptor.VelocityVariation = (float)rand.NextDouble() * 10;
            descriptor.MinLifetime = (float)rand.NextDouble() * 3f;
            descriptor.LifetimeVariation = (float)rand.NextDouble() * 4;

            var count = rand.NextDouble() > 0.9 ? 1000 : 250;
            return ParticleSystemFactory.CreateExplosion(descriptor, count);
        }

        public void Redraw()
        {
            foreach (var effect in effects)
            {
                effect.Redraw();
            }

            Renderer.Instance.BlitText(-0.3f, -0.3f, Glut.GLUT_BITMAP_TIMES_ROMAN_24, text, Color4.Black);
        }

        public void KeyDown(byte code)
        {
        }

        public void KeyUp(byte code)
        { }

        public void MouseClick(int button, int state, int x, int y)
        {
            if (totalLife > 1)
            {
                if (GameStateChanged != null)
                {
                    GameStateChanged(this, new GameStateChangedEventArgs(new Menu()));
                }
            }
        }

        public void MouseMove(int x, int y)
        { }

        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;
    }
}
