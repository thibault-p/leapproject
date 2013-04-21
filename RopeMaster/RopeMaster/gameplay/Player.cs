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
        private Rectangle sourcep1;
        private Rectangle sourcep2;
        private Rectangle srcWing;
        private Vector2 poswing;
        private Texture2D texture;
        private Vector2 smokeLoc = new Vector2(-38, 23);
        private long time, wingtime;
        private int burst = 0;
        private bool iscatched = false;
        private Radar radar;
        protected Hitbox hitboxbig;
        private Vector2 originp2;
        private Vector2 gunsrc;

        private VerletRope rope;

        public Player()
            : base()
        {

            texture = Game1.Instance.magicContentManager.GetTexture("player");
            sourcep1 = new Rectangle(0, 0, 80, 128);
            sourcep2 = new Rectangle(0, 128, 92, 128);
            time = 0;
            radar = new Radar();
            rope = new VerletRope(20, 200, Vector2.Zero, Game1.Instance.magicContentManager.GetTexture("point"));
            rope.setOrigin(40, 46);
            origin = new Vector2(40, 30);
            originp2 = new Vector2(29, 11);
            gunsrc = new Vector2(12, 50);
            hitboxbig = new SphereBox(this.position, 25);
            poswing = new Vector2(-20, -2);
            srcWing = new Rectangle(80, 0, 27, 18);

            this.setPosition(50, 50);
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
            var r = rope.getAttachAngleF();
            Vector2 v = gunsrc;
            v.X *= (float)Math.Sin(r);
            v.Y *= (float)Math.Cos(r);
            return rope.getAttachPosition() + v;
        }



        public Vector2 getShotAngle()
        {
            return rope.getAttachAngle();
        }

        public override void Update(GameTime gameTime)
        {
            time += gameTime.ElapsedGameTime.Milliseconds;
            wingtime += gameTime.ElapsedGameTime.Milliseconds;
            if (wingtime > 150)
            {
                wingtime = 0;
                srcWing.X += 27;
                if (srcWing.X > 161) srcWing.X = 80;
            }


            if (time > 300)
            {
                var loc = this.getPosition() + smokeLoc;
                var rand = Game1.Instance.randomizator;
                Gamescreen.Instance.particuleManager.AddParticule(new Smoke(loc, rand.GetRandomTrajectory(200, MathHelper.ToRadians(180), MathHelper.ToRadians(190)), rand.GetRandomFloat(0.4f, 0.7f), Color.White, false));
                if (burst > 0)
                {
                    for (int i = 0; i < rand.GetRandomInt(5, 10); i++)
                    {
                        Gamescreen.Instance.particuleManager.AddParticule(new Smoke(loc, rand.GetRandomTrajectory(200, MathHelper.ToRadians(180), MathHelper.ToRadians(190)), rand.GetRandomFloat(0.4f, 0.7f), Color.White, false));
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
                p = d / rope.length;
            return (int)(100 * p) + 1;
        }






        public bool steerRope(int way)
        {
            var l = rope.fixeddiv;
            burst = 1;
            if (way < 0)
                rope.up();
            else
                rope.down();
            return l != rope.fixeddiv;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!iscatched)
            {
                radar.Draw(spriteBatch);

            }
            var rotation = rope.getAttachAngle();
            rope.Draw(spriteBatch);
            spriteBatch.Draw(texture, this.getPosition(), sourcep1, Color.White, 0, origin, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(texture, position + poswing, srcWing, Color.White);
            spriteBatch.Draw(texture, this.rope.getAttachPosition(), sourcep2, Color.White, rope.getAttachAngleF(), originp2, 1, SpriteEffects.None, 0);
        }
    }
}
