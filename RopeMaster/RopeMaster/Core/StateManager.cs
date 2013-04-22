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
            //changeState(3);

        }


        public void changeState(int screen)
        {
            switch (screen)
            {
                case 0: currentState = new LeapScreen();
                    break;

                case 1: currentState = new PresentScreen();
                    break;
                case 2: currentState = new TitleScreen();
                    break;
                case 3: currentState = new Gamescreen();
                    break;
                case 4: currentState = new GameOverscreen();
                    break;
                case 5: currentState = new HallofFameScreen();
                    break;
            }
            currentState.Initialyze();



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
