using Msm.Geometry;
using System.Linq;
using EasyStone.Engine.Particles;

namespace EasyStone
{
    abstract class EnemyBase: DynamicObject
    {
        protected float totalLifetime;
        protected int lives;

        public EnemyBase(Vector2 position, Map world)
            : base(position, world)
        {
            totalLifetime = 0;
            lives = 1;
        }

        protected abstract float Speed { get; }

        public override void Hit()
        {
            this.lives -= 1;

            if (lives <= 0)
            { base.Kill(); }
        }

        protected override void OnKilled()
        {
            SessionStatistics.EnemyKilled(this);

            world.AddEffect(PredefinedEffects.UnitKilled(Position, Color, ObjectSize, velocity.Length));

            base.OnKilled();
        }

        protected override void BeforeUpdate(float delta)
        {
            totalLifetime += delta;

            DynamicObject nearest = world.Units
                .OfType<Player>()
                .OrderBy(x => (Position - this.Position).Length)
                .FirstOrDefault();

            if (nearest == null)
            {
                this.velocity = Vector2.Zero;
                return;
            }

            Vector2 velocity = nearest.Position - this.Position;

            if ((nearest.Position - this.Position).Length < 0.1)
            { this.velocity = Vector2.Zero; return; }

            velocity.Length = Speed;

            this.velocity = velocity;
        }

        protected override void Collision(DynamicObject other, Vector2 force)
        {
            if (other.GetType() != typeof(Player)) return;

            other.Hit();
            base.Collision(other, force);
        }

        protected virtual int MaxLives { get { return 1; } }
    }
}
