using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glitch.Engine.Particules;
using Microsoft.Xna.Framework;
using RopeMaster;

namespace Glitch.Graphics.particules
{
    public class Feather : Particule
    {
        public Feather(Vector2 location, Vector2 trajectory, float scale, Color color, bool background)
            : base(location, trajectory, new Rectangle(Game1.Instance.randomizator.GetRandomInt(0, 2) * 20, 255, 20, 20), 3f, scale, color, background)
        {

        }

        public override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Slowly move the smoke
            if (TimeToLive < 500f)
            {
                if (trajectory.Y < -10.0f) trajectory.Y += elapsedTime * 500f;
                if (trajectory.X < -10.0f) trajectory.X += elapsedTime * 150f;
                if (trajectory.Y > 10.0f) trajectory.Y += elapsedTime * 150f;
            }
            trajectory.Y += 1;
            alpha -= elapsedTime;
            if (alpha < 0) alpha = 0f;

            base.Update(gameTime);
        }
    }
}
