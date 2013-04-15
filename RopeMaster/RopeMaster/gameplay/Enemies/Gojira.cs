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
using Glitch.Graphics.particules;



namespace RopeMaster.gameplay.Enemies
{

    [TextureContent(AssetName = "gojira", AssetPath = "gfx/sprites/gojira")]
    [TextureContent(AssetName = "boat", AssetPath = "gfx/sprites/boat")]
    public class Gojira : Enemy
    {

        private const long TIMER_SMOKE = 300;



        private Rectangle srcbody, srcboat, srcsmoker, srcmast, srcwheel;
        private Rectangle srcEye;

        private Vector2 eyePos, mastPos, gojPos, smokePos, wheelPos, wheelOrg;
        private Texture2D texture1, texture2;
        private long timersmoke;

        private Vector2 bubbleSpawner;


        private int shootPhase = 1;
        private float rotation = 0f;
        private List<Bubble> bubbleList;
        private bool wheelmoving;
        private float nextangle;
        private int wheelway = 1;
        private int nbBubbles=0;
        private int nbBubblesphase;
        private long bubbleCD;
       
        private long idletimer=0; 

        public Gojira()
            : base()
        {
            nbBubblesphase = 20;
            bubbleList = new List<Bubble>(nbBubblesphase+1);
         
            srcbody = new Rectangle(0, 0, 172, 172);
            srcEye = new Rectangle(172, 119, 57, 65);
            srcboat = new Rectangle(0, 0, 300, 260);
            srcmast = new Rectangle(307, 0, 12, 213);
            srcwheel = new Rectangle(0, 260, 49, 79);
            eyePos = new Vector2(56, 25);
            mastPos = new Vector2(140, 0);
            gojPos = new Vector2(10, 42);
            srcsmoker = new Rectangle(60, 260, 64, 12);
            smokePos = new Vector2(140, 215);
            wheelPos = new Vector2(34, 247);
            wheelOrg = new Vector2(24,54);
            texture1 = Game1.Instance.magicContentManager.GetTexture("gojira");
            texture2 = Game1.Instance.magicContentManager.GetTexture("boat");
            this.position = new Vector2(400,250);

            this.bubbleSpawner = new Vector2(30, 249);
        
        }



        public override void Update(GameTime gametime)
        {
            base.Update(gametime);

            timersmoke += gametime.ElapsedGameTime.Milliseconds;
            if (timersmoke % 60 == 0) smokePos.X++;
            if (timersmoke >= TIMER_SMOKE)
            {
                var rand = Game1.Instance.randomizator;
                Game1.Instance.particuleManager.AddParticule(new Smoke(this.position + this.smokePos, rand.GetRandomTrajectory(200, MathHelper.ToRadians(180), MathHelper.ToRadians(190)), rand.GetRandomFloat(0.4f, 0.7f), Color.White, false));
                timersmoke = 0;
                smokePos.X = 66;
            }

            switch (shootPhase)
            {
                case 1: ShootBubble(gametime);
                    break;

                default: Idle(gametime);
                    break;
            }

        }



        public void Idle(GameTime gameTime)
        {
            idletimer+=gameTime.ElapsedGameTime.Milliseconds;
            if(idletimer>= 3000-Game1.Instance.difficulty*300){
                changePhase();
                
                
            }

        }

        public void ShootBubble(GameTime gameTime)
        {
            
            if (!wheelmoving)
            {
                bubbleCD += gameTime.ElapsedGameTime.Milliseconds;
                if (bubbleCD > 200)
                {
                    var rand = Game1.Instance.randomizator;
                    nextangle = rand.GetRandomFloat(0 + Math.PI / 64, Math.PI + Math.PI / 64);
                    wheelmoving = true;
                    wheelway = (rotation > nextangle) ? -1 : 1;
                }
            }
            else
            {

                if (((wheelway == 1) && (rotation >= nextangle)) || ((wheelway == -1) && (rotation <= nextangle)))
                {
                    wheelmoving = false;
                    nbBubbles++;
                    Vector2 v = new Vector2((float)Math.Sin(rotation), (float)Math.Cos(rotation));
                    var p= this.position + bubbleSpawner;
                    
                    Bubble b = new Bubble(p, 0, v*-500);
                    Game1.Instance.enemyManager.Add(b);
                    this.bubbleList.Add(b);
                    bubbleCD = 0;
                    if (nbBubbles >= nbBubblesphase)
                    {
                        nbBubbles = 0;
                        changePhase();
                    }
                }
                else
                {
                    rotation += wheelway * 0.05f;
                }
            }

            




        }



        private void changePhase(){
            shootPhase= (shootPhase+1)%2;
            if (shootPhase==0)
            {
                //drop bubble
                foreach (Bubble b in bubbleList)
                {
                    b.drop(500);
                }
                bubbleList.Clear();
            }
            Console.WriteLine("changePhase " + shootPhase);

        }


        public override void Draw(SpriteBatch spritebatch)
        {

            spritebatch.Draw(texture2, this.position + mastPos, srcmast, Color.White);
            spritebatch.Draw(texture1, this.position + gojPos, srcbody, Color.White);
            spritebatch.Draw(texture1, this.position + gojPos + eyePos, srcEye, Color.White);
            spritebatch.Draw(texture2, this.position, srcboat, Color.White);
            spritebatch.Draw(texture2, this.position + smokePos, srcsmoker, Color.White);
            spritebatch.Draw(texture2, this.position + wheelPos, srcwheel, Color.White, -rotation, wheelOrg, 1, SpriteEffects.None, 0);

        }




    }
}
