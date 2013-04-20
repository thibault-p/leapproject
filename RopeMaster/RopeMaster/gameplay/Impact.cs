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


           public Impact(Vector2 position)
               : base(position,Vector2.Zero)
           {
               timer = 0;
               texture = Game1.Instance.magicContentManager.GetTexture("impact");
               origin = new Vector2(8, 8);
           }

           public override void Update(GameTime gameTime)
           {
               timer += gameTime.ElapsedGameTime.Milliseconds;
               base.Update(gameTime);
           }

           public override bool exterminate()
           {
               return timer > 200;
           }

           public override void Draw(SpriteBatch spritebatch)
           {
               spritebatch.Draw(texture, position, Color.White);
           }




    }
}
