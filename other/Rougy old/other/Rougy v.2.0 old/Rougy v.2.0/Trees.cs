using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class Trees : Objects
    {

        public override char Icon
        {
            get
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                return 'B'; 
            }
        }

        public override string Name
        {
            get
            {
                return "Tree"; 
            }
        }
        public static int treeY = 0;
        public static int treeX = 0;
        public static Trees[] DrawRandomTree(MapField[,] forestFields)
        {
            Random spawnCount = new Random();
            Random Rnd = new Random();
            int SpawnCount = spawnCount.Next(35, 70);
            Trees[] tree = new Trees[SpawnCount];
            
            Random rnd = new Random();
            for (int i = 0; i < SpawnCount; i++)
            {
                treeX = rnd.Next(1, 38);
               treeY = rnd.Next(1, 18);
               if (forestFields[treeX, treeY].Content is BlankSpace)
               {
                   tree[i] = new Trees(new Vector2i(treeX, treeY));
                   
               }

            }
            return tree;
        }
        
        public Trees(Vector2i position)
            : base(position)
        {

        }
    }
}
