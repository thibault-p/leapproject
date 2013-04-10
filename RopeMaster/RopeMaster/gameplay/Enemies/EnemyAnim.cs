using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RopeMaster.gameplay.Helpers;
using RopeMaster.Core;

namespace RopeMaster.gameplay.Enemies
{
    public class EnemyAnim : Enemy
    {

        public static long animRate = 100;

        protected enum Anim { idle = 0, fire, die };


        protected int hp;
        protected float d;
        protected Func<float, float> trajectory;
        protected Rectangle srcBox;
        protected Anim currentAnim;
        protected int[] nbFrame;
        protected int currentFrame;
        private bool ready2Die = false;
        private long timer;
        protected Texture2D texture1;
        protected Hitbox hitbox;
        protected float rotation;



        public EnemyAnim()
            : base(Vector2.Zero, 1)
        {
            initialize();
        }



        public EnemyAnim(Vector2 _pos, int _hp)
            : base(_pos, _hp)
        {
            initialize();
        }

        private void initialize()
        {
            d = 1;
            currentAnim = Anim.idle;
            rotation = 0;
        }




        public override bool exterminate()
        {
            if (position.X < -2 * srcBox.Width || position.X > Game1.Instance.Screen.Width + 2 * srcBox.Width)
                return true;
            return ((hp <= 0) && ready2Die);

        }

        public bool collideWith(Vector2 v, int r)
        {
            return hitbox.collide(v, r);
        }


        public bool Exterminate()
        {
            return this.exterminate();
        }



        public override void Update(GameTime gameTime)
        {
            var prev = this.position;
            base.Update(gameTime);
            if (trajectory != null)
            {
                var x = position.X / Game1.Instance.Screen.Width;
                var y = trajectory.Invoke(x);
                position.Y = y * Game1.Instance.Screen.Height;
            }
            var d = position - prev;
            rotation =(float)Math.Atan2(d.Y, d.X);

            //animation
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer >= animRate)
            {
                timer = 0;
                currentFrame++;
                if (currentFrame >= nbFrame[(int)currentAnim])
                {
                    if (currentAnim == Anim.idle)
                    {
                        currentFrame = 0; //*loop
                    }
                    else if (currentAnim == Anim.fire)
                    {
                        currentFrame = 0;
                        currentAnim = Anim.idle;
                    }
                    else
                    {
                        ready2Die = true;
                    }
                }
                srcBox.X = srcBox.Width * currentFrame;
                srcBox.Y = srcBox.Height * (int)currentAnim;
            }
            if(hitbox!=null)
                hitbox.setPosition(this.position);
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture1, position, srcBox, Color.White, rotation, origin, 1, SpriteEffects.None, 0);
        }
    }
}
