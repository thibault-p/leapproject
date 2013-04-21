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
    [TextureContent(AssetName = "presentscreen", AssetPath = "gfx/screen/presentscreen")]
    public  class PresentScreen : State
    {



        private Texture2D texture;
        private Rectangle rabbit;
        private Vector2 posrabbit;
        private long timer;


        public PresentScreen()
        {
            texture = Game1.Instance.magicContentManager.GetTexture("presentscreen");
            rabbit = new Rectangle(0, 0, 50, 55);
            posrabbit = new Vector2(440, 50);
        }


        public override void Initialyze()
        {
         
        }


        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer > 3000)
            {
                changeState();
            }
            

        }


        public override void changeState()
        {
            
        }


        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, posrabbit, rabbit, Color.White);


        }




    }
}
