using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Glitch.Engine.Content;
using Microsoft.Xna.Framework;
using RopeMaster.Core;

namespace RopeMaster.gameplay.Enemies
{


    public class Bubble : Shot
    {
        
  

        public Bubble(Vector2 pos, int maxX, Vector2 dir)
            : base(pos, dir, 1, 0, false, 2,3)
        {
            shotWidth = 32;

        }






    }
}
