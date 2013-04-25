using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RopeMaster.Core
{




    public class Shot : Entity
    {
        public int shotWidth = 4;
        public static int framerate = 150;
        public float scale;
        public bool PlayerShot { get; set; }
        public int damage;
        public bool Exterminate = false;
        protected int point;
        public int type;
        public int nbAnim;
        public int current = 0;
        private long timer = 0;
        public float angle = 0;

        public Shot(Vector2 pos, Vector2 velo, int dmg, int pts, bool isPlayer, int _type, int nb_anim) :
            base(pos, velo)
        {
            damage = dmg;
            PlayerShot = isPlayer;
            this.origin = new Vector2(5, 5);
            this.point = pts;
            type = _type;
            nbAnim = nb_anim;

        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer > framerate)
            {
                timer = 0;
                current = (current + 1) % nbAnim;
            }
        }

        public override bool exterminate()
        {
            return false;
        }






    }
}
