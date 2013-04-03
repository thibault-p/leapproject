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
using RopeMaster.gameplay;



namespace RopeMaster.gameplay.Enemies
{
    [TextureContent(AssetName = "bogus", AssetPath = "gfx/sprites/bogus")]
    public class EnemyBogus : Enemy
    {

        public EnemyBogus(Vector2 _postion)
            : base(_postion, 10)
        {
            base.nbFrame= new int[] {1,2,3};
            base.velocity = Vector2.UnitX * -100;
        }
        public EnemyBogus()
            : base(Vector2.Zero, 10)
        {
            base.nbFrame = new int[] { 1, 2, 3 };
            base.velocity = Vector2.UnitX * -100;
            texture = Game1.Instance.magicContentManager.GetTexture("bogus");
            srcBox = new Rectangle(0, 0, 64, 64);
        }

     





    }
}
