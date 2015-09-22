using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class Ork : Objects
    {
        public override char Icon
        {
            get
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                return (char)190;
            }
        }
        public Ork(Vector2i position)
            : base(position)
        {
        }
        public override string Name
        {
            get { return "Ork"; }
        }
        public static int OrkCount = 0;
        static int orkOneHealth = 1;
        static int orkOneX = 0;
        static int orkOneY = 0;
        static int orkTwoHealth = 1;
        static int orkTwoX = 0;
        static int orkTwoY = 0;


        public static void OrkSpawn(MapField[,] mapFields)
        {
            Random rnd = new Random();
            orkOneX = rnd.Next(1, 38);
            orkOneY = rnd.Next(1, 18);
            orkTwoX = rnd.Next(1, 38);
            orkTwoY = rnd.Next(1, 18);

        }
       
        public static void OrkOneKI()
        {
            if (orkOneHealth >0 && Map.Level == 2)
            {
                if (orkOneX < Player.PlayerX1)
                {
                    Console.SetCursorPosition(orkOneX, orkOneY);
                    Console.Write(' ');
                    orkOneX += 1;
                    Console.SetCursorPosition(orkOneX, orkOneY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)190);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapForest.MaxWidth, MapForest.MaxHeight);
                }
                 if (orkOneX > Player.PlayerX1)
                {
                    Console.SetCursorPosition(orkOneX, orkOneY);
                    Console.Write(' ');
                    orkOneX -= 1;
                    Console.SetCursorPosition(orkOneX, orkOneY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)190);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapForest.MaxWidth, MapForest.MaxHeight);
                }
                 if (orkOneY < Player.PlayerY1)
                {
                    Console.SetCursorPosition(orkOneX, orkOneY);
                    Console.Write(' ');
                    orkOneY += 1;
                    Console.SetCursorPosition(orkOneX, orkOneY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)190);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapForest.MaxWidth, MapForest.MaxHeight);
                }
                 if (orkOneY > Player.PlayerY1)
                {
                    Console.SetCursorPosition(orkOneX, orkOneY);
                    Console.Write(' ');
                    orkOneY -= 1;
                    Console.SetCursorPosition(orkOneX, orkOneY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)190);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapForest.MaxWidth, MapForest.MaxHeight);
                }
            }
            if (orkOneX == Player.PlayerX1 && orkOneY == Player.PlayerY1)
            {
                Console.SetCursorPosition(orkOneX, orkOneY);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write((char)2);
                Console.ResetColor();
                orkOneHealth--;
                Player.Health--;
                MapForest.forestFields[orkOneX, orkOneY].Content = new BlankSpace(new Vector2i(orkOneX, orkOneY));
                orkOneX = 0;
                orkOneY = 0;
                Console.SetCursorPosition(MapForest.MaxWidth, MapForest.MaxHeight);
            }


        }
        public static void OrkTwoKI()
        {
            if (orkTwoHealth >0 && Map.Level == 2)
            {
                if (orkTwoX < Player.PlayerX1)
                {
                    Console.SetCursorPosition(orkTwoX, orkTwoY);
                    Console.Write(' ');
                    orkTwoX += 1;
                    Console.SetCursorPosition(orkTwoX, orkTwoY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)190);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapForest.MaxWidth, MapForest.MaxHeight);
                }
                 if (orkTwoX > Player.PlayerX1)
                {
                    Console.SetCursorPosition(orkTwoX, orkTwoY);
                    Console.Write(' ');
                    orkTwoX -= 1;
                    Console.SetCursorPosition(orkTwoX, orkTwoY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)190);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapForest.MaxWidth, MapForest.MaxHeight);
                }
                 if (orkTwoY < Player.PlayerY1)
                {
                    Console.SetCursorPosition(orkTwoX, orkTwoY);
                    Console.Write(' ');
                    orkTwoY += 1;
                    Console.SetCursorPosition(orkTwoX, orkTwoY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)190);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapForest.MaxWidth, MapForest.MaxHeight);
                }
                 if (orkTwoY > Player.PlayerY1)
                {
                    Console.SetCursorPosition(orkTwoX, orkTwoY);
                    Console.Write(' ');
                    orkTwoY -= 1;
                    Console.SetCursorPosition(orkTwoX, orkTwoY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)190);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapForest.MaxWidth, MapForest.MaxHeight);
                }
            }
            if (orkTwoX == Player.PlayerX1 && orkTwoY == Player.PlayerY1)
            {
                Console.SetCursorPosition(orkTwoX, orkTwoY);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write((char)2);
                Console.ResetColor();
                orkTwoHealth--;
                Player.Health--;
                MapForest.forestFields[orkTwoX, orkTwoY].Content = new BlankSpace(new Vector2i(orkTwoX, orkTwoY));
                orkTwoX = 0;
                orkTwoY = 0;
                Console.SetCursorPosition(MapForest.MaxWidth, MapForest.MaxHeight);
            }


        }

    }
}
