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
    [TextureContent(AssetName = "impact", AssetPath = "gfx/sprites/impact")]
    public class Impact : Entity
    {

        private Texture2D texture;
        private long timer;
        private int frame;
        private float rotation;
        private Rectangle srcrect;

        public Impact(Vector2 position, float rot)
            : base(position, Vector2.Zero)
        {
            timer = 0; 
            texture = Game1.Instance.magicContentManager.GetTexture("impact");
            origin = new Vector2(8, 16);
            srcrect = new Rectangle(0, 0, 16, 16);
            frame = 0;
            rotation = rot;
        }

        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer > 20)
            {
                frame++;
                srcrect.X = frame * 16;
                timer = 0;
            }
            base.Update(gameTime);
        }

        public override bool exterminate()
        {
            return frame > 3;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, position,srcrect,Color.White,rotation,origin,1,SpriteEffects.None,0);
        }




    }
}
