using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class Skeleton : Objects
    {
        public override char Icon
        {
            get
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                return (char)42;
            }
        }
        public override string Name
        {
            get { return "Skeleton"; }
        }
        //public int Health = 1;
        public Skeleton(Vector2i position)
            : base(position)
        {
        }
        //public void GetDamage(int damage)
        //{
        //    Health -= damage;
        //}
        public static int AliveSkeleton = 0;
        static int skeletonOneHealth = 1;
        static int skeletonOneX = 0;
        static int skeletonOneY = 0;
        static int skeletonTwoHealth = 1;
        static int skeletonTwoX = 0;
        static int skeletonTwoY = 0;


        public static void skeletonSpawn(MapField[,] mapFields)
        {
            Random rnd = new Random();
            skeletonOneX = rnd.Next(1, 38);
            skeletonOneY = rnd.Next(6, 18);
            skeletonTwoX = rnd.Next(1, 38);
            skeletonTwoY = rnd.Next(6, 18);

        }

        public static void skeletonOneKI()
        {
            if (skeletonOneHealth > 0 && Map.Level == 3)
            {
                if (skeletonOneX < Player.PlayerX1)
                {
                    Console.SetCursorPosition(skeletonOneX, skeletonOneY);
                    Console.Write(' ');
                    skeletonOneX += 1;
                    Console.SetCursorPosition(skeletonOneX, skeletonOneY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)42);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapGraveyard.MaxWidth, MapGraveyard.MaxHeight);
                }
                if (skeletonOneX > Player.PlayerX1)
                {
                    Console.SetCursorPosition(skeletonOneX, skeletonOneY);
                    Console.Write(' ');
                    skeletonOneX -= 1;
                    Console.SetCursorPosition(skeletonOneX, skeletonOneY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)42);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapGraveyard.MaxWidth, MapGraveyard.MaxHeight);
                }
                if (skeletonOneY < Player.PlayerY1)
                {
                    Console.SetCursorPosition(skeletonOneX, skeletonOneY);
                    Console.Write(' ');
                    skeletonOneY += 1;
                    Console.SetCursorPosition(skeletonOneX, skeletonOneY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)42);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapGraveyard.MaxWidth, MapGraveyard.MaxHeight);
                }
                if (skeletonOneY > Player.PlayerY1)
                {
                    Console.SetCursorPosition(skeletonOneX, skeletonOneY);
                    Console.Write(' ');
                    skeletonOneY -= 1;
                    Console.SetCursorPosition(skeletonOneX, skeletonOneY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)42);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapGraveyard.MaxWidth, MapGraveyard.MaxHeight);
                }
            }
            if (skeletonOneX == Player.PlayerX1 && skeletonOneY == Player.PlayerY1)
            {
                Console.SetCursorPosition(skeletonOneX, skeletonOneY);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write((char)2);
                Console.ResetColor();
                skeletonOneHealth--;
                Player.Health--;
                MapGraveyard.graveyardFields[skeletonOneX, skeletonOneY].Content = new BlankSpace(new Vector2i(skeletonOneX, skeletonOneY));
                skeletonOneX = 0;
                skeletonOneY = 0;
                Console.SetCursorPosition(MapGraveyard.MaxWidth, MapGraveyard.MaxHeight);
            }


        }
        public static void skeletonTwoKI()
        {
            if (skeletonTwoHealth > 0 && Map.Level == 3)
            {
                if (skeletonTwoX < Player.PlayerX1)
                {
                    Console.SetCursorPosition(skeletonTwoX, skeletonTwoY);
                    Console.Write(' ');
                    skeletonTwoX += 1;
                    Console.SetCursorPosition(skeletonTwoX, skeletonTwoY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)42);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapGraveyard.MaxWidth, MapGraveyard.MaxHeight);
                }
                if (skeletonTwoX > Player.PlayerX1)
                {
                    Console.SetCursorPosition(skeletonTwoX, skeletonTwoY);
                    Console.Write(' ');
                    skeletonTwoX -= 1;
                    Console.SetCursorPosition(skeletonTwoX, skeletonTwoY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)42);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapGraveyard.MaxWidth, MapGraveyard.MaxHeight);
                }
                if (skeletonTwoY < Player.PlayerY1)
                {
                    Console.SetCursorPosition(skeletonTwoX, skeletonTwoY);
                    Console.Write(' ');
                    skeletonTwoY += 1;
                    Console.SetCursorPosition(skeletonTwoX, skeletonTwoY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)42);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapGraveyard.MaxWidth, MapGraveyard.MaxHeight);
                }
                if (skeletonTwoY > Player.PlayerY1)
                {
                    Console.SetCursorPosition(skeletonTwoX, skeletonTwoY);
                    Console.Write(' ');
                    skeletonTwoY -= 1;
                    Console.SetCursorPosition(skeletonTwoX, skeletonTwoY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)42);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapGraveyard.MaxWidth, MapGraveyard.MaxHeight);
                }
            }
            if (skeletonTwoX == Player.PlayerX1 && skeletonTwoY == Player.PlayerY1)
            {
                Console.SetCursorPosition(skeletonTwoX, skeletonTwoY);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write((char)2);
                Console.ResetColor();
                skeletonTwoHealth--;
                Player.Health--;
                MapGraveyard.graveyardFields[skeletonTwoX, skeletonTwoY].Content = new BlankSpace(new Vector2i(skeletonTwoX, skeletonTwoY));
                skeletonTwoX = 0;
                skeletonTwoY = 0;
                Console.SetCursorPosition(MapGraveyard.MaxWidth, MapGraveyard.MaxHeight);
            }


        }

    }
}
