using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class Gold : Objects
    {
        public override char Icon
        {
            get
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                return '$';

            }
        }


        public override string Name
        {
            get { return "Gold"; }
        }
        public static int GoldCounter = 0;
        //static Random Rnd = new Random();


        public static Gold[] InitGold(MapField[,] mapFields)
        {
            Random spawnCount = new Random();
            Random rnd = new Random();
            int SpawnCount = spawnCount.Next(3, 8);
            Gold[] gold = new Gold[SpawnCount];
            int goldX = 0;
            int goldY = 0;
            for (int i = 0; i < SpawnCount; i++)
            {
                goldX = rnd.Next(1, 38);
                goldY = rnd.Next(1, 18);
                if (mapFields[goldX, goldY].Content is BlankSpace)
                {
                    gold[i] = new Gold(new Vector2i(goldX, goldY));

                }

            }
            return gold;
        }

        public Gold(Vector2i position)
            : base(position)
        {

        }

    }
}
