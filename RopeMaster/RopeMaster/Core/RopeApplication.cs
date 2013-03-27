using System;
using System.Reflection;
using Glitch.Engine.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using RopeMaster.Core;
using RopeMaster;
using RopeMaster.gameplay;
using Glitch.Engine.Particules;
using Glitch.Engine.Util;

namespace Glitch.Engine.Core
{
    /// <summary>
    /// Provides the basics features/helpers/services
    /// </summary>
    public abstract class RopeApplication : Game
    {
        public static RopeApplication Instance;


        public MagicContentManager magicContentManager;
        public ShotManager shotManager;
        private string _name;
        private string _version;
        private bool _isdebugMode;
        public Rectangle Screen { get; set; }
        public InputManager inputManager;
        public ParticuleManager particuleManager;
        public EnemyManager enemyManager;
        public Camera2D Camera;
        public Player player;
        public RandomMachine randomizator;
        public int difficulty=1;

        /// <summary>
        /// Constructor
        /// </summary>
        protected RopeApplication(string name, string rootContentDirectory, string version)
        {
            // associates the static application instance to the current one
            Instance = this;

            _name = name;
            _version = version;

#if WINDOWS
            Window.Title = _name;
#endif

            Content.RootDirectory = rootContentDirectory;

            //_graphics = new GraphicsDeviceManager(this);
            // Initialize the resolution
            magicContentManager = new MagicContentManager(GameAssemblies, Content);
            particuleManager = new ParticuleManager();
            inputManager = new InputManager();
            shotManager = new ShotManager();
            enemyManager = new EnemyManager();
            randomizator = new RandomMachine(1337);
            //Xbox Live
#if XBOX
            this.Components.Add(new GamerServicesComponent(this));
#endif
        }

        protected override void LoadContent()
        {
            magicContentManager.Initialize();
            base.LoadContent();
            shotManager.Initialize();
            particuleManager.Initialize();
            enemyManager.Initialize();

        }

        /// <summary>
        /// Reference page contains links to related conceptual articles.
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Update.</param>
        protected override void Update(GameTime gameTime)
        {
            inputManager.Update(gameTime);
            shotManager.Update(gameTime);
            particuleManager.Update(gameTime);
            enemyManager.Update(gameTime);
            base.Update(gameTime);



        }

        /// <summary>
        /// Reference page contains code sample.
        /// </summary>
        /// <param name="gameTime">Time passed since the last call to Draw.</param>
        protected override void Draw(GameTime gameTime)
        {

            base.Draw(gameTime);
        }

        public static void Quit()
        {
            Instance.Exit();
        }

        public static bool IsApplicationActive
        {
            get { return Instance.IsActive; }
        }



        public static bool IsDebugMode
        {
            get { return Instance._isdebugMode; }
            set { Instance._isdebugMode = value; }
        }


        protected abstract Assembly[] GameAssemblies { get; }
    }
}
