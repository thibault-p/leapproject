using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RopeMaster.Core
{
    public class SphereBox : Hitbox
    {

        protected int rayon;
     

        public SphereBox(Vector2 p,int r):base(p)
        {
            rayon = r;
        }





        public override bool collide(Vector2 p2, int r)
        {
            var d = Vector2.Distance(this.position, p2);
            return (d <= r + rayon);
        }


        public override bool collide(Hitbox h)
        {
            return false;
        }
    }
}
