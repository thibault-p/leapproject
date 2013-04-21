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
using Glitch.Engine.Content;



namespace RopeMaster.Core
{
    [TextureContent(AssetName = "null", AssetPath = "gfx/misc/null", IsEmptyTexture = true, LoadOnStartup = true)]
    public abstract class State
    {

        protected Texture2D text_null;

        protected Color color;
        protected int fade;
        protected int speed = 1;





        public virtual void Initialyze()
        {
            Game1.Instance.musicPlayer.StopMusic();
            color = Color.Black;
            color.A = 255;
            text_null = Game1.Instance.magicContentManager.GetTexture("null");
        }


        public virtual void changeState()
        {


        }

        protected void fadeIn()
        {
            fade = -1;
        }
        protected void fadeOut()
        {
            fade = 1;
        }


        public virtual void Update(GameTime _gametime)
        {
            var tc = (int)color.A;
            if (fade != 0)  tc+= (fade * speed);
            if (fade > 0 && tc>= 255)
            {
                tc = 255;
                fade = 0;
            }
            else if (fade < 0 && tc <= 0)
            {
                tc= 0;
                fade = 0;
            }
            color.A = (byte)tc;

        }

        public virtual void Draw(SpriteBatch _spritebatch)
        {
            if (color.A > 0) _spritebatch.Draw(text_null, Game1.Instance.Screen, color);


        }

    }
}
