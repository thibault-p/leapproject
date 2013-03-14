﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace RopeMaster.Core
{
    public abstract class Entity
    {
        private Vector2 position;
        private Vector2 velocity;



        public Entity()
        {
            initialyze(Vector2.Zero, Vector2.Zero);
        }


        public Entity(Vector2 pos, Vector2 velo){
            this.initialyze(pos, velo);
        }   

        private void initialyze(Vector2 pos, Vector2 velo){
            this.position = pos;
            this.velocity = velo*500;
        }

        public void Update(GameTime gameTime)
        {
            var d = gameTime.ElapsedGameTime.Milliseconds/1000f;
            position += velocity *d;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public bool exterminate()
        {
            return false;
        }


    }
}