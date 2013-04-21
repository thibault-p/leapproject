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
using Glitch.Engine.Core;
using Glitch.Engine.Content;



namespace RopeMaster.Core
{
    [FontContent(AssetName = "default", AssetPath = "font/default", IsDefaultFont=true, LoadOnStartup=true)]
    public class GameManager : Manager
    {

        public const long timemax=180000;

        private SpriteFont font;
        public long time;
        public int life;
        public int currentstate = 0;
        public long score = 0;

        public GameManager():base()
        {
            Initialize();

        }




        public void Initialize()
        {
            life = 3;
            score = 0;
            time = 0;
            font = Game1.Instance.magicContentManager.Font;
        }



        public void Update(GameTime gametime)
        {
            time += gametime.ElapsedGameTime.Milliseconds;



        }





        public void Draw(SpriteBatch spritebatch)
        {
            var t = (timemax-time)/1000;
            int s =  (int)t%60;
            spritebatch.DrawString(font, "Score: " + score.ToString("0000000000"), Vector2.Zero, Color.White);
            spritebatch.DrawString(font, "Timeleft: " + (t/60 ) +":"+s.ToString("00"), Vector2.Zero, Color.White);

        }




    }
}
