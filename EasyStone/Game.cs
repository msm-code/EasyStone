using Msm.Geometry;
using Tao.FreeGlut;
using System;
using EasyStone.Items;
using EasyStone.Waves;
using EasyStone.Enemy;
using EasyStone.Engine.Particles;
using EasyStone.Engine;
using Tao.OpenGl;

namespace EasyStone
{
    class Game : IGameState
    {
        Map world;
        Player player;
        float totalTime;
        PlayerMovesCollection moves;
        EnemyManager2 enemyManager;

        public Game(Map world)
        {
            this.world = world;
            this.player = new Player(new Vector2(10, 0), world);
            this.world.Add(player);
            this.moves = new PlayerMovesCollection();
            this.enemyManager = new EnemyManager2(world);

            this.moves.UnlockedMove += (sender, e) =>
            {
                GlobalInterface.AddInfoBox(e.MoveName + " unlocked!");
            };

            this.player.GiveGun(new Pistol(world, player));

            this.player.Killed += (sender, e) =>
            {
                GlobalInterface.AddInfoBox("You are dead :<");

                if (GameStateChanged != null)
                {
                    var splash = new SplashScreen(
                        "Game over\nVanquished " + SessionStatistics.KillCount + " Enemies",
                        new Color4(255, 50, 50, 255));
                    GameStateChanged(this, new GameStateChangedEventArgs(splash));
                }
            };

            TextureRepository.Instance.LoadTexture(@"textures\smoke.png", "smoke");
            TextureRepository.Instance.LoadTexture(@"textures\fire.png", "fire");
            TextureRepository.Instance.LoadTexture(@"textures\white.png", "white");
            TextureRepository.Instance.LoadTexture(@"textures\particle-star.png", "particle-star");
            TextureRepository.Instance.LoadTexture(@"textures\bullet.png", "bullet");
            TextureRepository.Instance.LoadTexture(@"textures\metal_test.jpg", "metal");

            GlobalInterface.AddInfoBox("Game started");
        }

        private unsafe void SpawnWave()
        {
            int oldWave = enemyManager.WaveNumber;

            enemyManager.SpawnNextWave();

            if (oldWave != enemyManager.WaveNumber)
            {
                GlobalInterface.AddInfoBox("Wave " + enemyManager.WaveNumber + " incoming!");

                Glut.glutSetIconTitle("Easy Stone Survival - wave " + enemyManager.WaveNumber);

                if (enemyManager.WaveNumber % 5 == 0)
                    moves.UnlockNext();
            }
        }

        public void Update(float delta)
        {
            if (enemyManager.Victory)
            {
                GlobalInterface.AddInfoBox("Victory!");
                GlobalInterface.AddInfoBox("Good Job");
                var msg = "Victory!\nVanquished " + SessionStatistics.KillCount + " Enemies";
                var splash = new SplashScreen(msg, new Color4(50, 255, 50, 255));
                GameStateChanged(this, new GameStateChangedEventArgs(splash));
            }

            enemyManager.Update(delta);
            if (enemyManager.NextWaveReady)
            {
                SpawnWave();
            }

            totalTime += delta;

            world.Update(delta);
        }

        public void Redraw()
        {
            world.Redraw();
        }

        public void KeyDown(byte code)
        {
            if (code == 'a')
                player.SetHorisontalDir(HorisontalDir.Left);
            else if (code == 'd')
                player.SetHorisontalDir(HorisontalDir.Right);
            else if (code == 'w')
                player.SetVerticalDir(VerticalDir.Up);
            else if (code == 's')
                player.SetVerticalDir(VerticalDir.Down);

            else if (code == '1')
                player.GiveGun(new Pistol(world, player));
            else if (code == '2' && moves.IsUnlocked(MoveTypes.MachineGun))
                player.GiveGun(new MachineGun(world, player));
            else if (code == '3' && moves.IsUnlocked(MoveTypes.Shotgun))
                player.GiveGun(new Shotgun(world, player));
            else if (code == '4' && moves.IsUnlocked(MoveTypes.GrenadeLauncher))
                player.GiveGun(new GrenadeLauncher(world, player));
            else if (code == '5' && moves.IsUnlocked(MoveTypes.Aura))
                player.GiveGun(new Aura(world, player, 90));
            else if (code == '6' && moves.IsUnlocked(MoveTypes.Venom))
                player.GiveGun(new Venom(world, player));
            else if (code == '0' && moves.IsUnlocked(MoveTypes.Artifact19))
                player.GiveGun(new Artifact19(world, player));

            // codes (temporary :D )
            else if (code == '\\')
                for (int i = 0; i < 100; i++)
                    moves.UnlockNext();
        }

        public void KeyUp(byte code)
        {   
            if (code == 'a' || code == 'd')
                player.SetHorisontalDir(HorisontalDir.None);
            if (code == 's' || code == 'w')
                player.SetVerticalDir(VerticalDir.None);
        }

        private int mouseX;
        private int mouseY;
        public void MouseClick(int button, int state, int x, int y)
        {
            Vector2 position = Camera.ScreenToGl(x, y);

            if (button == Glut.GLUT_LEFT)
            {
                if (state == Glut.GLUT_DOWN)
                {
                    mouseX = x;
                    mouseY = y;
                    Vector2 target = Camera.ScreenToGl(mouseX, mouseY);
                    player.Gun.BeginShooting(target);
                }
                else if (state == Glut.GLUT_UP)
                {
                    player.Gun.EndShooting();
                }
            }
        }

        public void MouseMove(int x, int y)
        {
            mouseX = x;
            mouseY = y;
            Vector2 target = Camera.ScreenToGl(mouseX, mouseY);
            player.Gun.Target = target;
        }

        public event EventHandler<GameStateChangedEventArgs> GameStateChanged;
    }
}