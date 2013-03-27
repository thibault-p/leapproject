using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RopeMaster.Core
{
    public class EnemySpawner<T> : Entity
    {
        private int load;



        public EnemySpawner(Vector2 _pos, int _load):base(_pos,Vector2.Zero)
        {
            load = _load;
        }


        public void Update(GameTime gameTime)
        {


            







        }



        public override  bool exterminate()
        {
            return load <= 0;
        }

        

        public void fire()
        {
            Game1.Instance.enemyManager.Add(typeof(T));

        }






        




    }
}
