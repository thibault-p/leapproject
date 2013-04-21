using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Glitch.Engine.Core;



namespace RopeMaster.Core
{
    public  class StateManager : Manager
    {

        private State currentState;


        public StateManager()
        {
           
        }



        public void Initialize()
        {
            Console.WriteLine("init");
            currentState = new PresentScreen();

        }

        public void Update(GameTime gametime)
        {

            currentState.Update(gametime);
        }

        public void Draw(SpriteBatch spritebatch)
        {

            currentState.Draw(spritebatch);


        }




    }
}
