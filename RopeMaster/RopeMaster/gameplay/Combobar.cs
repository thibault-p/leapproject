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



namespace RopeMaster.gameplay
{
    [TextureContent(AssetName = "combobar", AssetPath = "gfx/sprites/combobar")]
    public class Combobar
    {

        public int length;
        public long combo;
        private long timer_combo;
        private Texture2D texture;
        private Vector2 position, posunit,posdozen, pospart;
        private Rectangle srcMain, srcunit, srcdozen, srcpart, dstbar,srcbar;

        private BonusMaster bonusmaster;



        public Combobar(int len)
        {
            length = len;
            combo = 0;
            texture = Game1.Instance.magicContentManager.GetTexture("combobar");
            srcMain = new Rectangle(0, 0, 92, 50);
            srcunit = new Rectangle(117, 0, 18, 32);
            srcdozen = new Rectangle(117, 0, 18, 32);
            posunit = new Vector2(38, 11);
            posdozen = new Vector2(18, 11);
            srcpart = new Rectangle(93,0,22, 52);
            pospart = new Vector2(92, 0);
            this.position = new Vector2(0, 720 - srcMain.Height);
            dstbar = new Rectangle((int)(this.position.X+pospart.X), (int)(this.position.Y),0 ,52);
            srcbar = new Rectangle(0, 50, 1, 52);
            bonusmaster = new BonusMaster();
        }


        public void AddCombo(int c)
        {
            combo = Math.Min(combo + c, length * 100);
        }


        public void AddLength()
        {
            modifyLength(1);
        }


        public void RemoveLenght()
        {
            modifyLength(-1);
        }

        public int getMultiplicator()
        {
            return (int)Math.Floor(combo / 100f);
        }


        private void modifyLength(int way)
        {
            var nl = length + way;
            if (nl < 1) return;
            if (combo > nl * 100)
            {
                combo = nl * 100;
            }
            length = nl;
        }


        public void Update(GameTime gametime)
        {
            combo++;
            timer_combo += gametime.ElapsedGameTime.Milliseconds;
            if (timer_combo > 1000)
            {
                //combo counter decrease
                if (timer_combo % 50 == 0)
                {
                    combo = Math.Max(0,combo-1);
                    
                }
            }
            //update roll
            var m = getMultiplicator();
            var unit = m % 10;
            if (srcunit.Y != (unit) * 35)
            {
                srcunit.Y += ((srcunit.Y > (unit) * 35) ? 1 : 1);

            }
            var dozen = (int)Math.Floor(m/10f);
            if (srcdozen.Y != (dozen) * 35)
                srcdozen.Y += ((srcdozen.Y > (dozen) * 35) ? 1 : 1);

            dstbar.Width = (int)((22f / 100f) * combo);
            bonusmaster.Update(gametime);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            bonusmaster.Draw(spritebatch);
            spritebatch.Draw(texture,this.position, srcMain,Color.White);
            spritebatch.Draw(texture, this.position + this.posunit, srcunit, Color.White); 
            spritebatch.Draw(texture, this.position + this.posdozen, srcdozen, Color.White); 
            //draw level
            spritebatch.Draw(texture, dstbar,srcbar,Color.White);


            var p = this.pospart;
            for (int i = 0; i < length; i++)
            {
                spritebatch.Draw(texture, this.position + p, srcpart, Color.White);
                p.X+= srcpart.Width;
            }

        }




    }
}
