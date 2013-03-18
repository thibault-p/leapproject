using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glitch.Engine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Glitch.Engine.Content;

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

        public ShotManager()
        {
            playerShots = new List<Shot>();
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

            }
            playerShots.RemoveAll(c => c.Exterminate);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Shot s in playerShots)
            {
                spriteBatch.Draw(texture, s.getPosition(), Color.White);


            }
        }


        public void playerFire(Vector2 p, Vector2 v, int dmg)
        {
            if (fire)
            {
                fireShot(p, v, dmg, true);
                fire = autofire = false;
            }
        }

        public void playerAutoFire(Vector2 p, Vector2 v, int dmg)
        {

            if (autofire)
            {
                fireShot(p, v, dmg, true);
                autofire = false;
            }
        }

        private void fireShot(Vector2 p, Vector2 v, int dmg, bool player)
        {
            playerShots.Add(new Shot(p, v, dmg, player));

        }

    }
}
