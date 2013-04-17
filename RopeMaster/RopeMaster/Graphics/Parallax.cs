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
        float[] layerspeed = { 0.1f, 0.2f, 0.3f };
        Texture2D[] textures;
        Rectangle[] source;
        Vector2[] sourceloc;
        Vector2 move;
        private int stuck = 0;

        public float Speed { get; set; }



        public Parallax()
        {
            textures = new Texture2D[NB_LAYERS];
            sourceloc = new Vector2[NB_LAYERS];
            source = new Rectangle[NB_LAYERS];
            for (int i = 0; i < NB_LAYERS; i++)
            {
                textures[i] = Game1.Instance.magicContentManager.GetTexture("background_" + i);
                source[i] = new Rectangle(0, 0, 400, (int)(400 / 1.77f));
                sourceloc[i] = Vector2.Zero;
            }
            move = Vector2.Zero;
            Speed = 8;
        }


        public void Update(GameTime gameTime)
        {
            stuck = 0;
            for (int i = 0; i < NB_LAYERS; i++)
            {
                sourceloc[i].X += move.X * layerspeed[i] * Speed;
                source[i].X = (int)(sourceloc[i].X);

                if (move.Y != 0)
                {
                    sourceloc[i].Y += move.Y * layerspeed[i] * Speed;
                    if (sourceloc[i].Y < 0)
                    {
                        sourceloc[i].Y = 0;
                        stuck=-1;
                    }
                    else if(sourceloc[i].Y>(textures[i].Height - source[i].Height - 1)){
                            stuck=1;
                            sourceloc[i].Y =(textures[i].Height - source[i].Height - 1 );
                    }

                    source[i].Y = (int)(sourceloc[i].Y);

                }

            }
            move = Vector2.Zero;
        }

        public void moveVertical(int way)
        {

            if (stuck == way) way = 0;
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
