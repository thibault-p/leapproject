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
    [TextureContent(AssetName = "highscore", AssetPath = "gfx/screens/highscore")]
    [TextureContent(AssetName = "homescreen", AssetPath = "gfx/screens/home")]
    [TextureContent(AssetName = "halloffame", AssetPath = "gfx/screens/halloffame")]
    public class HallofFameScreen : State
    {

        private List<RopeMaster.Core.TitleScreen.Star> stars;
        private SpriteFont font;
        private Texture2D texture, back, text;

        private Vector2 position, postext;
        private long timer;
        private bool done;
        private List<ScoreEntry> entries;
        private List<KeyValuePair<long, string>> scores;
        private int n;

        public override void Initialyze()
        {
            n = 0;
            postext = new Vector2(320, 20);
            stars = new List<RopeMaster.Core.TitleScreen.Star>();
            font = Game1.Instance.magicContentManager.Font;
            scores = new List<KeyValuePair<long, string>>(10);
            entries = new List<ScoreEntry>(10);
            texture = Game1.Instance.magicContentManager.GetTexture("highscore");
            back =  Game1.Instance.magicContentManager.GetTexture("homescreen");
            text = Game1.Instance.magicContentManager.GetTexture("halloffame");
            position = new Vector2((1280 - 530) / 2, (720 - 266) / 2);
            base.Initialyze();
            done = false;
            Game1.Instance.musicPlayer.PlayMusic("spacealone");
            speed = 10;
            fadeIn();
            readFile();
            scores.Sort(Game1.Compare);
            int r = 1;
            foreach (KeyValuePair<long, string> v in scores)
            {
                initScore(r++, v);
            }
            entries[0].playing = true;
        }
        public override void Update(GameTime gametime)
        {

            base.Update(gametime);
            var input = Game1.Instance.inputManager;
            if (input.IsAnyKetPress())
            {
                done = true;
            }

            foreach (Vector2 v in Game1.Instance.leapControl.getAllPoints())
            {
                if (position.X == 0 || position.X == 1280 || position.Y == 0) continue;
                stars.Add(new RopeMaster.Core.TitleScreen.Star(v));
            }


            foreach (RopeMaster.Core.TitleScreen.Star s in stars)
            {
                s.Update(gametime);
            }
            stars.RemoveAll(c => c.killMe());


            if (n < entries.Count && entries[n].nameposition.X >= 240)
            {
                entries[n].playing = false;
                n++;
                if (n < entries.Count)
                    entries[n].playing = true;

            }
            foreach (ScoreEntry s in entries) { s.Update(gametime); }

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

        private void initScore(int r, KeyValuePair<long, string> p)
        {

            var ns = font.MeasureString("XXth - " + p.Value);
            var ss = font.MeasureString(" " + p.Key);
            entries.Add(new ScoreEntry(r, p.Value, p.Key, ns, ss));
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
            Vector2 pos = Vector2.Zero;
            spritebatch.Draw(back, Game1.Instance.Screen, Color.White);
            
      
            
            //draw stars
            foreach (RopeMaster.Core.TitleScreen.Star s in stars)
            {
                spritebatch.Draw(Game1.Instance.magicContentManager.EmptyTexture, s.dst, s.color);
            }


            spritebatch.Draw(texture, Game1.Instance.Screen, Color.White);
            spritebatch.Draw(text, postext, Color.White);
            foreach (ScoreEntry s in entries)
            {
                spritebatch.DrawString(font, s.name, s.nameposition, Color.White);
                spritebatch.DrawString(font, " " + s.score, s.scorposition, Color.White);
            }
            base.Draw(spritebatch);
            spritebatch.End();

        }



        public class ScoreEntry
        {
            public int rank;
            public string name;
            public long score;
            public Vector2 nameposition, scorposition;
            public bool playing;
            public Rectangle box;


            public ScoreEntry(int r, string n, long s, Vector2 npos, Vector2 spos)
            {
                rank = r;

                var t = "th";
                switch (r)
                {
                    case 1: t = "st"; break;
                    case 2: t = "nd"; break;
                    case 3: t = "rd"; break;
                }
                name = r + t + " - " + n;
                score = s;
                var size = npos + spos;
                nameposition = new Vector2(240, r * npos.Y+160);
                scorposition = new Vector2(1040 - spos.X, r * spos.Y+160);
                nameposition.X -= 1040;
                scorposition.X -= 1040;
                playing = false;
            }

            public void Update(GameTime gametime)
            {
                if (playing)
                {
                    nameposition.X += 40;
                    scorposition.X += 40;

                }
            }


        }

    }



}
