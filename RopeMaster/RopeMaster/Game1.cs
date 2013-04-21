using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Leap;
using Glitch.Engine.Core;
using System.Reflection;
using RopeMaster.Graphics;
using RopeMaster.Core;
using RopeMaster.gameplay;
using RopeMaster.gameplay.Helpers;
using RopeMaster.gameplay.Enemies;



namespace RopeMaster
{


    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : RopeApplication
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D tex;
        KeyboardState prevKey;

        Parallax parallax;
        Rectangle screen;
        LeapControl leapControl;

        EnemySpawner<Bubble> spawner;

        Gojira gojira;
        Combobar combobar;
        GameManager gamemanager;

        public Game1() :
            base("Rope Master", "Content", "1.0")
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            base.Initialize();
            this.Screen = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);


            //controller = new Controller();
            Camera = new Camera2D();
            leapControl = new LeapControl();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            tex = Content.Load<Texture2D>("gfx/point");
            screen = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            base.LoadContent();
            parallax = new Parallax();
            player = new Player();
            gojira = new Gojira();
            enemyManager.Add(gojira);
            gamemanager = new GameManager();
            combobar = new Combobar(20);
            //spawner = new EnemySpawner<Bubble>(Vector2.One * 500, 10, 2000, new SinTrajectory(0.5f, 0, 2 * (float)Math.PI));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary> 
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
            parallax.moveHorizontal(1);
            leapControl.Update(gameTime);



            // Allows the game to exit
            KeyboardState k = Keyboard.GetState();
            if (k.IsKeyDown(Keys.PageUp) && prevKey.IsKeyUp(Keys.PageUp))
            {

                lifeBar.setHP(10);
            }
            if (k.IsKeyDown(Keys.PageDown) && prevKey.IsKeyUp(Keys.PageDown))
            {
                lifeBar.setHP(20);
            }
            if (k.IsKeyDown(Keys.Left))
            {
                parallax.moveHorizontal(-1);
            }
            if (k.IsKeyDown(Keys.LeftShift))
            {
                shotManager.playerAutoFire(player.getShotSource(), player.getShotAngle(), 50, player.getPointShot());
            }
            if (k.IsKeyDown(Keys.Up))
            {
                parallax.moveVertical(-1);
            }
            if (k.IsKeyDown(Keys.Down))
            {
                parallax.moveVertical(1);
            }
            parallax.Update(gameTime);
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






            if (Game1.Instance.inputManager.getState(InputManager.Commands.Down) == InputManager.State.JustOn)
            {
                if (player.steerRope(1))
                    combobar.RemoveLenght();

            }
            if (Game1.Instance.inputManager.getState(InputManager.Commands.Up) == InputManager.State.JustOn)
            {
                if (player.steerRope(-1))
                    combobar.AddLength();
            }
            if (Game1.Instance.inputManager.getState(InputManager.Commands.Fire) == InputManager.State.JustOn)
            {
                shotManager.playerFire(player.getShotSource(), player.getShotAngle(), 50, player.getPointShot());
            }
            else if (Game1.Instance.inputManager.getState(InputManager.Commands.Fire) == InputManager.State.On)
            {
                shotManager.playerAutoFire(player.getShotSource(), player.getShotAngle(), 50, player.getPointShot());

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
            player.Update(gameTime);
            combobar.Update(gameTime);
            gamemanager.Update(gameTime);
            //spawner.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Microsoft.Xna.Framework.Matrix m = Camera.get_transformation(GraphicsDevice);

            //paralax background wrapping
            parallax.Draw(spriteBatch);

            spriteBatch.Begin(SpriteSortMode.Immediate,
                        BlendState.AlphaBlend, null,
                        null,
                        null,
                        null, m);




            player.Draw(spriteBatch);
            enemyManager.Draw(spriteBatch);
            particuleManager.Draw(spriteBatch);
            shotManager.Draw(spriteBatch);
            stuffManager.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin();
            //get rid of Camera
            if (!leapControl.Is)
            {
                this.lifeBar.Draw(spriteBatch);
                leapControl.Draw(spriteBatch);

            }
            combobar.Draw(spriteBatch);
            gamemanager.Draw(spriteBatch);
            spriteBatch.End();


            base.Draw(gameTime);
        }


        protected override Assembly[] GameAssemblies
        {
            get { return new Assembly[] { typeof(Game1).Assembly }; }
        }
    }
}
