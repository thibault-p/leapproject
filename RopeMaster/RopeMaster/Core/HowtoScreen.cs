using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Glitch.Engine.Content;

namespace RopeMaster.Core
{
    [TextureContent(AssetName = "howto", AssetPath = "gfx/screens/howto")]
    public class HowtoScreen : State
    {



        private Texture2D texture;
        private Vector2 position;
        private long timer;
        private bool done;



        public override void Initialyze()
        {
            texture = Game1.Instance.magicContentManager.GetTexture("howto");
            position =Vector2.Zero;
            base.Initialyze();
            done = false;
            speed = 10;
            fadeIn();

        }
        public override void Update(GameTime gametime)
        {

            base.Update(gametime);
            var input = Game1.Instance.inputManager;
            if (input.IsAnyKetPress())
            {
                done = true;
            }




            timer += gametime.ElapsedGameTime.Milliseconds;
            if (timer > 10000)
            {
                done = true;
            }
            if (done)
            {
                fadeOut();
                if (color.A >= 255)
                    changeState();
            }


        }

        public override void changeState()
        {
            Game1.Instance.StateManage.changeState(3);


        }



        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
          

            spritebatch.Draw(texture, position, Color.White);
            base.Draw(spritebatch);
            spritebatch.End();

        }





    }


}
