using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glitch.Engine.Core;
using Microsoft.Xna.Framework;

namespace RopeMaster.Core
{
    public class EnemyManager : Manager
    {

        private List<Enemy> enemies; 

        public EnemyManager()
        {


        }



        public void Initialize()
        {
            enemies = new List<Enemy>();

        }





        public void Update(GameTime gameTime){

            //delete all useless enemies
            enemies.RemoveAll(e => e.exterminate());

        }


    }
}
