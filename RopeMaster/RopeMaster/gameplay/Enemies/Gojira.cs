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
    [TextureContent(AssetName = "gojira_hitbox", AssetPath = "gfx/sprites/gojira_hitbox")]
    [TextureContent(AssetName = "gojira", AssetPath = "gfx/sprites/gojira")]
    [TextureContent(AssetName = "boat", AssetPath = "gfx/sprites/boat")]
    [TextureContent(AssetName = "gojira_normal", AssetPath = "gfx/sprites/gojira_normalmap")]
    public class Gojira : Enemy
    {

        private const long TIMER_SMOKE = 300;



        private Rectangle srcbody, srcboat, srcsmoker, srcmast, srcwheel, srcmouth, srctongue;
        private Rectangle srcEye;

        private Vector2 eyePos, mastPos, gojPos, smokePos, wheelPos, wheelOrg, mouthPos, tonguePos, hitboxpos = new Vector2(42, 109);
        private Texture2D texture1, texture2, maphitbox;
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
        private Color[] collider;
        private Rectangle rectcollider;
        private Texture2D normaltex;
        private Color[] normalMap;

        private Vector2 shake;
        private long rushTimer;
        private int rushstate;
        public Hitbox tonguebox;
        public long tonguetimer;

        private long timereye;
        private bool eyeclosed = false;

        private int eyephase;
        private long eyeCD;
        private int eyefrm = 0;


        Color color = Color.White;

        private int tonguephase;
        public bool dead = false;



        public bool ismoving;

        



        public Gojira()
            : base()
        {
            nbBubblesphase = 10;
            hp = 8;
            dead = false;
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
            tonguePos = new Vector2(9, 74);
            srctongue = new Rectangle(1, 173, 63, 55);
            texture1 = Game1.Instance.magicContentManager.GetTexture("gojira");
            texture2 = Game1.Instance.magicContentManager.GetTexture("boat");
            maphitbox = Game1.Instance.magicContentManager.GetTexture("gojira_hitbox");
            normaltex = Game1.Instance.magicContentManager.GetTexture("gojira_normal");
            this.position = new Vector2(800, 150);
            this.bubbleSpawner = new Vector2(30, 249);
            eyephase = 2;
            ismoving = true;
            tonguetimer = 0;
            eyeCD = 0;
            this.velocity = new Vector2(0, 100);
            collider = new Color[300 * 256];
            normalMap = new Color[300 * 256];
            generateMap(true);
            rectcollider = new Rectangle((int)this.position.X, (int)this.position.Y, 300, 256);

        }





        public override void Update(GameTime gametime)
        {
            if (shootPhase != 4 && ismoving)
            {
                if (this.position.Y < 120)
                {
                    this.velocity = new Vector2(0, 50);

                }else if (this.position.Y > 400)
                {
                    this.velocity = new Vector2(0, -50);

                }

            }




            base.Update(gametime);
            timereye += gametime.ElapsedGameTime.Milliseconds;
            if (!eyeclosed && timereye > 1200)
            {
                eyeclosed = eyephase == 2;
                timereye = 0;
            }
            else if (eyeclosed && timereye > 100)
            {
                eyeclosed = false;
                timereye = 0;
            }


            this.rectcollider.X = (int)this.position.X;
            this.rectcollider.Y = (int)this.position.Y;
            eyeCD += gametime.ElapsedGameTime.Milliseconds;
            if (eyeCD > 200)
            {
                if (eyephase == 0)
                { //crying
                    eyefrm = (eyefrm + 1) % 3;
                    srcEye.X = 172 + eyefrm * 57;

                }
                if (eyephase == 1)
                {
                    eyefrm = (eyefrm + 1) % 2;
                    srcEye.X = 343 + eyefrm * 57;
                }
                if (eyephase == 2)
                {

                    srcEye.X = 457;
                }
                eyeCD = 0;
            }
            if (tonguebox != null)
            {
                tonguebox.setPosition(position + gojPos + hitboxpos);
            }


            dead = (hp <= 0);
            if (dead)
                velocity = new Vector2(-1,1)*50;

            timersmoke += gametime.ElapsedGameTime.Milliseconds;
            if (timersmoke % 60 == 0) smokePos.X++;
            if (timersmoke >= TIMER_SMOKE)
            {
                var rand = Game1.Instance.randomizator;
                Gamescreen.Instance.particuleManager.AddParticule(new Smoke(this.position + this.smokePos, rand.GetRandomTrajectory(200, MathHelper.ToRadians(180), MathHelper.ToRadians(190)), rand.GetRandomFloat(0.4f, 0.7f), Color.White, false));
                timersmoke = 0;
                smokePos.X = 66;
            }
            switch (shootPhase)
            {
                case 1: ShootBubble(gametime);
                    break;
                case 2: breathFire(gametime);
                    break;
                case 3: warmdown(gametime);
                    break;
                case 4: rush(gametime);
                    break;
                default: Idle(gametime);
                    break;
            }
        }







        private void rush(GameTime gametime)
        {

            if (rushstate == 0) //charging
            {
                rushTimer += gametime.ElapsedGameTime.Milliseconds;
                if (rushTimer % 20 == 0)
                {
                    this.position -= shake;
                    shake = Game1.Instance.randomizator.GetRandomVector2(0, 0, -2, 2);
                    Gamescreen.Instance.particuleManager.AddParticule(new Smoke(this.position + this.smokePos, Game1.Instance.randomizator.GetRandomTrajectory(200, MathHelper.ToRadians(180), MathHelper.ToRadians(190)), Game1.Instance.randomizator.GetRandomFloat(0.6f, 0.1f), Color.White, false));

                    this.position += shake;

                }
                if (rushTimer > 3000)
                {
                    var a = Math.Atan2(Gamescreen.Instance.player.getShotSource().Y - (this.position.Y + this.mouthPos.Y), Gamescreen.Instance.player.getShotSource().X - (this.position.X + this.mouthPos.X));
                    this.velocity = new Vector2(-700,0);
                    rushstate = 1;// go left
                    rushTimer = 0;
                }
            }
            else if (rushstate == 1)
            {
                if (this.position.X <= 50)
                {
                    this.velocity *= -1;
                    rushstate = 2;
                }


            }
            else
            {


                if (this.position.X >= 800)
                {
                    this.velocity = Vector2.Zero;
                    changePhase();
                }
            }




        }


        private void generateMap(bool open)
        {
            var src = new Rectangle((open) ? 0 : 300, 0, 300, 256);
            maphitbox.GetData(0, src, collider, 0, 300 * 256);
            normaltex.GetData(0, src, normalMap, 0, 300 * 256);
        }


        public override void hit(int damage)
        {
            this.hp -= damage;
        }


        private void warmdown(GameTime gameTime)
        {
            tonguetimer += gameTime.ElapsedGameTime.Milliseconds;

  
            if (tonguetimer > 300)
            {
                tonguetimer = 0;
                tonguephase++;
                srctongue.X += srctongue.Width;
                if (srctongue.X > 100) srctongue.X = 0;
                if (dead)
                for (int i = 0; i < 3; i++)
                    Gamescreen.Instance.particuleManager.AddParticule(new Blood(this.position + this.mouthShot + this.gojPos, Game1.Instance.randomizator.GetRandomTrajectory(200, MathHelper.ToRadians(170), MathHelper.ToRadians(200)), Game1.Instance.randomizator.GetRandomFloat(0.6f, 1f), Color.White, false));
            }

            if (!dead && tonguephase > 30)
            {
                changePhase();
            }

        }



        private void breathFire(GameTime gameTime)
        {

            if (animfire)
            {
                mouthAnim += gameTime.ElapsedGameTime.Milliseconds;
                if (mouthAnim > 200)
                {
                    var rand = Game1.Instance.randomizator;
                    Gamescreen.Instance.particuleManager.AddParticule(new Smoke(this.position + this.mouthShot + this.gojPos, rand.GetRandomTrajectory(200, MathHelper.ToRadians(200), MathHelper.ToRadians(240)), rand.GetRandomFloat(0.4f, 0.7f), Color.White, false));
                    mouthAnim = 0;
                    srcmouth.X += 68 * ((mouthphase % 2 == 0) ? 1 : -1);
                    if ((mouthphase > 0 & mouthphase < 6 & srcmouth.X == 308) || (srcmouth.X == 444)) mouthphase++;
                    if (mouthphase > 6 & srcmouth.X == 172)
                    {
                        animfire = false;
                        generateMap(true);
                    }
                    firephase = 0;

                }
                return;
            }

            fireCD += gameTime.ElapsedGameTime.Milliseconds;
            if (fireCD > 400)
            {

                var nb = (int)(firephase / 8) + 4;
                if (nb == 6)
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
                    Gamescreen.Instance.particuleManager.AddParticule(new Smoke(this.position + this.mouthShot + this.gojPos + v, rand.GetRandomTrajectory(200, MathHelper.ToRadians(200), MathHelper.ToRadians(240)), rand.GetRandomFloat(0.7f, 1f), Color.White, false));
                    var s = new Shot(this.position + gojPos + mouthShot, new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * 0.5f, 200, 0, false, 1, 3);
                    s.angle = (float)(angle - Math.PI);
                    Gamescreen.Instance.shotManager.AddShotEnemy(s);
                    angle += offset;
                }
                firephase++;

                fireCD = 0;

            }

        }



        public bool IsPointPerfectColliding(Rectangle r, Color[] mapr, ref float rot, ref Vector2 pos)
        {
            if (!r.Intersects(rectcollider)) return false;
            int top = Math.Max(r.Top, rectcollider.Top);
            int bottom = Math.Min(r.Bottom, rectcollider.Bottom);
            int left = Math.Max(r.Left, rectcollider.Left);
            int right = Math.Min(r.Right, rectcollider.Right);
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    if (mapr[(x - r.Left) + (y - r.Top) * r.Width].A != 0 &&
                        collider[(x - rectcollider.Left) + (y - rectcollider.Top) * rectcollider.Width].A != 0)
                    {
                        rot = (float)((normalMap[(x - rectcollider.Left) + (y - rectcollider.Top) * rectcollider.Width].G - 128) / 128f * Math.PI / 2);
                        pos = new Vector2(x, y);
                        return true;
                    }
                }
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
                    nextangle = rand.GetRandomFloat(0 + Math.PI / 10, Math.PI - Math.PI / 10);
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

                    Bubble b = new Bubble(p, 0, v * -0.8f);
                    Gamescreen.Instance.shotManager.AddShotEnemy(b);

                    bubbleCD = 0;
                    if (nbBubbles >= nbBubblesphase)
                    {
                        nbBubbles = 0;
                        changePhase();
                    }
                }
                else
                {
                    rotation += wheelway * 0.02f;
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
            shootPhase = (shootPhase + 1) % 5;
            if (!dead & shootPhase == 0)
            {
                //drop bubble
                eyephase = 2; 
                this.velocity = new Vector2(0, 50);
                ismoving = true;

            }
            else if (!dead & shootPhase == 1)
            {

                wheelmoving = false;
                nbBubbles = 0;
                this.velocity = new Vector2(0, 50);
                ismoving = true;
            }
            else if (!dead & shootPhase == 2)
            {
                //reinit phase
                eyephase = 1;
                tonguephase=0;
                animfire = true;
                mouthAnim = 0;
                mouthphase = 0;
                srcmouth.X = 172;
                mouthphase = 0;
                fireCD = 0;
                generateMap(false);
            }
            else if (dead | shootPhase == 3) // warm down
            {
                ismoving = false;
                tonguebox = new SphereBox(this.position + gojPos + hitboxpos, 30);
                eyephase = 0;
                velocity = Vector2.Zero;
            }
            else if (!dead & shootPhase == 4)
            {
                
                eyephase = 1;
                tonguebox = null;
                rushstate = 0;
                rushTimer = 0;
                velocity = Vector2.Zero;
            }


            Console.WriteLine("changePhase " + shootPhase);

        }


        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture2, this.position + mastPos, srcmast, Color.White);
            if (shootPhase == 3) spritebatch.Draw(texture1, this.position + gojPos + tonguePos, srctongue, color);
            spritebatch.Draw(texture1, this.position + gojPos, srcbody, color);
            spritebatch.Draw(texture2, this.position, srcboat, Color.White);
            spritebatch.Draw(texture2, this.position + smokePos, srcsmoker, Color.White);
            spritebatch.Draw(texture2, this.position + wheelPos, srcwheel, Color.White, -rotation, wheelOrg, 1, SpriteEffects.None, 0);
            if (animfire) spritebatch.Draw(texture1, this.position + gojPos + mouthPos, srcmouth, Color.White);
            if (!eyeclosed) spritebatch.Draw(texture1, this.position + gojPos + eyePos, srcEye, Color.White);
        }








    }
}
