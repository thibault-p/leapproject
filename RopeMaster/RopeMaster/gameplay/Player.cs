using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RopeMaster.Core;
using Glitch.Engine.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Glitch.Graphics.particules;

namespace RopeMaster.gameplay
{
    [TextureContent(AssetName = "player", AssetPath = "gfx/sprites/Player1")]
    public class Player : Entity
    {
        private Rectangle source;
        private Texture2D texture;
        private Vector2 smokeLoc = new Vector2(2, 53);
        private long time;
        private int burst = 0;
        private bool iscatched= false;
        private Radar radar;


        public Player()
            : base()
        {
            texture = Game1.Instance.magicContentManager.GetTexture("player");
            source = new Rectangle(0, 0, 92, 128);
            time = 0;
            radar = new Radar();
        }


        public void setIsCatched(bool state)
        {
            if (state != iscatched)
                radar.resetAnim();

            this.iscatched = state;
        }

        public void setPosition(int x, int y)
        {
            base.setPosition(x, y);
            radar.setPosition(x+40, y+30);
        }



        public void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.Milliseconds;
            if (time > 300)
            {
                var loc = this.getPosition() + smokeLoc;
                var rand = Game1.Instance.randomizator;
                Game1.Instance.particuleManager.AddParticule(new Smoke(loc, rand.GetRandomTrajectory(200, MathHelper.ToRadians(180), MathHelper.ToRadians(190)), rand.GetRandomFloat(0.4f, 0.7f), Color.White, false));
                if (burst > 0)
                {
                    for (int i = 0; i < rand.GetRandomInt(5, 10); i++)
                    {
                        Game1.Instance.particuleManager.AddParticule(new Smoke(loc, rand.GetRandomTrajectory(200, MathHelper.ToRadians(180), MathHelper.ToRadians(190)), rand.GetRandomFloat(0.4f, 0.7f), Color.White, false));
                    }
                    burst--;
                }

                time = 0;
            }
            if (!iscatched)
            {
                radar.Update(gameTime);
            }
        }


        public void steerRope()
        {
            burst = 1;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (!iscatched)
            {
                radar.Draw(spriteBatch);
            }

            spriteBatch.Draw(texture, this.getPosition(), source, Color.White);
        }
    }
}
