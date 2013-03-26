using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RopeMaster.Core
{
    public abstract class Enemy : Entity
    {

        public Enemy():base()
        {

        }


        public override bool exterminate()
        {
            return base.exterminate();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
