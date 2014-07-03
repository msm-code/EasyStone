using Msm.Geometry;
using System.Linq;
using EasyStone.Items;

namespace EasyStone
{
    enum HorisontalDir
    {
        None,
        Left,
        Right
    }

    enum VerticalDir
    {
        None,
        Up,
        Down
    }

    class Player : DynamicObject
    {
        private HorisontalDir hdir;
        private VerticalDir vdir;
        private Gun gun;
        private int lifes;

        public Player(Vector2 position, Map world)
            :base(position, world)
        {
            lifes = 5;
        }

        public void SetHorisontalDir(HorisontalDir dir)
        {
            this.hdir = dir;
        }

        public void SetVerticalDir(VerticalDir dir)
        {
            this.vdir = dir;
        }

        public void GiveGun(Gun gun)
        {
            if (this.gun == null || gun.GetType() != this.gun.GetType())
            {
                if (this.gun != null)
                {
                    if (this.gun.IsShooting)
                        gun.BeginShooting(this.gun.Target);
                    this.gun.EndShooting();
                }
                this.gun = gun;
            }
        }

        public Gun Gun
        { get { return gun; } }

        public override void Hit()
        {
            lifes -= 1;
            if (lifes <= 0)
            { base.Kill(); }

            GlobalInterface.AddInfoBox("They hits you! :/ " + lifes.ToString() + " hp left.");
        }

        protected override void Collision(DynamicObject other, Vector2 force)
        {
            other.Hit();
            base.Collision(other, force);
        }

        protected override void BeforeUpdate(float delta)
        {
            float xspeed = 0;
            float yspeed = 0;
            if (hdir == HorisontalDir.Left)
                xspeed = -10;
            else if (hdir == HorisontalDir.Right)
                xspeed = 10;
            if (vdir == VerticalDir.Down)
                yspeed = -10;
            else if (vdir == VerticalDir.Up)
                yspeed = 10;

            this.velocity = new Vector2(xspeed, yspeed);

            this.gun.TimeElsaped(delta);
        }

        public void PerformAction(float x, float y)
        {
            if (gun == null)
                return;

            //gun.Shoot(new Vector2(x, y));
        }

        protected override Color4 Color
        {
            get { return new Color4(0, (byte)(lifes * 50), 50); }
        }
    }
}
