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
            Down,
            Up
        }

        private Buttons[] defaultconfig = { Buttons.A, Buttons.B, Buttons.LeftTrigger, Buttons.RightTrigger, Buttons.X };


        public enum InputState
        {
            On = 0,
            Off,
            JustOn,
            JustOff
        }
        private GamePadState previousState;
        private InputState[] commandState;
        private Buttons[] keyMapping;


        public InputManager()
        {
            commandState = new InputState[5];
            keyMapping = new Buttons[5];
            keyMapping = defaultconfig;
            for (int i = 0; i < 5; i++)
            {
                commandState[i] = InputState.Off;
            }
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
                        commandState[i] = InputState.On;
                    }
                    else if (g.IsButtonUp(keyMapping[i]) && previousState.IsButtonDown(keyMapping[i]))
                    {
                        commandState[i] = InputState.JustOff;
                    }
                    else if (g.IsButtonDown(keyMapping[i]) && previousState.IsButtonUp(keyMapping[i]))
                    {
                        commandState[i] = InputState.JustOn;
                    }
                    else if (g.IsButtonUp(keyMapping[i]) && previousState.IsButtonUp(keyMapping[i]))
                    {
                        commandState[i] = InputState.Off;
                    }
                }
                previousState = g;
            }
        }

        public InputState getState(Commands cmd)
        {
            return commandState[(int)cmd];
        }

        public bool IsAnyKetPress()
        {
            for (int i = 0; i < commandState.Length; i++)
            {
                if (commandState[i] == InputState.On) return true;
            }
            return false;
        }



    }

}
