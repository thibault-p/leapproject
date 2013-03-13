using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace RopeMaster.Core
{
    public class InputManager
    {

        private int NB_CMD = 5;
        public enum Commands
        {
            Validate = 0,
            Cancel,
            Fire,
            Up,
            Down
        }

        private Buttons[] defaultconfig ={ Buttons.A, Buttons.B, Buttons.A, Buttons.A, Buttons.X}; 


        public enum State
        {
            On = 0,
            Off,
            JustOn,
            JustOff
        }
        private GamePadState previousState;
        private State[] commandState;
        private Buttons[] keyMapping;


        public InputManager()
        {
            commandState = new State[5];
            keyMapping = new Buttons[5];
            keyMapping = defaultconfig;
     }

        public void Update(GameTime gameTime)
        {
            var g = GamePad.GetState(PlayerIndex.One);
            if (g.IsConnected)
            {
                for (int i = 0; i < NB_CMD; i++)
                {
                    if (g.IsButtonDown(keyMapping[i]) && previousState.IsButtonDown(keyMapping[i]))
                    {
                        commandState[i] = State.On;
                    }else if (g.IsButtonUp(keyMapping[i]) && previousState.IsButtonDown(keyMapping[i]))
                    {
                        commandState[i] = State.JustOff;
                    }else if (g.IsButtonDown(keyMapping[i]) && previousState.IsButtonUp(keyMapping[i]))
                    {
                        commandState[i] = State.JustOn;
                    }else if (g.IsButtonUp(keyMapping[i]) && previousState.IsButtonUp(keyMapping[i]))
                    {
                        commandState[i] = State.Off;
                    }
                }
                previousState = g;
            }
        }

        public State getState(Commands cmd)
        {
            return commandState[(int)cmd];
        }

    }

}
