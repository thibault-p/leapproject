using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glitch.Engine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Glitch.Engine.Content;
using RopeMaster.gameplay.Enemies;

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
        private Rectangle srcFire;


        public ShotManager()
        {
            playerShots = new List<Shot>();
            srcRectPlayer = new Rectangle(0, 0, 10, 10);
            srcFire = new Rectangle(0, 10, 16, 16);
            ennemiesShots = new List<Shot>();
        }



        public void Initialize()
        {
            texture = Game1.Instance.magicContentManager.GetTexture("shotsheet");

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
            Rectangle r = Game1.Instance.Camera.ScreenVisible;

            foreach (Shot s in playerShots)
            {
                s.Update(gameTime);
                if (!r.Contains((int)s.getPosition().X, (int)s.getPosition().Y))
                {
                    s.Exterminate = true;
                }
                else
                {
                    foreach (Enemy e in Game1.Instance.enemyManager.enemies)
                    {

                        if (e.collideWith(s.getPosition(), 5))
                        {
                            e.hit(9000);
                            s.Exterminate = true;
                        }
                    }


                }

            }
            playerShots.RemoveAll(c => c.Exterminate);


            foreach (Shot s in ennemiesShots)
            {
                s.Update(gameTime);
                if (!r.Contains((int)s.getPosition().X, (int)s.getPosition().Y))
                {
                    s.Exterminate = true;
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
