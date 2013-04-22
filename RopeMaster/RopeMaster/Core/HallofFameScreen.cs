using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using Glitch.Engine.Content;

namespace RopeMaster.Core
{
     [TextureContent(AssetName = "highscore", AssetPath = "gfx/screen/highscore")]
    public class HallofFameScreen : State
    {


        private SpriteFont font;
        private Texture2D texture;
        private Vector2 position;
        private long timer;
        private bool done;
        private List<ScoreEntry> entries;
        private List<KeyValuePair<long, string>> scores;

        public override void Initialyze()
        {
            stopmusic = false;
            font = Game1.Instance.magicContentManager.Font;
            scores = new List<KeyValuePair<long, string>>(10);
            entries = new List<ScoreEntry>(10);
            texture = Game1.Instance.magicContentManager.GetTexture("highscore");
            Game1.Instance.musicPlayer.PlayMusic("spacealone");
            position = new Vector2((1280 - 530) / 2, (720 - 266) / 2);
            base.Initialyze();
            done = false;
            speed = 10;
            fadeIn();
            readFile();
            scores.Sort(Game1.Compare);



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
            if (timer > 5000)
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

        private void initScore(int r, KeyValuePair<long, string> p)
        {

            var ns = font.MeasureString("XXth - " + p.Value);
            var ss= font.MeasureString(" " + p.Key);
            entries.Add(new ScoreEntry(r,p.Value,p.Key,ns,ss));
        }


        private void readFile()
        {
            TextReader readFile = null;
            try
            {
                string line;
                readFile = new StreamReader(Game1.SCORE_FILE);
                while (true)
                {
                    line = readFile.ReadLine();
                    if (line != null)
                    {
                        string[] splited = line.Split('=');
                        long s = long.Parse(splited[1]);
                        Console.WriteLine(splited[0] + "  -> " + s);
                        scores.Add(new KeyValuePair<long, string>(s, splited[0]));
                    }
                    else
                    {
                        break;
                    }
                }
                readFile.Close();
                readFile = null;
            }
            catch (IOException e)
            {
                if (readFile != null)
                {
                    readFile.Close();
                }
            }
        }


        public override void changeState()
        {
            Game1.Instance.StateManage.changeState(0);


        }



        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            spritebatch.Draw(texture, Game1.Instance.Screen,Color.White);
            Vector2 pos = Vector2.Zero;
            foreach ( ScoreEntry s in entries){
                var t = "th";
                spritebatch.DrawString(font, s.rank+t, pos, Color.Black);
                pos.Y += 25;

            }
            //spritebatch.Draw(texture, position, Color.White);
            base.Draw(spritebatch);
            spritebatch.End();

        }


        public class Star
        {

            public Color color = Color.White;
            public Vector2 position;
            public Rectangle dst;
            private int speed = 30;


            public Star(Vector2 pos)
            {

                position = pos;
                var w = Game1.Instance.randomizator.GetRandomInt(2, 4);
                dst = new Rectangle((int)pos.X, (int)pos.Y, w, w);
                speed -= Game1.Instance.randomizator.GetRandomInt(2, 10);
            }


            public void Update(GameTime gameTime)
            {
                int t = color.A;
                t = Math.Max(0, t - speed);

                color.A = (byte)t;


            }

            public bool killMe()
            {
                return color.A == 0;
            }

        }

        public class ScoreEntry
        {
            public int rank;
            public string name;
            public long score;
            public Vector2 nameposition, scorposition;
            public bool playing;
            public Rectangle box;


            public ScoreEntry(int r,string n, long s, Vector2 npos, Vector2 spos)
            {
                rank = r;
                name = n;
                score = s;
                nameposition = npos;
                scorposition = spos;
                playing = false;
            }

            public void Update(GameTime gametime)
            {
                if (playing)
                {


                }
            }

            
        }

    }



}
