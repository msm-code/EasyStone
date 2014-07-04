using Msm.Geometry;
using EasyStone.Engine;

namespace EasyStone
{
    class StrongEnemyArmor : DynamicObject
    {
        private int lives;
        private Angle rotation;

        public StrongEnemyArmor(Vector2 position, Map world)
            :base(position, world)
        {
            this.lives = 10;
        }

        public override void Hit()
        {
            this.lives--;
            if (lives <= 0)
                base.Kill();
        }

        public void SetPosition(Vector2 value)
        {
            this.position = value;
        }

        public void SetRotation(Angle rotation)
        {
            this.rotation = rotation;
        }

        protected override Color4 Color
        {
            get { return new Color4(255, 255, 255, 255); }
        }

        protected override float ObjectSize
        { get { return 1; } }

        protected override ShapeType ShapeType
        { get { return ShapeType.Armor; } }

        protected override int Texture
        { get { return TextureRepository.Instance.GetTexture("metal"); } }

        protected override float Rotation
        { get { return (float)rotation.Degree; } }
    }
}
