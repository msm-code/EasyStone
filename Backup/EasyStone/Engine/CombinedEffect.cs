namespace EasyStone.Engine
{
    class CombinedEffect : GraphicsEffect
    {
        GraphicsEffect[] effects;

        public CombinedEffect(params GraphicsEffect[] effects )
        {
            this.effects = effects;
        }

        public void Redraw()
        {
            foreach (GraphicsEffect effect in effects)
            {
                effect.Redraw();
            }
        }

        public bool Update(float delta)
        {
            bool done = true;

            foreach (GraphicsEffect effect in effects)
            {
                done &= effect.Update(delta);
            }

            return done;
        }

        public void Detach()
        {
            foreach (GraphicsEffect effect in effects)
            {
                effect.Detach();
            }
        }
    }
}
