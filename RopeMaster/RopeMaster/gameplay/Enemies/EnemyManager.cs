using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glitch.Engine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RopeMaster.gameplay.Enemies
{
    public class EnemyManager : Manager
    {

        public List<Enemy> enemies;
        public List<Enemy> enemiestoAdd; 
        public EnemyManager()
        {


        }

        


        public void Initialize()
        {
            enemies = new List<Enemy>();
            enemiestoAdd = new List<Enemy>();
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
            enemies.AddRange(enemiestoAdd);
            enemiestoAdd.Clear();

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
