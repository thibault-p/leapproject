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
    public class PresentScreen : State
    {



        private Texture2D texture;
        private Rectangle rabbit, name, srcshadow,dstshadow,present,music;
        private Vector2 posrabbit, origin, posname,pospresent,posmusic;
        private long timer, timerwait;

        private int curAnim = 0;
        private bool animdone = false;


        public PresentScreen()
        {
            Initialyze();
        }


        public override void Initialyze()
        {

            texture = Game1.Instance.magicContentManager.GetTexture("presentscreen");
            rabbit = new Rectangle(0, 0, 50, 57);
            origin = new Vector2(25, 57);
            posrabbit = new Vector2(440, 0);
            name = new Rectangle(0, 176, 400, 40);
            posname = new Vector2(490, 320);
            srcshadow = new Rectangle(250, 110, 39, 6);
            dstshadow = new Rectangle(385, 360, 100, 6);
            present = new Rectangle(0, 222, 175, 20);
            pospresent = new Vector2(552, 360);
            animdone = false;
            music = new Rectangle(0, 252, 370, 55);
            posmusic = new Vector2((1280 - 370) / 2, 460);
            curAnim = 1;
            timer = timerwait = 0;
        }


        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
          
            {
                if (timer > 100)
                {
                    //animation
                    rabbit.X += 50;
                    if (curAnim == 0 && rabbit.X > 200)
                        rabbit.X = 0; //idle
                    else if (curAnim == 1 && rabbit.X > 50) // falling
                        rabbit.X = 0;
                    else if (curAnim == 2 && rabbit.X > 200)
                    { // touching
                        rabbit.X = 0;
                        curAnim = 0;
                        animdone = true;
                    }

                    timer = 0;
                }

                rabbit.Y = curAnim * 57;
   
                if (posrabbit.Y <= 360)
                {
                    posrabbit.Y += 4;
                    dstshadow.Width = (int)(posrabbit.Y / 360f * 100f);
                    dstshadow.X = (int)(385f + (100f -dstshadow.Width ) / 2f);
                }
                else if (curAnim == 1)
                {
                    dstshadow.X=1300;
                    rabbit.X = 0;
                    curAnim = 2;
                }

            }
            if (animdone)
            {
                timerwait += gameTime.ElapsedGameTime.Milliseconds;
                if (timer > 3000)
                {
                    changeState();
                }

            }




        }


        public override void changeState()
        {

        }


        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin(SpriteSortMode.Immediate,BlendState.AlphaBlend);

            spritebatch.Draw(texture, dstshadow, srcshadow, Color.White);
            spritebatch.Draw(texture, posrabbit, rabbit, Color.White, 0, origin, 2, SpriteEffects.None, 0);
            if (curAnim == 0)
            {
                spritebatch.Draw(texture, posname, name, Color.White);
                spritebatch.Draw(texture, pospresent, present, Color.White);
                spritebatch.Draw(texture, posmusic, music, Color.White);
            }
            spritebatch.End();
        }




    }
}
