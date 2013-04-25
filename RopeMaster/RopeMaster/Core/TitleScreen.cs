using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Glitch.Engine.Content;

namespace RopeMaster.Core
{
    [TextureContent(AssetName = "homescreen", AssetPath = "gfx/screens/home")]
    [TextureContent(AssetName = "title", AssetPath = "gfx/screens/title")]
    [TextureContent(AssetName = "hometitle", AssetPath = "gfx/screens/home_title")]
    public class TitleScreen : State
    {
        private Texture2D texture, title,start;
        private Vector2 position;
        private long timer;
        private bool done;
        private int nextscreen;
        private List<Star> stars;
        private bool displayed;

        public override void Initialyze()
        {
            stars = new List<Star>();
            texture = Game1.Instance.magicContentManager.GetTexture("homescreen");
            start = Game1.Instance.magicContentManager.GetTexture("title");
            title = Game1.Instance.magicContentManager.GetTexture("hometitle");
            position = new Vector2(849,585);
            base.Initialyze();
            done = false;
            nextscreen = 5;
            Game1.Instance.musicPlayer.PlayMusic("spacealone");
            speed = 10;
            fadeIn();
            displayed = false;


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
            if (timer % 400== 0) displayed = !displayed;
            if (timer > 15000)
            {
                done = true;
            }
            if (done)
            {
                fadeOut();
                if (color.A >= 255)
                    changeState();
            }

            foreach (Vector2 v in Game1.Instance.leapControl.getAllPoints())
            {
                if (position.X == 0 || position.X == 1280 || position.Y == 0) continue;
                stars.Add(new Star(v));
            }


            foreach (Star s in stars)
            {
                s.Update(gametime);
            }
            stars.RemoveAll(c => c.killMe());

        }

        public override void changeState()
        {
            Game1.Instance.StateManage.changeState(nextscreen);
        }



        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spritebatch.Draw(texture, Game1.Instance.Screen, Color.White);
            //draw stars
            foreach (Star s in stars)
            {
                spritebatch.Draw(Game1.Instance.magicContentManager.EmptyTexture, s.dst, s.color);
            }

            spritebatch.Draw(title, Game1.Instance.Screen, Color.White);
            if(displayed) spritebatch.Draw(start, position, Color.White);
            base.Draw(spritebatch);
            spritebatch.End();

        }



        public class Star{

            public Color color = Color.White;
            public Vector2 position;
            public Rectangle dst;
            private int speed = 30;


            public Star(Vector2 pos)
            {
                
                position = pos;
                var w = Game1.Instance.randomizator.GetRandomInt(2,4);
                dst = new Rectangle((int)pos.X,(int)pos.Y,w,w);
                speed -= Game1.Instance.randomizator.GetRandomInt(2,10);
            }


            public void Update(GameTime gameTime)
            {
                int t=color.A;
                t= Math.Max(0,t-speed);

                color.A = (byte)t;


            }

            public bool killMe()
            {
                return color.A == 0;
            }

        }




    }


}
