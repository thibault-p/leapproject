using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RopeMaster.Core
{
    public abstract class Entity
    {
        protected Vector2 position;
        protected Vector2 velocity;



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
            this.velocity = velo * 500;
        }

         public void Update(GameTime gameTime)
        {
            var d = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            position += velocity * d;
        }

         public Vector2 getPosition()
        {
            return position;
        }

         public void setPosition(int x, int y)
        {
            this.position.X = x;
            this.position.Y = y;
        }


        public bool exterminate()
        {
            return false;
        }


    }
}
