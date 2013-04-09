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
using RopeMaster.Core;
using Glitch.Engine.Content;



namespace RopeMaster.gameplay.Enemies
{

    [TextureContent(AssetName = "gojira", AssetPath = "gfx/sprites/gojira")]
    [TextureContent(AssetName = "boat", AssetPath = "gfx/sprites/boat")]
    public class Gojira : Enemy
    {


        private Rectangle srcbody,srcboat,srcsmoker,srcmast, srcwheel;
        private Rectangle srcEye;

        private Vector2 eyePos, mastPos,gojPos;
        private Texture2D texture1, texture2;






        public Gojira()
        {
            srcbody = new Rectangle(0, 0, 172, 172);
            srcEye = new Rectangle(172, 119,57,65);
            srcboat = new Rectangle(0,0,300,260);
            srcmast = new Rectangle(307, 0, 12, 213);
            eyePos = new Vector2(56,25);
            mastPos = new Vector2(140, 0);
            gojPos = new Vector2(10, 42);
            texture1 = Game1.Instance.magicContentManager.GetTexture("gojira");
            texture2 = Game1.Instance.magicContentManager.GetTexture("boat");
        }



        public override void Update(GameTime gametime)
        {
        }

        public override void Draw(SpriteBatch spritebatch)
        {

            spritebatch.Draw(texture2, this.position + mastPos, srcmast, Color.White);
            spritebatch.Draw(texture1, this.position+gojPos, srcbody, Color.White);
            spritebatch.Draw(texture1, this.position + gojPos+eyePos, srcEye, Color.White);
            spritebatch.Draw(texture2,this.position, srcboat, Color.White );


        }




    }
}
