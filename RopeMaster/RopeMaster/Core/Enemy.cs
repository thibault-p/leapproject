using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RopeMaster.Core
{
    public abstract class Enemy : Entity
    {
        protected int hp;

        public Enemy(int _hp):base()
        {
            hp = Game1.Instance.difficulty * _hp;
        }

        public override bool exterminate()
        {
            return hp <= 0;
          
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public abstract void Draw(SpriteBatch spriteBatch);
        
    }
}
