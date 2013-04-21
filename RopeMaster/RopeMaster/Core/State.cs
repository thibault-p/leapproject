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



namespace RopeMaster.Core
{
    public abstract class State
    {

        public abstract void Initialyze(); 


        public virtual void changeState()
        {

        }

        public abstract void Update(GameTime _gametime);

        public abstract void Draw(SpriteBatch _spritebatch);
       
    }
}
