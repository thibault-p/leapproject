using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glitch.Engine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Glitch.Engine.Content;
using RopeMaster.gameplay.Enemies;
using RopeMaster.gameplay;
using Glitch.Graphics.particules;

namespace RopeMaster.Core
{
    [TextureContent(AssetName = "shotsheet", AssetPath = "gfx/shot")]
    public class ShotManager : Manager
    {

        private List<Shot> playerShots;
        private List<Shot> ennemiesShots;


        private float fireCooldown = 0;
        private float autofireCooldown = 0;

        private bool fire = false;
        private bool autofire = false;


        private Texture2D texture;
        private Rectangle srcRectPlayer;
        private Rectangle srcFire, srcPlasma;

        private Color[] mapCollshot;


        public ShotManager()
        {
            playerShots = new List<Shot>();
            srcRectPlayer = new Rectangle(0, 0, 13, 10);
            srcFire = new Rectangle(0, 10, 23, 23);
            srcPlasma = new Rectangle(0, 36, 64, 64);
            ennemiesShots = new List<Shot>();
            mapCollshot = new Color[srcRectPlayer.Width * srcRectPlayer.Height];
        }



        public void Initialize()
        {
            texture = Game1.Instance.magicContentManager.GetTexture("shotsheet");
            texture.GetData(0, srcRectPlayer, mapCollshot, 0, srcRectPlayer.Height * srcRectPlayer.Width);
        }


        public void Update(GameTime gameTime)
        {
            var d = gameTime.ElapsedGameTime.Milliseconds;
            fireCooldown += d;
            autofireCooldown += d;
            if (fireCooldown > 125)
            {
                fire = true;
                fireCooldown = 0;
            }
            if (autofireCooldown > 300)
            {
                autofire = true;
                autofireCooldown = 0;
            }
            Rectangle r = Gamescreen.Instance.camera.ScreenVisible;
            Rectangle hitb = new Rectangle(0, 0, 10, 10);

            foreach (Shot s in playerShots)
            {
                s.Update(gameTime);
                if (!r.Contains((int)s.getPosition().X, (int)s.getPosition().Y))
                {
                    s.Exterminate = true;
                }
                else
                {
                    foreach (Enemy e in Gamescreen.Instance.enemyManager.enemies)
                    {

                        if (e.collideWith(s.getPosition(), 5))
                        {
                            e.hit(9000);
                            s.Exterminate = true;
                        }
                        if (e is Gojira)
                        {
                            
                            var g = (Gojira)e;
                            float rot = 42;
                            Vector2 i_p=Vector2.Zero;
                            hitb.X = (int)s.getPosition().X - 5;
                            hitb.Y = (int)s.getPosition().Y - 5;
                            if (g.IsPointPerfectColliding(hitb, mapCollshot, ref rot, ref i_p))
                            {
                                s.Exterminate = true;
                                Gamescreen.Instance.stuffManager.Add(new Impact(i_p, rot));
                                Gamescreen.Instance.combobar.AddCombo(10, "Hit!");
                                
                            }
                            if (!g.dead && g.tonguebox != null)
                            {
                                if (g.tonguebox.collide(s.getPosition(), 5))
                                {
                                    g.hit(10);
                                    Gamescreen.Instance.combobar.AddCombo(20, "Painful Hit!");
                                    Gamescreen.Instance.gamemanager.score+=(Gamescreen.Instance.combobar.getMultiplicator() * 2000);
                                    s.Exterminate = true;
                                    Gamescreen.Instance.particuleManager.AddParticule(new Blood(s.getPosition(), Game1.Instance.randomizator.GetRandomTrajectory(200, MathHelper.ToRadians(180), MathHelper.ToRadians(190)), Game1.Instance.randomizator.GetRandomFloat(0.6f, 1f), Color.White, false));

                                }

                            }

                        }
                    }


                }

            }
            playerShots.RemoveAll(c => c.Exterminate);

            var big = (SphereBox) Gamescreen.Instance.player.getBigHitbox();
            var small = (SphereBox)Gamescreen.Instance.player.getSmallHitbox();
            var cancel = (SphereBox)Gamescreen.Instance.player.getCancelHitbox();
            foreach (Shot s in ennemiesShots)
            {
                s.Update(gameTime);
                var pos = s.getPosition();
                if (!r.Contains((int)pos.X, (int)pos.Y))
                {
                    s.Exterminate = true;
                }
                else
                {
                    if (big.collide(pos, s.shotWidth))
                    {
                        Gamescreen.Instance.player.KillBig();
                        s.Exterminate = true;
                        Gamescreen.Instance.combobar.combo = 0;
                    }
                    if (small.collide(pos, s.shotWidth))
                    {
                        Gamescreen.Instance.player.KillSmall();
                        s.Exterminate = true;
                        Gamescreen.Instance.combobar.combo = 0;
                    }
                    if (cancel.collide(pos, s.shotWidth))
                    {
                        s.Exterminate = true;
                        Gamescreen.Instance.combobar.AddCombo(5, "Cancel !");
                    }



                }
                


            }

            ennemiesShots.RemoveAll(c => c.Exterminate);


        }

        public void AddShotEnemy(Shot s)
        {
            ennemiesShots.Add(s);
        }



        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Shot s in playerShots)
            {
                spriteBatch.Draw(texture, s.getPosition(), srcRectPlayer, Color.White, 0f, s.getOrigin(), 1, SpriteEffects.None, 0f);
            }

            foreach (Shot s in ennemiesShots)
            {
                switch (s.type)
                {
                    case 0:
                        spriteBatch.Draw(texture, s.getPosition(), srcFire, Color.White, s.angle, s.getOrigin(), 1, SpriteEffects.None, 0f);
                        break;
                    case 1:
                        srcFire.X = srcFire.Width * s.current;
                        spriteBatch.Draw(texture, s.getPosition(), srcFire, Color.White, s.angle, s.getOrigin(), 1, SpriteEffects.None, 0f);
                        break;
                    case 2:
                        srcPlasma.X = srcPlasma.Width * s.current;
                        spriteBatch.Draw(texture, s.getPosition(), srcPlasma, Color.White, s.angle, s.getOrigin(), s.scale, SpriteEffects.None, 0f);
                        break;
                    default: Console.WriteLine("ohoh");
                        break;
                }
            }
        }


        public void playerFire(Vector2 p, Vector2 v, int dmg, int point)
        {
            if (fire)
            {
                fireShot(p, v, dmg, point, true);
                fire = autofire = false;
            }
        }

        public void playerAutoFire(Vector2 p, Vector2 v, int dmg, int point)
        {

            if (autofire)
            {
                fireShot(p, v, dmg, point, true);
                autofire = false;
            }
        }

        private void fireShot(Vector2 p, Vector2 v, int dmg, int point, bool player)
        {
            playerShots.Add(new Shot(p, v, dmg, point, player, 0, 1));

        }

    }
}
