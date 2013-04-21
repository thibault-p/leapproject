using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using RopeMaster.gameplay.Helpers;
using RopeMaster.Core;

namespace RopeMaster.gameplay.Enemies
{
    public class EnemySpawner<T> : Entity where T : Enemy, new()
    {
        private int load;
        private int cooldown;
        private bool ready;
        private int time;
        private Trajectory trajectoty;



        public EnemySpawner(Vector2 _pos, int _load, int _cooldown, Trajectory traj)
            : base(_pos, Vector2.Zero)
        {
            load = _load;
            cooldown = _cooldown;
            ready = true;
            trajectoty = traj;
        }


        public override void Update(GameTime gameTime)
        {
            if (load > 0)
            {
                time += gameTime.ElapsedGameTime.Milliseconds;
                if (time > cooldown)
                {
                    fire();
                    time = 0;
                }
            }
        }


        public override bool exterminate()
        {
            return load <= 0;
        }



        public void fire()
        {
            load--;
            T tmp = new T();
            var p = this.getPosition();
            tmp.setPosition(p);
            tmp.setTrajectory(trajectoty.compute);
            Gamescreen.Instance.enemyManager.Add(tmp);
        }











    }
}
