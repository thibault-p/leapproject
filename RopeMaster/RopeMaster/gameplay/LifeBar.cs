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
    [TextureContent(AssetName = "lifebar", AssetPath = "gfx/lifebar")]
    public class LifeBar
    {

        private Vector2 position;
        private Rectangle srcbar;
        private int max;
        private float current;
        private Texture2D texture;

        public LifeBar(Vector2 pos, int maxhp)
        {
            this.position = pos;
            srcbar = new Rectangle(0, 0, 20, 600);
            texture = Game1.Instance.magicContentManager.GetTexture("lifebar");

        }


        public void setHP(int hp)
        {
            current = hp;
            float div = 600/ max;
        }


        public void Update(GameTime gametime)
        {
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position, srcbar, Color.White);
        }




    }
}
