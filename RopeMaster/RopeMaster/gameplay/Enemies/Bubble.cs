using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Glitch.Engine.Content;
using Microsoft.Xna.Framework;
using RopeMaster.Core;

namespace RopeMaster.gameplay.Enemies
{

    [TextureContent(AssetName = "bubble", AssetPath = "gfx/sprites/bubble")]
    public class Bubble : Enemy
    {
        private int state;
        private Texture2D texture;
        private int maxX;
        private Color color;
        private bool moving;
        private long timerhit;
        private bool droping = false;

        public Bubble(Vector2 pos, int maxX, Vector2 dir)
            : base()
        {
            Initialyze(pos,dir,maxX);

        }

        public Bubble()
            : base()
        {
            Initialyze(Vector2.Zero,Vector2.Zero, 0);

        }

        public void drop(float speed)
        {
            velocity = Vector2.UnitY * speed;
            droping = true;
        }

        protected void Initialyze(Vector2 pos , Vector2 dir, int maxX)
        {
            texture = Game1.Instance.magicContentManager.GetTexture("bubble");
            state = 0;
            this.position = pos;
            this.velocity = dir;
            this.maxX = maxX;
            srcBox = new Rectangle(0, 0, 64, 64);
            this.origin = new Vector2(srcBox.Width / 2, srcBox.Height / 2);
            color = Color.White;
            this.hitbox = new SphereBox(this.origin + position, 32);
            timerhit = 0;
            this.moving = true;
        }
        public override void Update(GameTime gameTime)
        {
            if (droping)
            {
                base.Update(gameTime);
                this.hitbox.setPosition(this.position + origin);

            }
            else if (moving)
            {
                base.Update(gameTime);
                if (position.X <= 32)
                {
                    this.moving = false;
                    this.velocity = Vector2.Zero;
                    position.Y = (int)Math.Floor(position.Y / 64) * 64 + 32;
                    this.hitbox.setPosition(this.position + origin);
                    return;
                }
                foreach (Enemy e in Gamescreen.Instance.enemyManager.enemies)
                {

                    if (e is Bubble)
                    {
                        var b = (Bubble)e;
                        if (!b.moving && this.hitbox.collide(b.getHitBox()))
                        {
                            this.moving = false;
                            this.velocity = Vector2.Zero;
                            //Console.WriteLine("colide");
                            this.position.X = e.getPosition().X + ((e.getPosition().X<=position.X) ? 1 : 0) * 54;


                            position.Y = e.getPosition().Y+ ((position.Y>=e.getPosition().Y)?32:-32);
                            this.hitbox.setPosition(this.position + origin);
                            return;
                        }
                        
                    }
                   

                }
                if (position.Y <= 32 || position.Y >= (Game1.Instance.Screen.Height)) velocity.Y *= -1;
            }

           
            

        }



        public override bool exterminate()
        {
            return position.Y - 32 > Game1.Instance.Screen.Height;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, this.position, srcBox, color, 0, origin, 1, SpriteEffects.None, 0);
        }



    }
}
