using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Glitch.Engine.Content;
using Microsoft.Xna.Framework;
using RopeMaster.Core;

namespace RopeMaster.gameplay.Enemies
{

    [TextureContent(AssetName = "bubble", AssetPath = "gfx/sprites/bubble")]
    public class Bubble : Enemy
    {
        private int state;
        private Texture2D texture;
        private int maxX;
        private Color color;
        private bool moving;
        private long timerhit;

        public Bubble(Vector2 pos, int maxX, Vector2 dir)
            : base()
        {
            Initialyze(pos,dir,maxX);

        }

        public Bubble()
            : base()
        {
            Initialyze(Vector2.Zero,Vector2.Zero, 0);

        }



        protected void Initialyze(Vector2 pos , Vector2 dir, int maxX)
        {
            texture = Game1.Instance.magicContentManager.GetTexture("bubble");
            state = 0;
            this.position = pos;
            this.velocity = dir;
            this.maxX = maxX;
            srcBox = new Rectangle(0, 0, 64, 64);
            this.origin = new Vector2(srcBox.Width / 2, srcBox.Height / 2);
            color = Color.White;
            this.hitbox = new SphereBox(this.origin + position, 32);
            timerhit = 0;
        }
        public override void Update(GameTime gameTime)
        {
            if (moving) base.Update(gameTime);
            moving = !(this.position.X <= maxX);
            this.hitbox.setPosition(this.position + origin);







        }





        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, this.position, srcBox, color, 0, origin, 1, SpriteEffects.None, 0);
        }



    }
}
