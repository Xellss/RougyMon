using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class Key : Objects
    {
        public override char Icon
        {
            get
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
                return (char)12;
            }
        }

        public override string Name
        {
            get { return "Key"; }
        }
        public Key(Vector2i position)
            : base(position)
        {
        }
        public static void KeySpawn(MapField[,] mapFields)
        {
            if (KeyCounter <= 0 )
            {
                Random rnd = new Random();
                int keyX = rnd.Next(1, 38);
                int keyY = rnd.Next(1, 18);
                if (mapFields[keyX, keyY].Content is BlankSpace)
                {
                    mapFields[keyX, keyY].Content = new Key(new Vector2i(keyX, keyY));
                }
                else
                {
                    KeySpawn(mapFields);
                }
            }
        }
        public static int KeyCounter = 0;

        public void KeySpawn()
        {

        }

    }
}
