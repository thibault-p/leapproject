using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Glitch.Engine.Content;

namespace RopeMaster.Core
{
    [TextureContent(AssetName = "title", AssetPath = "gfx/screen/title")]
    public class TitleScreen : State
    {
        private Texture2D texture;
        private Vector2 position;
        private long timer;
        private bool done;
        private int nextscreen;


        public override void Initialyze()
        {
            texture = Game1.Instance.magicContentManager.GetTexture("title");
            position = Vector2.Zero;
            base.Initialyze();
            done = false;
            nextscreen = 0;
            Game1.Instance.musicPlayer.PlayMusic("spacealone");
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
                nextscreen = 3;
            }




            timer += gametime.ElapsedGameTime.Milliseconds;
            if (timer > 20000)
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
            Game1.Instance.StateManage.changeState(nextscreen);
        }



        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spritebatch.Draw(texture, Game1.Instance.Screen, Color.White);
            base.Draw(spritebatch);
            spritebatch.End();

        }





    }


}
