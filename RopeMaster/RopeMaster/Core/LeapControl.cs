using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Leap;

namespace RopeMaster.Core
{
    public class LeapControl
    {



        public bool Is { get; set; }


        private Vector2 position, last;
        private Controller controller;
        
        public LeapControl()
        {
            Is = false;
            controller = new Controller();
        }


        public void Update(GameTime gameTime)
        {
            if (controller.CalibratedScreens.Empty)
            {
                Is = false;
                Console.WriteLine("no screen");
                return;
            }
            Frame frm = controller.Frame();
            ToolList t = frm.Tools;
            if (t.Empty)
            {
                Is = false;
                return ;
            }
            Tool tool = t[0];
            Vector v = tool.TipVelocity;
            Vector v2 =controller.CalibratedScreens[0].Intersect(tool,true);

            position.X=  controller.CalibratedScreens[0].WidthPixels * v2.x ;
            position.Y = controller.CalibratedScreens[0].HeightPixels - controller.CalibratedScreens[0].HeightPixels * v2.y;
            Is = true;
        }

        public Vector2 getPosition()
        {
            return position;
        }
    }
}
