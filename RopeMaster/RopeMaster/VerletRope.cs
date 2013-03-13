using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace RopeMaster
{

    public class VerletRope
    {
        public Stick[] sticks;
        public static float gravity = 9.8f ;
        public static float pm = 1;
        private int div;
        private int delta;
        private Texture2D tex;
        private int fixeddiv = 1;


        public VerletRope(int _div, int _length, Vector2 _pos, Texture2D _tex)
        {
            this.div = _div;
            this.tex = _tex;
            sticks = new Stick[div];
            delta = _length / _div;
            for (int i = 0; i < _div; i++)
            {
                sticks[i] = new Stick(new Vector2(50, 50 + delta * i));
            }

        }


        public void up()
        {
            if (this.fixeddiv > 1)
            {
                Console.WriteLine("UP !");
                this.fixeddiv--;
            }

        }


        public void down()
        {
            if (this.fixeddiv < this.div - 1)
            {
                Console.WriteLine("DOWN ! ");
                this.fixeddiv++;
            }

        }



        public void setOrigin(int x, int y)
        {

            sticks[fixeddiv].fixPosition(x, y);

        }

        public void Update(GameTime gameTime)
        {
            float dt = 1 / 60f;
            for (int i = 0; i < this.div; i++)
            {
                int weight = (i==this.div-1)? 1000:100;
                sticks[i].setAcc(0, gravity*(weight));
                sticks[i].verlet(dt);
            }
            for (int i = 1; i < this.div; i++)
            {
                var s1 = sticks[i];
                var s2 = sticks[i - 1];
                var dx = (s1.getPosition().X - s2.getPosition().X) / pm;
                var dy = (s1.getPosition().Y - s2.getPosition().Y) / pm;
                var d = (float)Math.Sqrt((dx * dx) + (dy * dy));
                var diff = d - delta;
                var x1 = s1.getPosition().X;
                var y1 = s1.getPosition().Y;
                var x2 = s2.getPosition().X;
                var y2 = s2.getPosition().Y;


                x1 -= (dx / d) * 0.5f * pm * diff;
                y1 -= (dy / d) * 0.5f * pm * diff;
                x2 += (dx / d) * 0.5f * pm * diff;
                y2 += (dy / d) * 0.5f * pm * diff;
                s1.setPosition(x1, y1);
                s2.setPosition(x2, y2);
            }
            checkColl();

        }

        public void checkColl()
        {
            for (int i = 0; i < this.div; i++)
            {
                var x = sticks[i].getPosition().X;
                var y = sticks[i].getPosition().Y;
                x = Math.Min(800-8, Math.Max(x, 0));

                y = Math.Min(600-8, Math.Max(y, 0));
                sticks[i].setPosition(x, y);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = fixeddiv; i < this.div; i++)
            {
                spriteBatch.Draw(this.tex, sticks[i].getPosition(), Color.White);
            }

        }

        public class Stick
        {
            Vector2 curpos, oldpos;
            Vector2 acc;
            public Stick(Vector2 _pos)
            {
                curpos = oldpos = _pos;
                acc = Vector2.Zero;
            }


            public void setAcc(float ax, float ay)
            {
                this.acc.X = ax;
                this.acc.Y = ay;
            }


            public Vector2 getPosition()
            {
                return this.curpos;
            }

            public void fixPosition(int x, int y)
            {
                this.curpos.X = x;
                this.curpos.Y = y;
                this.oldpos = this.curpos;

            }

            public void setPosition(float _x, float _y)
            {
                this.curpos.X = _x;
                this.curpos.Y = _y;
            }

            public void verlet(float dt)
            {
                var tmp = curpos;
                curpos.X += (0.99f * curpos.X - 0.99f * oldpos.X) + (acc.X * pm * dt * dt);
                curpos.Y += (0.99f * curpos.Y - 0.99f * oldpos.Y) + (acc.Y * pm * dt * dt);
                oldpos = tmp;
            }

        }


    }



}
