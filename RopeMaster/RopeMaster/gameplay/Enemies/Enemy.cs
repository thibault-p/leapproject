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
    public class Enemy : Entity
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



        public Enemy()
            : base(Vector2.Zero, Vector2.Zero)
        {
            initialize(1);
        }



        public Enemy(Vector2 _pos, int _hp)
            : base(_pos, Vector2.Zero)
        {
            initialize(_hp);
        }

        private void initialize(int _hp)
        {
            hp = Game1.Instance.difficulty * _hp;
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

        public bool collideWith(Vector2 v, int r)
        {
            return hitbox.collide(v, r);
        }


        public bool Exterminate()
        {
            return this.exterminate();
        }


        public virtual void hit(int damage)
        {
            this.hp -= damage;
            if (this.hp <= 0) this.currentAnim = Anim.die;
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

            
            if(hitbox!=null)
                hitbox.setPosition(this.position);
        }

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture1, position, srcBox, Color.White, rotation, origin, 1, SpriteEffects.None, 0);
        }
    }
}
