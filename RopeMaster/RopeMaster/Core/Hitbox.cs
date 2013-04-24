using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RopeMaster.Core
{
    public abstract class Hitbox
    {


        protected Vector2 position;

        public Hitbox(Vector2 p)
        {
            position = p;
        }

        public virtual void setPosition(Vector2 pos)
        {
            position = pos;
        }


        public virtual Vector2 getPosition()
        {
            return position;
        }


        public abstract bool collide(Vector2 p2, int r);
        public abstract bool collide(Hitbox h);

    }
}
