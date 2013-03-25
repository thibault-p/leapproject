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
        public static float gravity = 9.8f;
        public static float pm = 1;
        private int div;
        private int delta;
        private Texture2D tex;
        private int fixeddiv = 1;
        private Vector2 lastorigin;


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
                // Console.WriteLine("UP !");
                this.fixeddiv--;
            }

        }


        public void down()
        {
            if (this.fixeddiv < this.div - 1)
            {
                //Console.WriteLine("DOWN ! ");
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
                int weight = (i == this.div - 1) ? 1000 : 100;
                sticks[i].setAcc(0, gravity * (weight));
                sticks[i].verlet(dt);
            }
            for (int j = 0; j < div; j++)
                for (int i = 1; i < this.div; i++)
                {
                    var s1 = sticks[i];
                    var s2 = sticks[i - 1];
                    var dx = (s1.getPosition().X - s2.getPosition().X);
                    var dy = (s1.getPosition().Y - s2.getPosition().Y);
                    var d = (float)Math.Sqrt((dx * dx) + (dy * dy));
                    var diff = d - delta;
                    var x1 = s1.getPosition().X;
                    var y1 = s1.getPosition().Y;
                    var x2 = s2.getPosition().X;
                    var y2 = s2.getPosition().Y;
                    var offx = (dx / d) * 0.5f * diff;
                    var offy = (dy / d) * 0.5f * diff;



                    
                        x1 -= offx;
                        y1 -= offy;
                        x2 += offx;
                        y2 += offy;
                    
                    

                        s1.setPosition(x1, y1);
                        s2.setPosition(x2, y2);
                    
                }
            //checkColl();

        }

        public void checkColl()
        {
            for (int i = 0; i < this.div; i++)
            {
                var x = sticks[i].getPosition().X;
                var y = sticks[i].getPosition().Y;
                x = Math.Min(800 - 8, Math.Max(x, 0));

                y = Math.Min(600 - 8, Math.Max(y, 0));
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

        public Vector2 getAttachPosition()
        {
            return sticks[div - 1].getPosition();
        }

        public Vector2 getAttachAngle()
        {

            //var r = sticks[div - 1].getRotation();
            var dx = sticks[div - 1].getPosition().X - sticks[fixeddiv].getPosition().X;
            var dy = sticks[div - 1].getPosition().Y - sticks[fixeddiv].getPosition().Y;



            var r = Math.Atan2(dx, dy);

            return new Vector2((float)Math.Cos(r), -(float)Math.Sin(r));





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
                curpos += (0.99f * curpos - 0.99f * oldpos) + (acc * dt * dt);


                //curpos = (2 * curpos - oldpos) + (acc * dt * dt);
                oldpos = tmp;

            }
            public float getRotation()
            {
                var dx = oldpos.X - curpos.X;
                var dy = oldpos.Y - curpos.Y;
                return (float)Math.Atan2(dy, dx);
            }

        }




    }



}
