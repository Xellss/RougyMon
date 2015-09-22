using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class Water : Objects
    {
        public override char Icon
        {
            get
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                return (char)178; 
            }
        }

        public override string Name
        {
            get 
            { 
                return "Water"; 
            }
        }
        public Water(Vector2i position)
            : base(position)
        {

        }

        public static void SpawnWater(MapField[,] forestFields)
        {
            forestFields[1, 1].Content = new Water (new Vector2i(1,1));
            forestFields[1, 2].Content = new Water (new Vector2i(1,2));
            forestFields[1, 3].Content = new Water (new Vector2i(1,3));
            forestFields[1, 4].Content = new Water (new Vector2i(1,4));
            forestFields[1, 5].Content = new Water (new Vector2i(1,5));
            forestFields[1, 6].Content = new Water (new Vector2i(1,6));
            forestFields[2, 1].Content = new Water (new Vector2i(2,1));
            forestFields[2, 2].Content = new Water (new Vector2i(2,2));
            forestFields[2, 3].Content = new Water (new Vector2i(2,3));
            forestFields[2, 4].Content = new Water (new Vector2i(2,4));
            forestFields[2, 5].Content = new Water (new Vector2i(2,5));
            forestFields[3, 1].Content = new Water (new Vector2i(3,1));
            forestFields[3, 2].Content = new Water (new Vector2i(3,2));
            forestFields[3, 3].Content = new Water (new Vector2i(3,3));
            forestFields[3, 4].Content = new Water (new Vector2i(3,4));
            forestFields[4, 1].Content = new Water (new Vector2i(4,1));
            forestFields[4, 2].Content = new Water (new Vector2i(4,2));
            forestFields[4, 3].Content = new Water (new Vector2i(4,3));
            forestFields[5, 1].Content = new Water (new Vector2i(5,1));
            forestFields[5, 2].Content = new Water (new Vector2i(5,2));
            forestFields[6, 1].Content = new Water (new Vector2i(6,1));

        }
    }
}
