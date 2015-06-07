using Msm.Geometry;
using EasyStone.Bullets;

namespace EasyStone
{
    class GunnerEnemy : EnemyBase
    {
        public GunnerEnemy(Vector2 position, Map world)
            : base(position, world) { totalLifetime = 0; }
        
        protected override float Speed
        {
            get { return 25; }
        }

        protected override void BeforeUpdate(float delta)
        {
            base.BeforeUpdate(delta);

            if ((int)(totalLifetime) < (int)(totalLifetime + delta))
            {
                Vector2 bulletVelocity = velocity;
                bulletVelocity.Length = 15;
                world.Add(new SimpleBullet(Position, bulletVelocity, this, world));
            }

            this.velocity.Rotation += Angle.FromDegrees(75);
        }

        protected override Color4 Color
        {
            get { return new Color4(200, 0, 150); }
        }

        protected override EasyStone.Engine.ShapeType ShapeType
        { get { return EasyStone.Engine.ShapeType.Ship; } }
    }
}
