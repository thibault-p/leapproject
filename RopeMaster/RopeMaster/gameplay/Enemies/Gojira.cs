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



        private Rectangle srcbody, srcboat, srcsmoker, srcmast, srcwheel, srcmouth;
        private Rectangle srcEye;

        private Vector2 eyePos, mastPos, gojPos, smokePos, wheelPos, wheelOrg, mouthPos;
        private Texture2D texture1, texture2;
        private long timersmoke;

        private long fireCD = 0;
        private int firephase = 0;
        private bool animfire = false;
        private long mouthAnim = 0;
        private int mouthphase = 0;

        private int shootPhase = 1;
        private float rotation = 0f;
        private List<Bubble> bubbleList;
        private bool wheelmoving;
        private float nextangle;
        private int wheelway = 1;
        private int nbBubbles = 0;
        private int nbBubblesphase;
        private long bubbleCD;

        private Vector2 bubbleSpawner;

        private long idletimer = 0;
        private Vector2 mouthShot;

        private RenderTarget2D collider_tgt;
        private Color[] collider;
        private Rectangle rectcollider;



        private int eyephase;
        private long eyeCD;
        private int eyefrm=0;
        public Gojira()
            : base()
        {
            nbBubblesphase = 2;
            bubbleList = new List<Bubble>(nbBubblesphase + 1);
            srcbody = new Rectangle(0, 0, 172, 172);
            srcEye = new Rectangle(340, 119, 57, 65);
            srcboat = new Rectangle(0, 0, 300, 260);
            srcmast = new Rectangle(307, 0, 12, 213);
            srcwheel = new Rectangle(0, 260, 49, 79);
            eyePos = new Vector2(50, 25);
            mastPos = new Vector2(140, 0);
            gojPos = new Vector2(10, 42);
            srcsmoker = new Rectangle(60, 260, 64, 12);
            smokePos = new Vector2(140, 215);
            wheelPos = new Vector2(34, 247);
            wheelOrg = new Vector2(24, 54);
            srcmouth = new Rectangle(172, 0, 68, 60);
            mouthPos = new Vector2(27, 53);
            mouthShot = new Vector2(32, 78);
            texture1 = Game1.Instance.magicContentManager.GetTexture("gojira");
            texture2 = Game1.Instance.magicContentManager.GetTexture("boat");
            this.position = new Vector2(800, 250);
            this.bubbleSpawner = new Vector2(30, 249);
            hp = 50;
            eyeCD = 0;

            collider_tgt = new RenderTarget2D(Game1.Instance.GraphicsDevice,300,260);
            collider = new Color[300* 260];
            rectcollider = new Rectangle((int)this.position.X,(int)this.position.Y,300,260);
        }



        public override void Update(GameTime gametime)
        {
            base.Update(gametime);

            eyeCD += gametime.ElapsedGameTime.Milliseconds;
            if (eyeCD > 200)
            {
                if (eyephase == 0)
                { //crying
                    eyefrm=(eyefrm+1)%3;
                    srcEye.X =172+ eyefrm * 57;

                }
                if (eyephase == 1)
                {
                    eyefrm = (eyefrm+1)%2; 
                    srcEye.X =343 + eyefrm*57;
                }
                if (eyephase == 2)
                {
                 
                    srcEye.X =  457;
                }
                eyeCD = 0;
            }

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
                case 2: breathFire(gametime);
                    break;
                default: Idle(gametime);
                    break;
            }
        }

        private void breathFire(GameTime gameTime)
        {

            if (animfire)
            {
                mouthAnim += gameTime.ElapsedGameTime.Milliseconds;
                if (mouthAnim > 100)
                {
                    var rand = Game1.Instance.randomizator;
                    Game1.Instance.particuleManager.AddParticule(new Smoke(this.position + this.mouthShot+this.gojPos, rand.GetRandomTrajectory(200, MathHelper.ToRadians(200), MathHelper.ToRadians(240)), rand.GetRandomFloat(0.4f, 0.7f), Color.White, false));
                    mouthAnim = 0;
                    srcmouth.X += 68 * ((mouthphase % 2 == 0) ? 1 : -1);
                    if ((mouthphase > 0 & mouthphase < 6 &  srcmouth.X == 308) ||(srcmouth.X == 444)) mouthphase++;
                    if (mouthphase > 6 & srcmouth.X == 172) animfire = false;
                    firephase = 0;

                }
                return;
            }

            fireCD += gameTime.ElapsedGameTime.Milliseconds;
            if (fireCD > 400)
            {
         
                var nb = (int)(firephase / 8) + 3;
                if (nb == 5)
                {
                    changePhase();
                    return;
                }
                var offset = (2f * Math.PI / 3) / (float)(nb - 1);
                var angle = Math.PI / 2 + Math.PI / 6;
                var rand = Game1.Instance.randomizator;
                var v = Vector2.One * 10;
                for (int i = 0; i < nb; i++)
                {
                    Game1.Instance.particuleManager.AddParticule(new Smoke(this.position + this.mouthShot + this.gojPos+v, rand.GetRandomTrajectory(200, MathHelper.ToRadians(200), MathHelper.ToRadians(240)), rand.GetRandomFloat(0.7f, 1f), Color.White, false));
                    var s =new Shot(this.position+gojPos+mouthShot, new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle))*0.5f, 200,0, false, 1, 2);
                    s.angle = (float)(angle-Math.PI);
                    Game1.Instance.shotManager.AddShotEnemy( s);
                    angle += offset;
                }
                firephase++;
             
                fireCD = 0;

            }

        }



        public bool IsPointPerfectColliding(Vector2 p)
        {
            if(rectcollider.Contains((int)p.X,(int)p.Y)){
                var x = p.X - rectcollider.X;
                var y = p.Y - rectcollider.Y;
                return collider[(int)(x + y*300)]!=Color.Green;
            }
            return false;
        }

        public void Idle(GameTime gameTime)
        {
            idletimer += gameTime.ElapsedGameTime.Milliseconds;
            if (idletimer >= 3000 - Game1.Instance.difficulty * 300)
            {
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
                    nextangle = rand.GetRandomFloat(0 + Math.PI / 64, Math.PI - Math.PI / 64);
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
                    var p = this.position + bubbleSpawner;

                    Bubble b = new Bubble(p, 0, v * -500);
                    Game1.Instance.enemyManager.enemiestoAdd.Add(b);
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

        public override void setPosition(int x, int y)
        {
            base.setPosition(x, y);
            this.rectcollider.X = (int)this.position.X;
            this.rectcollider.Y = (int)this.position.Y;
        }
        public override void setPosition(Vector2 pos)
        {
            base.setPosition(pos);
            this.rectcollider.X = (int)this.position.X;
            this.rectcollider.Y = (int)this.position.Y;
        }

        private void changePhase()
        {
            shootPhase = (shootPhase + 1) % 3;
            if (shootPhase == 0)
            {
                //drop bubble
                eyephase = 2;
                foreach (Bubble b in bubbleList)
                {
                    b.drop(500);
                }
                bubbleList.Clear();
            }
            else if (shootPhase == 1)
            {

                wheelmoving = false;
                nbBubbles = 0;
            }
            else if (shootPhase == 2)
            {
                //reinit phase
                eyephase = 1;
                animfire = true;
                mouthAnim = 0;
                mouthphase = 0;
                srcmouth.X = 172;
                mouthphase = 0;
                fireCD = 0;
            }

   
            Console.WriteLine("changePhase " + shootPhase);

        }


        public override void Draw(SpriteBatch spritebatch)
        {            
            spritebatch.Draw(texture2, this.position + mastPos, srcmast, Color.White);
            spritebatch.Draw(texture1, this.position + gojPos, srcbody, Color.White);
            spritebatch.Draw(texture2, this.position, srcboat, Color.White);
            spritebatch.Draw(texture2, this.position + smokePos, srcsmoker, Color.White);
            spritebatch.Draw(texture2, this.position + wheelPos, srcwheel, Color.White, -rotation, wheelOrg, 1, SpriteEffects.None, 0);
            if (animfire) spritebatch.Draw(texture1, this.position + gojPos + mouthPos, srcmouth, Color.White);
            spritebatch.Draw(texture1, this.position + gojPos + eyePos, srcEye, Color.White);
            spritebatch.Draw(collider_tgt, rectcollider, Color.White);
        }

        public void DrawCollider(     SpriteBatch spritebatch){
            //draw colider
            Game1.Instance.GraphicsDevice.SetRenderTarget(collider_tgt);
            Game1.Instance.GraphicsDevice.Clear(Color.Green);
            spritebatch.Begin();
            spritebatch.Draw(texture2,  mastPos, srcmast, Color.White);
            spritebatch.Draw(texture1, gojPos, srcbody, Color.White);
            spritebatch.Draw(texture2, Vector2.Zero, srcboat, Color.White);
            if (animfire) spritebatch.Draw(texture1, gojPos + mouthPos, srcmouth, Color.White);
            spritebatch.End();
            Game1.Instance.GraphicsDevice.SetRenderTarget(null);
            collider_tgt.GetData(collider);

        }






    }
}
