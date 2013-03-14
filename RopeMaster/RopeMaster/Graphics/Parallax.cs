using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glitch.Engine.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace RopeMaster.Graphics
{
    [TextureContent(AssetName = "background_0", AssetPath = "gfx/background/background_0")]
    [TextureContent(AssetName = "background_1", AssetPath = "gfx/background/background_1")]
    [TextureContent(AssetName = "background_2", AssetPath = "gfx/background/background_2")]
    class Parallax
    {
        const int NB_LAYERS = 3;
        float[] layerspeed = { 1,2, 3 };
        Texture2D[] textures;
        Rectangle[] source;

        Vector2 move;

        public float Speed { get; set; }



        public Parallax()
        {
            textures = new Texture2D[NB_LAYERS];

            source = new Rectangle[NB_LAYERS];
            for (int i = 0; i < NB_LAYERS; i++)
            {
                textures[i] = Game1.Instance.magicContentManager.GetTexture("background_"+i);
                source[i] = new Rectangle(0, 0, 400, (int)(400/1.77f));
            }
            move = Vector2.Zero;
            Speed = 1;
        }


        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < NB_LAYERS; i++)
            {
        
                source[i].X += (int)(move.X * layerspeed[i] * Speed);
                if (move.Y != 0)
                {
                    source[i].Y += (int)(move.Y * layerspeed[i] * Speed);
                    source[i].Y = (int)Math.Min(textures[i].Height - source[i].Height - 1, Math.Max(0, source[i].Y ));
                }
   
            }
            move = Vector2.Zero;
        }

        public void moveVertical(int way)
        {
            move.Y = way;
        }


        public void moveHorizontal(int way)
        {
            move.X = way;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate,
            BlendState.AlphaBlend, SamplerState.LinearWrap,
            null,
            null,
            null, Microsoft.Xna.Framework.Matrix.Identity);

            for (int i = 0; i < NB_LAYERS; i++)
            {
                spriteBatch.Draw(textures[i], Game1.Instance.Screen, source[i], Color.White);
            }
            spriteBatch.End();


        }





    }
}
