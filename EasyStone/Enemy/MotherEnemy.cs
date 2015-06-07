using Msm.Geometry;
namespace EasyStone
{
    class MotherEnemy : EnemyBase
    {
        public MotherEnemy(Vector2 position, Map world)
            : base(position, world) { lives = 35; }

        protected override float Speed
        {
            get { return 5; }
        }

        protected override void BeforeUpdate(float delta)
        {
            base.BeforeUpdate(delta);

            if ((int)(totalLifetime) / 2 < (int)(totalLifetime + delta) / 2)
            {
                world.Add(new SimpleEnemy(this.Position, world));
            }
        }

        protected override void OnKilled()
        {
            for (int i = 0; i < 5; i++)
            {
                world.Add(new SimpleEnemy(Position + new Vector2(0.2f * i, 0), world));
            }

            base.OnKilled();
        }

        protected override float ObjectSize
        { get { return 2.5f; } }

        protected override Color4 Color
        {
            get { return new Color4((byte)(15 * lives), 30, 60); }
        }
    }
}
