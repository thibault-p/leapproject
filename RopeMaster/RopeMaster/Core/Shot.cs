using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RopeMaster.Core
{




    public class Shot : Entity
    {

        public bool PlayerShot { get; set; }
        public int  damage;
        public bool Exterminate = false;

        public Shot(Vector2 pos, Vector2 velo, int dmg, bool isPlayer) :
            base(pos, velo)
        {
            damage = dmg;
            PlayerShot = isPlayer;
        }

        public void Update(GameTime gameTime)
        {

            base.Update(gameTime);



        }

        public bool exterminate()
        {
            return false;
        }


        



    }
}
