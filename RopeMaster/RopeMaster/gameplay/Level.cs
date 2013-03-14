using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RopeMaster.gameplay
{
    public class Level
    {
        public List<Obstacle> obstacles;
      


        public Level()
        {
            obstacles = new List<Obstacle>();


        }

        public void Initialyze()
        {
            obstacles.Clear();


            loadLevel();
        }

        private void loadLevel()
        {

        }



    }
}
