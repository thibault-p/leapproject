using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RopeMaster.Core;
using Glitch.Engine.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RopeMaster.gameplay
{
    [TextureContent(AssetName = "player", AssetPath = "gfx/sprites/Player1")]
    public class Player : Entity
    {
        private Rectangle source;
        private Texture2D texture;

        public Player()
            : base()
        {
            texture = Game1.Instance.magicContentManager.GetTexture("player");
            source = new Rectangle(0,0,92,128);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, this.getPosition(), source, Color.White); 
        }
    }
}
