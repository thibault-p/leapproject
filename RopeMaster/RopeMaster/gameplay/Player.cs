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
    [TextureContent(AssetName = "point", AssetPath = "gfx/point")]
    public class Player : Entity
    {
        private Rectangle source;
        private Texture2D texture;
        private Vector2 smokeLoc = new Vector2(-38, 23);
        private long time;
        private int burst = 0;
        private bool iscatched = false;
        private Radar radar;
        protected Hitbox hitboxbig;



        private VerletRope rope;

        public Player()
            : base()
        {
            texture = Game1.Instance.magicContentManager.GetTexture("player");
            source = new Rectangle(0, 0, 92, 128);
            time = 0;
            radar = new Radar();
            rope = new VerletRope(20, 200, Vector2.Zero, Game1.Instance.magicContentManager.GetTexture("point"));
            rope.setOrigin(40, 46);
            origin = new Vector2(40, 30);
            hitboxbig = new SphereBox(this.position, 25);
        }


        public void setIsCatched(bool state)
        {
            if (state != iscatched)
                radar.resetAnim();

            this.iscatched = state;
        }

        public override void setPosition(int x, int y)
        {
            base.setPosition(x, y);
            radar.setPosition(x, y);
            hitboxbig.setPosition(this.position);
        }

        public Vector2 getShotSource()
        {
            return rope.getAttachPosition();
        }

        public Vector2 getShotAngle()
        {
            return rope.getAttachAngle();
        }

        public override void Update(GameTime gameTime)
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

            rope.setOrigin((int)this.position.X, (int)this.position.Y + 36);
            rope.Update(gameTime);


        }



        public int getPointShot()
        {
            float p = 0;
            var d = rope.getOrigin().Y - rope.getAttachPosition().Y;
            if (d > 0)
                p =d/rope.length;
            Console.WriteLine(100*p);
            return (int)(100 * p)+1;
        }




        public void steerRope(int way)
        {
            burst = 1;
            if (way < 0)
                rope.up();
            else
                rope.down();
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (!iscatched)
            {
                radar.Draw(spriteBatch);

            }
            rope.Draw(spriteBatch);
            spriteBatch.Draw(texture, this.getPosition(), source, Color.White, 0, origin, 1, SpriteEffects.None, 0);
        }
    }
}
