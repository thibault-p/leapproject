using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Leap;
using Microsoft.Xna.Framework.Graphics;
using Glitch.Engine.Content;

namespace RopeMaster.Core
{

    [TextureContent(AssetName = "point", AssetPath = "gfx/point")]
    public class LeapControl
    {



        public bool Is { get; set; }


        private Vector2 position, last;
        private Controller controller;
        private List<Vector2> points;
        private long timer;

        public LeapControl()
        {
            Is = false;
            position = last = Vector2.Zero;
            controller = new Controller();
            points = new List<Vector2>();
        }


        public void Update(GameTime gameTime)
        {
            if (controller == null) return;
            Is = false;
            points.Clear();
            if (controller.CalibratedScreens.Empty)
            {
                Console.WriteLine("no screen");
                return;
            }
            Frame frm = controller.Frame();
            FingerList t = frm.Fingers;
            if (t.Empty)
            {
                return;
            }

            bestPosition(t);


        }


        private bool bestPosition(FingerList list)
        {
            if (Gamescreen.Instance == null) return false;
            if (Gamescreen.Instance.player == null) return false;
            double best = 9000;
            last = Gamescreen.Instance.player.getPosition();
            foreach (Finger f in list)
            {

                Vector v2 = controller.CalibratedScreens[0].Intersect(f, true);
                
                var px = controller.CalibratedScreens[0].WidthPixels * v2.x;
                var py = controller.CalibratedScreens[0].HeightPixels - controller.CalibratedScreens[0].HeightPixels * v2.y;
                points.Add(new Vector2(px, py));
                var d = Math.Sqrt((px - last.X) * (px - last.X) + (py - last.Y) * (py - last.Y));
                if (d < 100)
                {
                    if (d < best)
                    {
                        //last = position;
                        position.X = px;
                        position.Y = py;
                        Is = true;
                        best = d;
                    }
                }
            }
            return false;
        }


        public List<Vector2> getAllPoints()
        {
            return this.points;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (controller == null) return;
        
            foreach (Vector2 v in points)
                spriteBatch.Draw(Game1.Instance.magicContentManager.GetTexture("point"), v, Color.Red);


        }


        public Vector2 getPosition()
        {
            return position;
        }
    }
}
