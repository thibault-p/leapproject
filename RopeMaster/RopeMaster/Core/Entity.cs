using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RopeMaster.Core
{
    public abstract class Entity
    {
        protected Vector2 position;
        protected Vector2 velocity;
        protected Vector2 origin;


        public Entity()
        {
            initialyze(Vector2.Zero, Vector2.Zero);
        }


        public Entity(Vector2 pos, Vector2 velo)
        {
            this.initialyze(pos, velo);
        }

        private void initialyze(Vector2 pos, Vector2 velo)
        {
            this.position = pos;
            this.velocity = velo*400 ;
            this.origin = Vector2.Zero;
        }

        public virtual void Update(GameTime gameTime)
        {
            var d = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            position += velocity * d ;
        }

         public virtual Vector2 getPosition()
        {
            return position;
        }

         public virtual void setPosition(int x, int y)
        {
            this.position.X = x;
            this.position.Y = y;
        }

         public virtual void setPosition(Vector2 pos)
         {
             this.position = pos;
         }

         public Vector2 getOrigin()
         {
             return origin;
         }

         public virtual void Draw(SpriteBatch spritebatch)
         {

         }

        public virtual bool exterminate()
        {
            return false;
        }


    }
}
