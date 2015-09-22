using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class SkeletonKing : Objects
    {
        public override char Icon
        {
            get
            {
                return (char)15;
            }
        }

        public override string Name
        {
            get
            {
                return "Skeleton King";
            }
        }
        public SkeletonKing(Vector2i position)
            : base(position)
        {
        }
        static int skeletonKingHealth = 1;
        static int skeletonKingX = 0;
        static int skeletonKingY = 0;

        public static void skeletonKingSpawn(MapField[,] mapFields)
        {
            Random rnd = new Random();
            skeletonKingX = rnd.Next(1, 38);
            skeletonKingY = rnd.Next(1, 5);

        }

        public static void skeletonKingKI()
        {
            if (skeletonKingHealth > 0 && Map.Level == 3)
            {
                if (skeletonKingX < Player.PlayerX1)
                {
                    Console.SetCursorPosition(skeletonKingX, skeletonKingY);
                    Console.Write(' ');
                    skeletonKingX += 1;
                    Console.SetCursorPosition(skeletonKingX, skeletonKingY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)15);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapGraveyard.MaxWidth, MapGraveyard.MaxHeight);
                }
                if (skeletonKingX > Player.PlayerX1)
                {
                    Console.SetCursorPosition(skeletonKingX, skeletonKingY);
                    Console.Write(' ');
                    skeletonKingX -= 1;
                    Console.SetCursorPosition(skeletonKingX, skeletonKingY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)15);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapGraveyard.MaxWidth, MapGraveyard.MaxHeight);
                }
                if (skeletonKingY < Player.PlayerY1)
                {
                    Console.SetCursorPosition(skeletonKingX, skeletonKingY);
                    Console.Write(' ');
                    skeletonKingY += 1;
                    Console.SetCursorPosition(skeletonKingX, skeletonKingY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)15);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapGraveyard.MaxWidth, MapGraveyard.MaxHeight);
                }
                if (skeletonKingY > Player.PlayerY1)
                {
                    Console.SetCursorPosition(skeletonKingX, skeletonKingY);
                    Console.Write(' ');
                    skeletonKingY -= 1;
                    Console.SetCursorPosition(skeletonKingX, skeletonKingY);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write((char)15);
                    Console.ResetColor();
                    Console.SetCursorPosition(MapGraveyard.MaxWidth, MapGraveyard.MaxHeight);
                }
            }
            if (skeletonKingX == Player.PlayerX1 && skeletonKingY == Player.PlayerY1)
            {
                Console.SetCursorPosition(skeletonKingX, skeletonKingY);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write((char)2);
                Console.ResetColor();
                skeletonKingHealth--;
                Player.Health-=2;
                MapGraveyard.graveyardFields[skeletonKingX, skeletonKingY].Content = new BlankSpace(new Vector2i(skeletonKingX, skeletonKingY));
                skeletonKingX = 0;
                skeletonKingY = 0;
                Console.SetCursorPosition(MapGraveyard.MaxWidth, MapGraveyard.MaxHeight);
            }


        }

    }

}
