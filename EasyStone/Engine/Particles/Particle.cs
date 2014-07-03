using Msm.Geometry;
namespace EasyStone.Engine.Particles
{
    class Particle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public bool Alive;

        private float maxLife;
        private float left;
        private Color4 startingColor;
        private Color4 endingColor;

        public Particle(Vector2 position, Vector2 velocity, Color4 starting, Color4 ending, float life)
        {
            this.maxLife = life;
            this.left = life;
            this.Position = position;
            this.Velocity = velocity;
            this.startingColor = starting;
            this.endingColor = ending;
            this.Alive = true;
        }

        public virtual void Update(float delta)
        {
            this.Position += Velocity * delta;

            this.left -= delta;

            if (this.left < 0)
                this.Alive = false;
        }

        public Color4 StartColor
        { get { return startingColor; } }

        public Color4 EndColor
        { get { return endingColor; } }

        public Color4 Color
        {
            get { return Color4.Blend(startingColor, endingColor, left / maxLife); }
        }

        public float LifeFactor
        {
            get { return left / maxLife; }
        }
    }
}
