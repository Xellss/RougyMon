using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class GameOver
    {
        public GameOver()
        {
            if (Player.Health <= 0)
            {
                Map.Level = 0;
                Console.Clear();
                Console.WriteLine("Game Over");
            }
        }
    }
}
