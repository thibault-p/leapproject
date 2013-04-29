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
using RopeMaster.gameplay.Enemies;
using RopeMaster.gameplay;
using RopeMaster.Graphics;
using Glitch.Engine.Core;
using Glitch.Engine.Particules;



namespace RopeMaster.Core
{
    public class Gamescreen : State
    {

        public static Gamescreen Instance;
        public Camera2D camera;
        Parallax parallax;
        Gojira gojira;
        public Combobar combobar;
        public GameManager gamemanager;
        LeapControl leapControl;
        KeyboardState prevKey;
        public ShotManager shotManager;
        
        public ParticuleManager particuleManager;
        public EnemyManager enemyManager;
        public StuffManager stuffManager;
       
        public Player player;

        public bool playing;
        public bool gameover;
        public int liferemains;
        public LifeBar lifebar;

        public Gamescreen()
        {
            Instance = this;
        }



        public override void Initialyze()
        {
            base.Initialyze();
            Game1.Instance.score = 0;
            parallax = new Parallax();
            player = new Player();
            gojira = new Gojira();
            enemyManager = new EnemyManager();
            enemyManager.Initialize();
            enemyManager.Add(gojira);
            gamemanager = new GameManager();
            combobar = new Combobar(10);
            particuleManager = new ParticuleManager();
            camera = new Camera2D();
            gamemanager.Initialize();
            shotManager = new ShotManager();
            stuffManager = new StuffManager();
            enemyManager = new EnemyManager();
            lifebar = new LifeBar(new Vector2(1200, 20), 8);
            leapControl = Game1.Instance.leapControl;
            shotManager.Initialize();
            particuleManager.Initialize();
            enemyManager.Initialize();
            enemyManager.Add(gojira);
            liferemains = 9000;
            playing = true;
            gameover = false;
            Game1.Instance.musicPlayer.PlayMusic("teleporter");
        }



        public override void Update(GameTime gameTime)
        {
            parallax.moveHorizontal(1);
            if (playing && player.die==0)
            {
                // Allows the game to exit
                KeyboardState k = Keyboard.GetState();
           
                prevKey = k;

                if (k.IsKeyDown(Keys.Up))
                {
                    var p = gojira.getPosition();
                    gojira.setPosition(p - Vector2.UnitY);
                }
                if (k.IsKeyDown(Keys.Down))
                {
                    var p = gojira.getPosition();
                    gojira.setPosition(p + Vector2.UnitY);
                }

                if (Game1.Instance.inputManager.getState(InputManager.Commands.Down) == InputManager.InputState.JustOn)
                {
                    if (player.steerRope(1))
                        combobar.RemoveLenght();

                }
                if (Game1.Instance.inputManager.getState(InputManager.Commands.Up) == InputManager.InputState.JustOn)
                {
                    if (player.steerRope(-1))
                        combobar.AddLength();
                }
                if (Game1.Instance.inputManager.getState(InputManager.Commands.Fire) == InputManager.InputState.JustOn)
                {
                    shotManager.playerFire(player.getShotSource(), player.getShotAngle(), 1, player.getPointShot());
                }
                else if (Game1.Instance.inputManager.getState(InputManager.Commands.Fire) == InputManager.InputState.On)
                {
                    shotManager.playerAutoFire(player.getShotSource(), player.getShotAngle(), 1, player.getPointShot());

                }

                
                if (leapControl.Is)
                {
                    var p = leapControl.getPosition();
                    player.setPosition((int)p.X, (int)p.Y);
                    player.setIsCatched(true);
                }
                else
                {
                    var m = Mouse.GetState();
                    player.setIsCatched(false);
                }
            }
            parallax.Update(gameTime);
            player.Update(gameTime);
            if (player.isDead()) newPlayer();
            gojira.Update(gameTime);
            lifebar.setHP(gojira.hp);
            lifebar.Update(gameTime);
            particuleManager.Update(gameTime);
            enemyManager.Update(gameTime);
            shotManager.Update(gameTime);
            stuffManager.Update(gameTime);
            combobar.Update(gameTime);
            gamemanager.Update(gameTime);


            //GAMEOVER
            if (gameover && color.A == 255)
            {
                Game1.Instance.score = gamemanager.score;
                Game1.Instance.StateManage.changeState(4);
            }
            if (!gameover && gamemanager.time >=GameManager.timemax)
            {
                fadeOut();
                gameover = true;

            }
            if (!gameover && gojira.hp <= 0 && gojira.getPosition().Y>750)
            {
                fadeOut();
                gameover = true;
          
            }




        }




        private void newPlayer()
        {

            liferemains--;
            if (liferemains < 0)
            {
                fadeOut();
                gameover = true;
            }
            else
            {
                player = new Player();
                combobar.length = 10;
                playing = true;
            }


        }



    


        public override void Draw(SpriteBatch spriteBatch)
        {
            Microsoft.Xna.Framework.Matrix m = camera.get_transformation(Game1.Instance.GraphicsDevice);

            //paralax background wrapping
            parallax.Draw(spriteBatch);

            spriteBatch.Begin(SpriteSortMode.Immediate,
                        BlendState.AlphaBlend, null,
                        null,
                        null,
                        null, m);
            player.Draw(spriteBatch);
            enemyManager.Draw(spriteBatch);
            gojira.Draw(spriteBatch);
            particuleManager.Draw(spriteBatch);
            shotManager.Draw(spriteBatch);
            stuffManager.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            //get rid of Camera
            if (!leapControl.Is)
            {
                leapControl.Draw(spriteBatch);

            }
            combobar.Draw(spriteBatch);
            lifebar.Draw(spriteBatch);
            gamemanager.Draw(spriteBatch);
            spriteBatch.End();

        }




    }
}
