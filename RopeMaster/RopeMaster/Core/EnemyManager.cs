using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glitch.Engine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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



        public void Add(Enemy e)
        {
            enemies.Add(e);
        }

        public void Update(GameTime gameTime){
            foreach (Enemy e in enemies)
            {
                e.Update(gameTime);
            }
            //delete all useless enemies
            enemies.RemoveAll(e => e.Exterminate());

        }

        public void Draw(SpriteBatch spritebatch)
        {
            foreach (Enemy e in enemies)
            {
                e.Draw(spritebatch);
            }
        }

    }
}
