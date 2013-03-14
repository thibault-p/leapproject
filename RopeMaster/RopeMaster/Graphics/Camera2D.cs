using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RopeMaster
{
    public class Camera2D
    {
        protected float _zoom; // Camera Zoom
        public Matrix _transform; // Matrix Transform
        public Vector2 _pos; // Camera Position
        protected float _rotation; // Camera Rotation


        public Rectangle ScreenVisible;
        public Camera2D()
        {
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = Vector2.Zero;
            ScreenVisible = Game1.Instance.Screen;
        }
        // Sets and gets zoom
        public float Zoom
        {
            get { return _zoom; }
            set
            {
                _zoom = value;
                if (_zoom < 0.1f) _zoom = 0.1f;
                ScreenVisible.Width = (int)(_zoom * Game1.Instance.Screen.Width + 50);
                ScreenVisible.Height = (int)(_zoom * Game1.Instance.Screen.Height + 50);
            } // Negative zoom will flip image

        }

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        // Auxiliary function to move the Camera
        public void Move(Vector2 amount)
        {
            _pos += amount;
        }
        // Get set position
        public Vector2 Pos
        {
            get { return _pos; }
            set
            {
                _pos = value;
                ScreenVisible.X = (int)value.X-25;
                ScreenVisible.Y = (int)value.Y-25;
            }
        }

        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            _transform =
              Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));
            return _transform;
        }





    }

}
