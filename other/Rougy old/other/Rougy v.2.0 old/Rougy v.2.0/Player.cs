using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class Player : Objects
    {
        public override char Icon
        {
            get
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkCyan;

                return (char)2;
            }
        }
        public override string Name
        {
            get { return "Player"; }
        }
        public static int Health = 5;
        public Player(Vector2i position)
            : base(position)
        {
        }
        private int MaxWidth;
        private int MaxHeight;
        public static int PlayerX1;
        public static int PlayerY1;
        public void KeyInput()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {


                case ConsoleKey.W:
                    Console.SetCursorPosition(Position.X, Position.Y);
                    Console.Write(' ');
                    Move(0, -1);
                    Console.SetCursorPosition(Position.X, Position.Y);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write((char)2);
                    Console.ResetColor();
                    Console.SetCursorPosition(MaxWidth, MaxHeight);
                    break;
                case ConsoleKey.S:
                    Console.SetCursorPosition(Position.X, Position.Y);
                    Console.Write(' ');
                    Move(0, 1);
                    Console.SetCursorPosition(Position.X, Position.Y);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write((char)2);
                    Console.ResetColor();
                    Console.SetCursorPosition(MaxWidth, MaxHeight);
                    break;
                case ConsoleKey.A:
                    Console.SetCursorPosition(Position.X, Position.Y);
                    Console.Write(' ');
                    Move(-1, 0);
                    Console.SetCursorPosition(Position.X, Position.Y);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write((char)2);
                    Console.ResetColor();
                    Console.SetCursorPosition(MaxWidth, MaxHeight);
                    break;
                case ConsoleKey.D:
                    Console.SetCursorPosition(Position.X, Position.Y);
                    Console.Write(' ');
                    Move(1, 0);
                    Console.SetCursorPosition(Position.X, Position.Y);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write((char)2);
                    Console.ResetColor();
                    Console.SetCursorPosition(MaxWidth, MaxHeight);
                    break;


            }
        }


        private void Move(int PlayerX, int PlayerY)
        {
            Vector2i playerPosition = Position;
            if (Map.Level == 1)
            {
                playerPosition.X += PlayerX;
                playerPosition.Y += PlayerY;
                MaxHeight = MapCity.MaxHeight;
                MaxWidth = MapCity.MaxWidth;
                MapField nextField = MapCity.GetMapField(playerPosition);
                CheckWall(playerPosition, MaxHeight, MaxWidth);
                if (nextField.Content is BlankSpace)
                {
                    nextField.Content.Position = Position;
                    Position = playerPosition;
                }
                else
                {
                    CheckField(nextField);
                }
            }
            else if (Map.Level == 2)
            {
                playerPosition.X += PlayerX;
                playerPosition.Y += PlayerY;
                MaxHeight = MapForest.MaxHeight;
                MaxWidth = MapForest.MaxWidth;
                MapField nextField = MapForest.GetMapField(playerPosition);
                CheckWall(playerPosition, MaxHeight, MaxWidth);
                if (nextField.Content is BlankSpace)
                {

                    nextField.Content.Position = Position;
                    Position = playerPosition;
                }
                else
                {
                    CheckField(nextField);
                }
            }
            else if (Map.Level == 3)
            {
                playerPosition.X += PlayerX;
                playerPosition.Y += PlayerY;
                MaxHeight = MapGraveyard.MaxHeight;
                MaxWidth = MapGraveyard.MaxWidth;
                MapField nextField = MapGraveyard.GetMapField(playerPosition);
                CheckWall(playerPosition, MaxHeight, MaxWidth);
                if (nextField.Content is BlankSpace)
                {
                    nextField.Content.Position = Position;
                    Position = playerPosition;
                }
                else
                {
                    CheckField(nextField);
                }
            }
            PlayerX1 = playerPosition.X;
            PlayerY1 = playerPosition.Y;
        }
        private void CheckField(MapField nextField)
        {

            if (nextField.Content is PortalToForest)
            {
                Vector2i playerPosition = Position;
                Console.Clear();
                Map.Level = 2;
                playerPosition.X += 37;
                playerPosition.Y += 0;
                MapForest.CreateAfterPort();
                MapForest.DrawMap();
            }
            else if (nextField.Content is PortalToGraveyard && Key.KeyCounter > 0)
            {
                Vector2i playerPosition = Position;
                Console.Clear();
                Map.Level = 3;
                playerPosition.X += 0;
                playerPosition.Y += 17;
                MapGraveyard.CreateAfterPort();
                MapGraveyard.DrawMap();
            }
            else if (nextField.Content is PortalToCity)
            {
                Vector2i playerPosition = Position;
                Console.Clear();
                if (Map.Level == 2)
                {
                    Map.Level = 1;
                    playerPosition.X = 1;
                    playerPosition.Y = 3;
                }
                else if (Map.Level == 3)
                {
                    Map.Level = 1;
                    playerPosition.X = 10;
                    playerPosition.Y = 1;
                }

                MapCity.CreateAfterPort();
                MapCity.DrawMap();

            }
            else if (nextField.Content is Ork)
            {
                Ork Ork = (Ork)nextField.Content;
                nextField.Content = new BlankSpace(new Vector2i(nextField.Position));
                Health--;
            }
            else if (nextField.Content is Skeleton)
            {
                Skeleton Skeleton = (Skeleton)nextField.Content;
                nextField.Content = new BlankSpace(new Vector2i(nextField.Position));
                Health--;
            }
            else if (nextField.Content is SkeletonKing)
            {
                SkeletonKing SkeletonKing = (SkeletonKing)nextField.Content;
                nextField.Content = new Triforce(new Vector2i(nextField.Position));
            } 
            else if (nextField.Content is Gold)
            {
                nextField.Content = new BlankSpace(new Vector2i(nextField.Position));
                Gold.GoldCounter += 1;
            }
            else if (nextField.Content is Key)
            {
                nextField.Content = new BlankSpace(new Vector2i(nextField.Position));
                Key.KeyCounter++;
            }
            else if (nextField.Content is Triforce)
            {
                nextField.Content = new BlankSpace(new Vector2i(nextField.Position));
                Triforce.HaveTriforce = true;
            }



        }
        private void CheckWall(Vector2i playerPosition, int maxHeight, int maxWidth)
        {

            if (playerPosition.X - 1 < 1)
                playerPosition.X = 1;
            if (playerPosition.X + 1 >= maxWidth)
                playerPosition.X = 38;
            if (playerPosition.Y - 1 < 1)
                playerPosition.Y = 1;
            if (playerPosition.Y + 1 >= maxHeight)
                playerPosition.Y = 18;
        }


    }
}
