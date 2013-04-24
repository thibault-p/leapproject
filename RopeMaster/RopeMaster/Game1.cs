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

      
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;



        public Game1() :
            base("PiyPiyo", "Content", "1.0")
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
            this.Screen = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            StateManage = new StateManager();
           

            inputManager = new InputManager();
            leapControl = new LeapControl();
            musicPlayer = new MusicPlayer();
            LoadContent();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Console.WriteLine("loadcontent");
            // TODO: use this.Content to load your game content here
            base.LoadContent();
            StateManage.Initialize();
            StateManage.changeState(4);

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

            inputManager.Update(gameTime);
            leapControl.Update(gameTime);
            StateManage.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            StateManage.Draw(spriteBatch);

        }


        protected override Assembly[] GameAssemblies
        {
            get { return new Assembly[] { typeof(Game1).Assembly }; }
        }
    }
}
