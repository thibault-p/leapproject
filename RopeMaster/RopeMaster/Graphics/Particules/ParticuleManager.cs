
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Glitch.Engine.Core;
using Glitch.Engine.Particules;
using RopeMaster;
using Glitch.Engine.Content;

namespace Glitch.Engine.Particules
{
    /// <summary>
    /// Manage, move, create particules
    /// </summary>
    /// 
     [TextureContent(AssetName = "particles", AssetPath = "gfx/misc/particules")]
    public class ParticuleManager : Manager
    {
        private const int MaxParticules = 512;
        private Particule[] _particules;
        private Texture2D texture; 

        public ParticuleManager()
        {
            _particules = new Particule[MaxParticules];
        }

        /// <summary>
        /// Clear particule manager on init
        /// </summary>
        public void Initialize()
        {
            for (int i = 0; i < MaxParticules; i++)
            {
                _particules[i] = null;
            }
            texture = Game1.Instance.magicContentManager.GetTexture("particles");

        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < _particules.Length; i++)
            {
                if (_particules[i] != null)
                {
                    _particules[i].Update(gameTime);

                    if (_particules[i].IsAlive == false)
                    {
                        _particules[i] = null;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            //Normal ones
      
            for (int i = 0; i < _particules.Length; i++)
            {
                if ((_particules[i] != null) && (_particules[i].IsAdditive == false))
                {
                    _particules[i].Draw(spriteBatch, texture);
                }
            }

           
        }

        /// <summary>
        /// Add a particule
        /// </summary>
        /// <param name="p"></param>
        /// <returns>Added to the list</returns>
        public bool AddParticule(Particule p)
        {
            for (int i = 0; i < _particules.Length; i++)
            {
                if (_particules[i] == null)
                {
                    _particules[i] = p;
                    return true;
                }
            }

            return false;
        }
    }
}
