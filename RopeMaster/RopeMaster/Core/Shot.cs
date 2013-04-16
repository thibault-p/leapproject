using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RopeMaster.Core
{




    public class Shot : Entity
    {
        public static int shotWidth = 4;


        public bool PlayerShot { get; set; }
        public int  damage;
        public bool Exterminate = false;
        protected int point;
        public int type;
        public int nbAnim;

        public Shot(Vector2 pos, Vector2 velo, int dmg, int pts, bool isPlayer, int _type, int nb_anim) :
            base(pos, velo)
        {
            damage = dmg;
            PlayerShot = isPlayer;
            this.origin = new Vector2(5, 5);
            this.point = pts;
            type = _type;
            nbAnim = nbAnim;
        }



        public override bool exterminate()
        {
            return false;
        }


        



    }
}
