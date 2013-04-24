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
using System.IO;



namespace RopeMaster.Core
{
    [TextureContent(AssetName = "gameover", AssetPath = "gfx/screens/Gameover")]
    public class GameOverscreen : State
    {
        private List<KeyValuePair<long, string>> scores;
        private Texture2D texture, nulltex;
        private Vector2 position;
        private bool done;
        private long timer;
        private string name = "A";
        private Color level1 = new Color(255, 255, 255, 128);
        private bool blink;
        private int currentcar, blinktimer;
        private bool writing;


        private KeyboardState prev;

        public override void Initialyze()
        {
            Game1.Instance.score = 1337;
            texture = Game1.Instance.magicContentManager.GetTexture("gameover");
            nulltex = Game1.Instance.magicContentManager.EmptyTexture;
            position = new Vector2((1280 - 530) / 2, (720 - 266) / 2);
            scores = new List<KeyValuePair<long, string>>(10);
            base.Initialyze();
            done = false;
            writing = false;
            speed = 10;
            timer = 0;
            blink = true;
            currentcar = 0;
            blinktimer = 0;
            readFile();
            scores.Sort(Game1.Compare);

            if (scores[9].Key < Game1.Instance.score)
            {
                writing = true;
            }
            fadeIn();

        }
        public override void Update(GameTime gametime)
        {
            KeyboardState kb = Keyboard.GetState();
            if (prev.IsKeyUp(Keys.A) && kb.IsKeyDown(Keys.A)) changeletter(-1);
            if (prev.IsKeyUp(Keys.Q) && kb.IsKeyDown(Keys.Q)) changeletter(1);
            if (prev.IsKeyUp(Keys.Space) && kb.IsKeyDown(Keys.Space)) validletter();

            prev = kb;
            base.Update(gametime);
            var input = Game1.Instance.inputManager;
            if (input.IsAnyKetPress())
            {
                done = true;
            }
            blinktimer += gametime.ElapsedGameTime.Milliseconds;
            if (blinktimer > 250)
            {
                blink = !blink;
                blinktimer = 0;
            }
            timer += gametime.ElapsedGameTime.Milliseconds;
            if (timer > 30000)
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
            Game1.Instance.StateManage.changeState(5);


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



        private void validletter()
        {
            currentcar++;
            if (currentcar < 10)
            {
                name += "A";
            }
            else
            {
                if (name == "AAAAAAAAAA")
                {
                    name = "PIYO_PIYO";
                }
                done = true;
            }

        }

        private void changeletter(int off)
        {

            var tmp = name.ToArray<char>();
            var n = tmp[currentcar];
            if (n == '_')
            {
                if (off > 0) n = 'A'; else n = '9';
            }
            else
            {
                n = (char)(n + off);
                if (n <= 47) n = 'Z';
                else if (n >= 91) n = '0';
                else if (n == 58) n = '_';
                else if (n == 64) n = '_';
            }
            tmp[currentcar] = (char)(n);


            Console.WriteLine(tmp);
            name = new String(tmp);
        }


        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            spritebatch.Draw(nulltex, Game1.Instance.Screen, Color.Black);
            spritebatch.Draw(texture, position, Color.White);
            if (writing)
            {
                Vector2 p = position;
                p.Y += 300;
                spritebatch.DrawString(Game1.Instance.magicContentManager.Font, "Enter your name for posterity",p,Color.Wheat);

                p.Y += 40;
                for (int i = 0; i < name.Length; i++)
                {
                    var size = Game1.Instance.magicContentManager.Font.MeasureString(name[i].ToString());
                    var off = Vector2.UnitX * ((22 - size.X) / 2 + 2);
                    if (!(i == currentcar && blink)) spritebatch.DrawString(Game1.Instance.magicContentManager.Font, name[i].ToString(), p + off, Color.White);
                    p.X += 24;
                }

            }



            base.Draw(spritebatch);
            spritebatch.End();

        }


    }
}
