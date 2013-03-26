using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RopeMaster.Core;
using Glitch.Engine.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RopeMaster.gameplay
{
    [TextureContent(AssetName = "bonusmaster", AssetPath = "gfx/sprites/bonusmaster")]
    public class BonusMaster
    {
        private enum Animation{ popup=0, moving};
        private Texture2D texture;
        private bool playing;
        private Animation stateanim;
        private int currentAnim;
        private long time;
        private Vector2 position;
        private Rectangle srcRect;


        public BonusMaster()
        {
            texture = Game1.Instance.magicContentManager.GetTexture("bonusmaster");
            srcRect = new Rectangle(0, 0, 128, 128);
            position = new Vector2(0, Game1.Instance.GraphicsDevice.Viewport.Height );
            playing = true;
        }




        public void playAnim(int anim)
        {
            srcRect.Y = 128 * anim;
            playing = true;
            position = new Vector2(0, Game1.Instance.GraphicsDevice.Viewport.Height);
            stateanim = Animation.popup;
        }



        public void Update(GameTime gameTime)
        {
            time+=gameTime.ElapsedGameTime.Milliseconds;
            if(stateanim==Animation.popup){
                if (time > 10)
                {
                    position.Y--;
                    time = 0;
                    if (position.Y >= Game1.Instance.GraphicsDevice.Viewport.Height - 128)
                    {
                        stateanim = Animation.moving;

                    }
                }
                




            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (playing)
            {
                spriteBatch.Draw(texture, position, srcRect, Color.White);
            }

        }

    }
}
