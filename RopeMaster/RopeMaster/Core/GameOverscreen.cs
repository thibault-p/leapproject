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
     [TextureContent(AssetName = "gameover", AssetPath = "gfx/screen/Gameover")]
    public class GameOverscreen : State
    {
 
        private Texture2D texture, nulltex;
        private Vector2 position;
        private bool done;
        private long timer;

        public override void Initialyze()
        {
            texture = Game1.Instance.magicContentManager.GetTexture("gameover");
            nulltex = Game1.Instance.magicContentManager.EmptyTexture;
            position = new Vector2((1280 - 530) / 2, (720 - 266) / 2);
            base.Initialyze();
            done = false;
            speed = 10;
            timer = 0;
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
            if (timer > 3000)
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
            Game1.Instance.StateManage.changeState(0);


        }



        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            spritebatch.Draw(nulltex, Game1.Instance.Screen, Color.Black);
            spritebatch.Draw(texture, position, Color.White);
            base.Draw(spritebatch);
            spritebatch.End();

        }


    }
}
