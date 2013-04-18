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



namespace RopeMaster.gameplay
{
    [TextureContent(AssetName = "lifebar", AssetPath = "gfx/sprites/lifebar")]
    public class LifeBar
    {

        private Vector2 position,posbar;
        private Rectangle srcbar,srcback,srcfor;
        private int max;
        private float current;
        private Texture2D texture;

        public LifeBar(Vector2 pos, int maxhp)
        {
            this.position = pos;
            this.posbar = new Vector2(26,41);
            max = maxhp;
            setHP(max);
            srcbar = new Rectangle(0, 0, 20, 600);
            srcback = new Rectangle(20, 0, 77, 667);
            srcfor = new Rectangle(97, 0, 57, 667);
            texture = Game1.Instance.magicContentManager.GetTexture("lifebar");

        }


        public void setHP(int hp)
        {
            float div = 600f/ max;
            current = div * hp;
            srcbar.Height = (int)Math.Floor(current);
            srcbar.Y = 600 - srcbar.Height;
            posbar.Y = srcbar.Y+41;
        }


        public void Update(GameTime gametime)
        {
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position , srcback, Color.White);
            spritebatch.Draw(texture, position+posbar, srcbar, Color.White);
            spritebatch.Draw(texture, position, srcfor, Color.White);
        }




    }
}
