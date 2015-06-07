using Msm.Geometry;
using System.Collections.Generic;
using EasyStone.Engine;
namespace EasyStone
{
    class StrongEnemy : EnemyBase
    {
        List<StrongEnemyArmor> armor;

        public StrongEnemy(Vector2 position, Map world)
            :base(position, world)
        {
            this.lives = 10;
            this.armor = new List<StrongEnemyArmor>();

            for (int i = 0; i < 8; i++)
            {
                this.armor.Add(new StrongEnemyArmor(this.position, world));
                this.world.Add(armor[i]);
            }
        }

        protected override void AfterUpdate(float delta)
        {
            for (int i = 0; i < armor.Count; i++)
            {
                Angle angle = Angle.FromDegrees(360 * (i / (float)armor.Count)) + this.velocity.Rotation;
                Vector2 offset = Vector2.FromRotationAndLength(angle, this.ObjectSize / 1.6f);
                armor[i].SetPosition(this.position + offset);
                armor[i].SetRotation(angle);
            }

            base.AfterUpdate(delta);
        }

        protected override float ObjectSize
        { get { return 1.5f; } }

        protected override float Speed
        {
            get { return 5f; }
        }

        protected override EasyStone.Engine.ShapeType ShapeType
        { get { return EasyStone.Engine.ShapeType.Octagon; } }

        protected override int Texture
        { get { return TextureRepository.Instance.GetTexture("metal"); } }

        protected override Color4 Color
        {
            get { return new Color4(255, 255, 255, 255); }
        }
    }
}
