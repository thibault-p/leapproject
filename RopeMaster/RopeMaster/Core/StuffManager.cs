using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glitch.Engine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Glitch.Engine.Content;
using RopeMaster.gameplay.Enemies;

namespace RopeMaster.Core
{

    public class StuffManager : Manager
    {

        private List<Entity> entities;




        public StuffManager()
        {
            entities = new List<Entity>();

        }



        public void Initialize()
        {
            entities.Clear();

        }


        public void Update(GameTime gameTime)
        {
            foreach (Entity e in entities)
            {
                e.Update(gameTime);
            }
            entities.RemoveAll(c =>c.exterminate());

        }

        public void Add(Entity e)
        {
            entities.Add(e);
        }



        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Entity s in entities)
            {
                s.Draw(spriteBatch);
            }

       
        }

    }
}
