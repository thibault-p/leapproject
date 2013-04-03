using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RopeMaster.Core;
using Glitch.Engine.Content;
using Microsoft.Xna.Framework;

namespace RopeMaster.gameplay.Enemies
{
       [TextureContent(AssetName = "onyx", AssetPath = "gfx/sprites/onyx")]
    public class OnyxEnemy : Enemy
    {



           public OnyxEnemy()
            : base(Vector2.Zero, 10)
        {
            base.nbFrame = new int[] { 1, 2, 3 };
            base.velocity = Vector2.UnitX * -50;
            texture = Game1.Instance.magicContentManager.GetTexture("onyx");
            srcBox = new Rectangle(0, 0, 128, 128);
            base.hitbox = new SphereBox(this.position, 62);
            base.origin = new Vector2(64,64);
        }

           public override void Update(GameTime gameTime)
           {
               base.Update(gameTime);

               setPosition(position);
           }

    
    }
}
