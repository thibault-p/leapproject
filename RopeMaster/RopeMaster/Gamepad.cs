using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;



namespace RopeMaster
{
    class Gamepad
    {
        public enum PadState
        {
            Released,
            JustRelease,
            Pressed,
            JustPressed
        };
        public enum Command
        {
            Fire=0,
            GoUp=1,
            GoDown=2,
            Pause=3,
            Validate=4,
            Cancel=5
        }
        private const int cmd = 6;


        private GamePadState previousState;

        private PadState[] stateCommand;
        private GamePadButtons[] keymap;

        public Gamepad()
        {
            stateCommand = new PadState[cmd];
           // keymap = new Buton[cmd];
            for (int i = 0; i < cmd; i++)
            {
                stateCommand[i] = PadState.Released;

            }
        }


        private void defaultKeymap()
        {
     



        }
        public void Update(GameTime gameTime)
        {
            GamePadState state = GamePad.GetState(PlayerIndex.One);
            if (state.IsConnected)
            {
                for (int i = 0; i < cmd; i++)
                {
                    

                }
            }
            previousState = state;
        }







    }
}
