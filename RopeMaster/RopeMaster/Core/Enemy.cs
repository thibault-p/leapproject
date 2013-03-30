using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RopeMaster.gameplay.Helpers;

namespace RopeMaster.Core
{
    public abstract class Enemy : Entity
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
        protected Texture2D texture;

        public Enemy(Vector2 _pos, int _hp)
            : base(_pos, Vector2.Zero)
        {
            hp = Game1.Instance.difficulty * _hp;
            d = 1;
            currentAnim = Anim.idle;
        }

        public void setTrajectory(Func<float, float> traj)
        {
            trajectory = traj;
        }


        public override bool exterminate()
        {
            if (position.X < -2 * srcBox.Width || position.X > Game1.Instance.Screen.Width + 2 * srcBox.Width)
                return true;
            return ((hp <= 0) && ready2Die);

        }

        public bool Exterminate()
        {
            return this.exterminate();
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
            var x = position.X / Game1.Instance.Screen.Width;
            var y = trajectory.Invoke(x);
            position.Y = y * Game1.Instance.Screen.Height;
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

        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, srcBox, Color.White);
        }

    }
}
