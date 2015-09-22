using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class Gravestone : Objects
    {
        public override char Icon
        {
            get
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Gray;
                return (char)197;
            }
        }

        public override string Name
        {
            get { return "Gravestone"; }
        }
        public static void SpawnGravestone(MapField[,] graveyardFields)
        {
            graveyardFields[3, 5].Content = new Gravestone(new Vector2i(3, 5));
            graveyardFields[3, 10].Content = new Gravestone(new Vector2i(3, 10));
            graveyardFields[3, 15].Content = new Gravestone(new Vector2i(3, 15));
            graveyardFields[8, 5].Content = new Gravestone(new Vector2i(8, 5));
            graveyardFields[8, 10].Content = new Gravestone(new Vector2i(8, 10));
            graveyardFields[8, 15].Content = new Gravestone(new Vector2i(8, 15));
            graveyardFields[13, 5].Content = new Gravestone(new Vector2i(13, 5));
            graveyardFields[13, 10].Content = new Gravestone(new Vector2i(13, 10));
            graveyardFields[13, 15].Content = new Gravestone(new Vector2i(13, 15));
            graveyardFields[18, 5].Content = new Gravestone(new Vector2i(18, 5));
            graveyardFields[18, 10].Content = new Gravestone(new Vector2i(18, 10));
            graveyardFields[18, 15].Content = new Gravestone(new Vector2i(18, 15));
            graveyardFields[23, 5].Content = new Gravestone(new Vector2i(23, 5));
            graveyardFields[23, 10].Content = new Gravestone(new Vector2i(23, 10));
            graveyardFields[23, 15].Content = new Gravestone(new Vector2i(23, 15));
            graveyardFields[28, 5].Content = new Gravestone(new Vector2i(28, 5));
            graveyardFields[28, 10].Content = new Gravestone(new Vector2i(28, 10));
            graveyardFields[28, 15].Content = new Gravestone(new Vector2i(28, 15));
            graveyardFields[33, 5].Content = new Gravestone(new Vector2i(33, 5));
            graveyardFields[33, 10].Content = new Gravestone(new Vector2i(33, 10));
            graveyardFields[33, 15].Content = new Gravestone(new Vector2i(33, 15));
        }
        
        public Gravestone(Vector2i position)
            : base(position)
        {

        }
    }
}
