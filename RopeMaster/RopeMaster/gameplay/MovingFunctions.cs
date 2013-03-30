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



namespace RopeMaster.gameplay
{
    public class MovingFunctions
    {
        public static float SinHorizontal(float x, float amp, float phase)
        {
            return (float)Math.Sin((x+phase)/2*Math.PI)*amp+0.5f;
        }



     




    }
}
