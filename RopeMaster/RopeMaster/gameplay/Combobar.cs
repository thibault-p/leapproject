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
        public int combo;
        private long timer_combo;
        private Texture2D texture;
        private Vector2 position, posunit,posdozen, pospart;
        private Rectangle srcMain, srcunit, srcdozen, srcpart, dstbar,srcbar;

        private BonusMaster bonusmaster;
        private string description;
        private long desctimer;


        public Combobar(int len)
        {
            length = len;
            combo = 0;
            description = "";
            texture = Game1.Instance.magicContentManager.GetTexture("combobar");
            srcMain = new Rectangle(0, 0, 92, 50);
            srcunit = new Rectangle(117, 35, 18, 32);
            srcdozen = new Rectangle(117, 35, 18, 32);
            posunit = new Vector2(38, 12);
            posdozen = new Vector2(18, 12);
            srcpart = new Rectangle(93,0,22, 52);
            pospart = new Vector2(92, 0);
            this.position = new Vector2(0, 720 - srcMain.Height);
            dstbar = new Rectangle((int)(this.position.X+pospart.X), (int)(this.position.Y),0 ,52);
            srcbar = new Rectangle(0, 50, 1, 52);
            bonusmaster = new BonusMaster();
            desctimer = 0;
        }


        public void AddCombo(int c, string desc)
        {
            combo =(int) Math.Min(combo + c, length * 100);
            timer_combo = 0;
            desctimer = 0;
            description = desc;
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
            return (int)Math.Floor(combo / 100f)+1;
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
            timer_combo += gametime.ElapsedGameTime.Milliseconds;
            if (timer_combo > 1000)
            {
                //combo counter decrease
                    combo = Math.Max(0,combo-1);
                    description = "";
 
            }
            //update roll
            var m = getMultiplicator();
            var unit = m % 10;
            if (unit == 9)
            {
                if (srcunit.Y ==  35)
                { //we are on 0
                    srcunit.Y = 35*11;
                }
                
            }else if(unit==0){
                if (srcunit.Y == 10 * 35)
                { //we are on 9
                    srcunit.Y = 0;
                }
            }
            if (srcunit.Y != (unit+1) * 35)
            {
                srcunit.Y += ((srcunit.Y > (unit+1) * 35) ? -1 : 1);

            }

            //DOZEN////////////////////////////////////////////////////////
            var dozen = (int)Math.Floor(m/10f);
            if (dozen == 9)
            {
                if (srcdozen.Y == 35)
                { //we are on 0
                    srcdozen.Y = 35 * 11;
                }

            }
            else if (dozen == 0)
            {
                if (srcdozen.Y == 10 * 35)
                { //we are on 9
                    srcdozen.Y = 0;
                }
            }
            if (srcdozen.Y != (dozen+1) * 35)
                srcdozen.Y += ((srcdozen.Y > (dozen+1) * 35) ? -1 : 1);




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
            p.X+=15;
            p.Y += 25;
            spritebatch.DrawString(Game1.Instance.magicContentManager.Font, description,this.position  +p, Color.White);
        }




    }
}
