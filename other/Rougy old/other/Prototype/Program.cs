using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Rougy
{
    class Program
    {  // Initialisierung ----------------------------------------

        // Map
        const int MapWidth = 40;
        const int MapHeight = 20;
        const int MaxRight = MapWidth - 1;
        const int MaxLeft = 0;
        const int MaxBot = MapHeight - 1;
        const int MaxTop = 0;
        static int MapLevel = 1;
        static string MapName;
        public static char[,] MapCity = new char[MapWidth, MapHeight];
        public static char[,] MapForest = new char[MapWidth, MapHeight];
        public static char[,] MapGraveyard = new char[MapWidth, MapHeight];
        // static char[,] map4 = new char[MapWidth, MapHeight];
        //static char[,] Citymap = MapCity("C:\\Users\\Xells\\Desktop\\SAE\\rougy 1.6\\MapCity.txt");
        // Objects
        const char HealthPot = '.';
        const char Roof = 'o';
        const char Player = (char)2;
        const char Treasure = 'X';
        const char BlankSpace = ' ';
        const char Wall = '#';
        const char House = 'H';
        const char Gold = '$';
        static char Orc = (char)190;
        const char Tree = 'B';
        const char Water = (char)178;
        const char PortalTo3Close = 'X';
        const char PortalTo3Open = 'O';
        const char PortalTo2 = 'O';
        const char PortalTo1 = 'O';
        static int HealthCounter = 5;
        const char Graveyard = (char)197;
        const char GraveyardWall = (char)204;
        static char Key = (char)208;
        // Counter and Positions
        static int CounterOrc = 0;
        static int HowManyOrcs = 2;
        static int GoldCounter = 0;
        static int KeyCounter = 0;
        static int PlayerX = 3;
        static int PlayerY = 3;
        // Other
        static bool Moving = false;
        static Random Rnd = new Random();
        static int Fps = Rnd.Next(66, 74);
        static int SetKey = 0;
        static int KeyX = Rnd.Next(1, 38);
        static int KeyY = Rnd.Next(1, 18);
        static int GoldAmount1 = 0;
        static int GoldAmount2 = 0;
        static int GoldAmount3 = 0;
        static int HowManyGold = Rnd.Next(3, 7);
        static int HowManyTrees = Rnd.Next(35, 70);
        static int OrcOneX = Rnd.Next(1, 38);
        static int OrcOneY = Rnd.Next(1, 18);
        static int TreeAmount = 0;
        static bool enemyDead = false;

        // --------------------------------------------------------
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.GetEncoding(1252);
            // Welcome();
            // Intro();
            BlankspaceMapOne();
            BlankspaceMapTwo();
            BlankspaceMapGraveyard();
            FixPositions();
            WriteMapCity();
            while (true)
            {
                PlayerMap();
            }
        }
        static void Intro()
        {
            Console.Write("Hey you! Wake up! I just got mugged!!\n\n");
            Console.Write("You gonna help me, right? RIGHT?? ");
            Console.Write("Bye the way, whats ur name??\n\n ");
            string name = Convert.ToString(Console.ReadLine());
            Console.WriteLine("\nUhm.. " + name + " is a strange name.. I will just call u Link. ");
            Console.WriteLine("\nSo Link, please help me! Go out there and bring back my artefact!");
            Console.WriteLine();
   //         Console.Write("\n\n\n\n\n                                                   ");
            PressH();
        }
        static void Move(char[,] map)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            PlayerKilledEnemy(map, Orc, key);
            DeadOrc(MapForest, Orc, key);

            // to lvl 3
            if (key.Key == ConsoleKey.A && map[PlayerX - 1, PlayerY] == Key)
            {
                KeyCounter++;
            }
            else if (key.Key == ConsoleKey.S && map[PlayerX, PlayerY + 1] == Key)
            {
                KeyCounter++;
            }
            else if (key.Key == ConsoleKey.D && map[PlayerX + 1, PlayerY] == Key)
            {
                KeyCounter++;
            }
            else if (key.Key == ConsoleKey.W && map[PlayerX, PlayerY - 1] == Key)
            {
                KeyCounter++;
            }
            if (key.Key == ConsoleKey.A && map[PlayerX - 1, PlayerY] == BlankSpace || map[PlayerX - 1, PlayerY] == Key || map[PlayerX - 1, PlayerY] == Gold)
            {
                Console.SetCursorPosition(PlayerX, PlayerY);
                Console.Write(BlankSpace);
                map[PlayerX, PlayerY] = BlankSpace;
                PlayerX -= 1;
                GoldGold(map);
                map[PlayerX, PlayerY] = Player;
                Console.SetCursorPosition(PlayerX, PlayerY);
                ColorPlayer(PlayerX, PlayerY, map);
                Console.SetCursorPosition(MapWidth, MapHeight);
                Moving = true;
                Hud();
            }
            else if (key.Key == ConsoleKey.D && map[PlayerX + 1, PlayerY] == BlankSpace || map[PlayerX + 1, PlayerY] == Key || map[PlayerX + 1, PlayerY] == Gold)
            {
                Console.SetCursorPosition(PlayerX, PlayerY);
                Console.Write(BlankSpace);
                map[PlayerX, PlayerY] = BlankSpace;
                PlayerX += 1;
                GoldGold(map);
                map[PlayerX, PlayerY] = Player;
                Console.SetCursorPosition(PlayerX, PlayerY);
                ColorPlayer(PlayerX, PlayerY, map);
                Console.SetCursorPosition(MapWidth, MapHeight);
                Moving = true;
                Hud();
            }
            else if (key.Key == ConsoleKey.W && map[PlayerX, PlayerY - 1] == BlankSpace || map[PlayerX, PlayerY - 1] == Key || map[PlayerX, PlayerY - 1] == Gold)
            {
                Console.SetCursorPosition(PlayerX, PlayerY);
                Console.Write(BlankSpace);
                map[PlayerX, PlayerY] = BlankSpace;
                PlayerY -= 1;
                GoldGold(map);
                map[PlayerX, PlayerY] = Player;
                Console.SetCursorPosition(PlayerX, PlayerY);
                ColorPlayer(PlayerX, PlayerY, map);
                Console.SetCursorPosition(MapWidth, MapHeight);
                Moving = true;
                Hud();
            }
            else if (key.Key == ConsoleKey.S && map[PlayerX, PlayerY + 1] == BlankSpace || map[PlayerX, PlayerY + 1] == Key || map[PlayerX, PlayerY + 1] == Gold)
            {
                Console.SetCursorPosition(PlayerX, PlayerY);
                Console.Write(BlankSpace);
                map[PlayerX, PlayerY] = BlankSpace;
                PlayerY += 1;
                GoldGold(map);
                map[PlayerX, PlayerY] = Player;
                Console.SetCursorPosition(PlayerX, PlayerY);
                ColorPlayer(PlayerX, PlayerY, map);
                Console.SetCursorPosition(MapWidth, MapHeight);
                Moving = true;
                Hud();
            }
            // To lvl 1
            if (key.Key == ConsoleKey.D && map[PlayerX + 1, PlayerY] == PortalTo1 && MapLevel == 2)
            {
                map[PlayerX, PlayerY] = BlankSpace;
                MapLevel = 1;
                PlayerX = 1;
                PlayerY = 3;
                TreeAmount = 0;
                BlankSpaceWithoutTree();
                WriteMapCity();
                Hud();
            }
            if (key.Key == ConsoleKey.S && map[PlayerX, PlayerY + 1] == PortalTo1 && MapLevel == 3)
            {
                map[PlayerX, PlayerY] = BlankSpace;
                MapLevel = 1;
                PlayerX = 10;
                PlayerY = 1;
                WriteMapCity();
                Hud();
            }
            // To lvl 2
            if (key.Key == ConsoleKey.A && map[PlayerX - 1, PlayerY] == PortalTo2 && MapLevel == 1)
            {
                MapCity[PlayerX, PlayerY] = BlankSpace;
                MapLevel = 2;
                PlayerX = 38;
                PlayerY = 3;
                WriteMapForest();
                Hud();
            }
            // To lvl 3
            if (key.Key == ConsoleKey.W && map[PlayerX, PlayerY - 1] == PortalTo3Open && MapLevel == 1)
            {
                MapCity[PlayerX, PlayerY] = BlankSpace;
                PlayerY -= 1;
                PlayerX = 3;
                PlayerY = 18;
                MapLevel = 3;
                WriteMapGraveyard();
                Hud();
            }
            if (key.Key == ConsoleKey.O)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("      /\\     ");
                Console.WriteLine("     /--\\    ");
                Console.WriteLine("    /_\\/_\\  ");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Enter your Cheat: ");
                String read = Console.ReadLine();
                Cheats(read);
                Hud();
            }
            if (key.Key == ConsoleKey.H)
            {
                Console.Clear();
                Console.WriteLine("                                                                           ");
                Console.WriteLine("         Press \"W\" to Move Up                                            ");
                Console.WriteLine("         Press \"S\" to Move Down                                          ");
                Console.WriteLine("         Press \"A\" to Move Left                                          ");
                Console.WriteLine("         Press \"D\" to Move Right                                         ");
                Console.WriteLine("         Press \"B\" to Buy                                                ");
                Console.WriteLine("         Press \"O\" to insert your Cheat.                                 ");
                Console.WriteLine("                                                                           ");
                Console.WriteLine("         Rougy is created by Julian Hopp and Daniel Lause.                 ");
                Console.ReadKey(true);
                if (MapLevel == 1)
                {
                    WriteMapCity();
                } if (MapLevel == 2)
                {
                    WriteMapForest();
                } if (MapLevel == 3)
                {
                    WriteMapGraveyard();
                }
            }
            if (key.Key == ConsoleKey.B)
            {
                Console.Clear();
                Console.WriteLine("Do you want to Buy one Heart for 10 Gold? \nPress Y for Yes \nPress N for No");
                Console.ReadKey();

                if (GoldCounter >= 10)
                {
                    GoldCounter -= 10;
                    HealthCounter += 1;
                    if (MapLevel == 1)
                    {
                        WriteMapCity();
                    } if (MapLevel == 2)
                    {
                        WriteMapForest();
                    } if (MapLevel == 3)
                    {
                        WriteMapGraveyard();
                    }
                }
                else
                {
                    Console.WriteLine("\n You are to short on gold");
                    Console.ReadKey();
                    if (MapLevel == 1)
                    {
                        WriteMapCity();
                    } if (MapLevel == 2)
                    {
                        WriteMapForest();
                    } if (MapLevel == 3)
                    {
                        WriteMapGraveyard();
                    }
                }
                if (key.Key == ConsoleKey.N)
                {
                    if (MapLevel == 1)
                    {
                        WriteMapCity();
                    } if (MapLevel == 2)
                    {
                        WriteMapForest();
                    } if (MapLevel == 3)
                    {
                        WriteMapGraveyard();
                    }
                }
            }
        }
        static void Portals()
        {
            MapCity[0, 3] = PortalTo2;
            if (KeyCounter == 0)
            {
                MapCity[10, 0] = PortalTo3Close;
            }
            if (KeyCounter > 0)
            {
                MapCity[10, 0] = PortalTo3Open;
            }
            MapForest[MaxRight, 3] = PortalTo1;
            MapGraveyard[3, MaxBot] = PortalTo1;
        }
        static void WriteMapCity()
        {
            Console.Clear();
            Moving = true;
            MapCity[PlayerX, PlayerY] = Player;
            GoldSpawn(MapCity);
            Portals();
            for (int y = 0; y < MapCity.GetLength(1); y++)
            {
                for (int x = 0; x < MapCity.GetLength(0); x++)
                {
                    ColorBlankspace(x, y, MapCity);
                    ColorWall(x, y, MapCity);
                    ColorHouseRoof(x, y, MapCity);
                    ColorHouse(x, y, MapCity);
                    ColorPlayer(x, y, MapCity);
                    ColorPortal2(x, y, MapCity);
                    ColorPortal3(x, y, MapCity);
                    ColorGraveyard(x, y, MapCity);
                    ColorGold(x, y, MapCity);
                }
                Console.WriteLine();
            }
        }
        static void ColorKey(int x, int y, char[,] map)
        {
            {
                if (KeyCounter == 0)
                    if (map[x, y] == Key)
                    {
                        Colors("White");
                        Console.Write(Key);
                        Console.ResetColor();
                    }
            }
        }
        static void WriteMapForest()
        {
            Console.Clear();
            Moving = true;
            MapForest[PlayerX, PlayerY] = Player;
            GoldSpawn(MapForest);
            TreeSpawn(MapForest);
            KeySpawnForest(MapForest);
            EnemyOrkSpawn(MapForest);

            for (int y = 0; y < MapForest.GetLength(1); y++)
            {
                for (int x = 0; x < MapForest.GetLength(0); x++)
                {
                    ColorBlankspace(x, y, MapForest);
                    ColorTree(x, y, MapForest);
                    ColorPortal1(x, y, MapForest);
                    ColorPlayer(x, y, MapForest);
                    ColorKey(x, y, MapForest);
                    ColorGold(x, y, MapForest);
                    ColorWater(x, y, MapForest);
                    ColorOrc(x, y, MapForest);
                }
                Console.WriteLine();
            }
        }
        static void WriteMapGraveyard()
        {
            Console.Clear();
            Moving = true;
            MapGraveyard[PlayerX, PlayerY] = Player;
            if (KeyX != 0 && KeyY != 0)
            {
                MapForest[KeyX, KeyY] = Key;
            }
            GoldSpawn(MapGraveyard);
            for (int y = 0; y < MapGraveyard.GetLength(1); y++)
            {
                for (int x = 0; x < MapGraveyard.GetLength(0); x++)
                {

                    ColorBlankspace(x, y, MapGraveyard);
                    ColorGraveyardWall(x, y, MapGraveyard);
                    ColorGraveyard(x, y, MapGraveyard);
                    ColorPortal1(x, y, MapGraveyard);
                    ColorPlayer(x, y, MapGraveyard);
                    ColorKey(x, y, MapGraveyard);
                    ColorGold(x, y, MapGraveyard);
                }
                Console.WriteLine();
            }
        }
        static void BlankspaceMapOne()
        {
            for (int y = 0; y < MapCity.GetLength(1); y++)
            {
                for (int x = 0; x < MapCity.GetLength(0); x++)
                {
                    MapCity[x, y] = BlankSpace;
                    if (y == 0 || x == 0 || x == MaxRight || y == MaxBot)
                    {
                        MapCity[x, y] = Wall;
                    }
                }
            }
        }
        static void BlankspaceMapTwo()
        {
            for (int y = 0; y < MapForest.GetLength(1); y++)
            {
                for (int x = 0; x < MapForest.GetLength(0); x++)
                {
                    MapForest[x, y] = BlankSpace;

                    if (y == 0 || x == 0 || x == MaxRight || y == MaxBot)
                    {
                        MapForest[x, y] = Tree;
                    }
                }
            }
        }

        static void BlankspaceMapGraveyard()
        {
            for (int y = 0; y < MapGraveyard.GetLength(1); y++)
            {
                for (int x = 0; x < MapGraveyard.GetLength(0); x++)
                {
                    MapGraveyard[x, y] = BlankSpace;
                    if (y == 0 || x == 0 || x == MaxRight || y == MaxBot)
                    {
                        MapGraveyard[x, y] = GraveyardWall;
                    }
                }
            }
        }
        static void WriteHouse()
        {
            MapCity[6, 6] = House;
            MapCity[6, 7] = House;
            MapCity[6, 8] = House;
            MapCity[6, 9] = House;
            MapCity[6, 10] = House;
            MapCity[7, 10] = House;
            MapCity[8, 10] = House;
            MapCity[9, 10] = House;
            MapCity[7, 6] = House;
            MapCity[8, 6] = House;
            MapCity[9, 6] = House;
            MapCity[10, 6] = House;
            MapCity[11, 6] = House;
            MapCity[11, 7] = House;
            MapCity[11, 8] = House;
            MapCity[11, 9] = House;
            MapCity[11, 10] = House;
        }
        //Colors and Objects
        static void ColorPlayer(int x, int y, char[,] map)
        {
            if (map[x, y] == Player)
            {
                //Console.BackgroundColor = ConsoleColor.Black;
                Colors("darkGreen");
                Console.Write(Player);
                Console.ResetColor();
            }
        }
        static void ColorWall(int x, int y, char[,] map)
        {
            if (map[x, y] == Wall)
            {
                Colors("cyan");
                Console.Write(Wall);
                Console.ResetColor();
            }
        }
        static void ColorHouse(int x, int y, char[,] map)
        {
            if (map[x, y] == House)
            {
                Colors("darkGray");
                Console.Write(House);
                Console.ResetColor();
            }
        }
        static void ColorBlankspace(int x, int y, char[,] map)
        {
            if (map[x, y] == BlankSpace)
            {
                //  Console.BackgroundColor = ConsoleColor.Red;
                Console.Write(BlankSpace);
                //  Console.ResetColor();
            }
        }
        static void ColorTreasure(int x, int y, char[,] map)
        {
            if (map[x, y] == Treasure)
            {
                Colors("cyan");
                Console.Write(Treasure);
                Console.ResetColor();
            }
        }
        static void ColorGraveyard(int x, int y, char[,] map)
        {
            if (map[x, y] == Graveyard)
            {
                Colors("red");
                Console.Write(Graveyard);
                Console.ResetColor();
            }
        }
        static void ColorHealth(int healthcounter)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < healthcounter; i++)
            {
                Console.Write((char)3);
            }
            Console.ResetColor();
        }
        static void ColorGold(int x, int y, char[,] map)
        {
            if (map[x, y] == Gold)
            {
                Colors("yellow");
                Console.Write(Gold);
                Console.ResetColor();
            }
        }
        static void ColorOrc(int x, int y, char[,] map)
        {
            if (map[x, y] == Orc)
            {
                Colors("red");
                Console.Write(Orc);
                Console.ResetColor();
            }
        }
        static void ColorGraveyardWall(int x, int y, char[,] map)
        {
            if (map[x, y] == GraveyardWall)
            {
                Colors("red");
                Console.Write(GraveyardWall);
                Console.ResetColor();
            }
        }
        static void ColorHouseRoof(int x, int y, char[,] map)
        {
            if (map[x, y] == Roof)
            {
                Colors("red");
                Console.Write(Roof);
                Console.ResetColor();
            }
        }
        static void ColorWater(int x, int y, char[,] map)
        {
            if (map[x, y] == Water)
            {
                Colors("blue");
                Console.Write(Water);
                Console.ResetColor();
            }
        }
        static void ColorTree(int x, int y, char[,] map)
        {
            if (map[x, y] == Tree)
            {
                Colors("green");
                Console.Write(Tree);
                Console.ResetColor();
            }
        }
        static void ColorPortal1(int x, int y, char[,] map)
        {
            if (map[x, y] == PortalTo1)
            {

                Colors("Blue");
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write(PortalTo2);
                Console.ResetColor();
            }
        }
        static void ColorPortal2(int x, int y, char[,] map)
        {
            if (map[x, y] == PortalTo2)
            {
                Colors("Red");
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write(PortalTo1);
                Console.ResetColor();
            }
        }
        static void ColorPortal3(int x, int y, char[,] map)
        {
            if (KeyCounter == 0)
            {
                if (map[x, y] == PortalTo3Close)
                {
                    Colors("Red");
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write(PortalTo3Close);
                    Console.ResetColor();
                }
            }
        }
        static void Colors(string color)
        {

            switch (color)
            {
                case "darkBlue": Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case "darkCyan": Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case "darkGray": Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case "darkGreen": Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case "darkMagenta": Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case "DarkRed": Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "darkYellow": Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "black": Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case "blue": Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "cyan": Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "gray": Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "green": Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "magenta": Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "red": Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "white": Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "yellow": Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
        }
        static void GoldGold(char[,] map)
        {
            if (map[PlayerX, PlayerY] == Gold)
            {
                GoldCounter++;
            }
        }

        static void KeySpawnForest(char[,] map)
        {
            if (/*KeyX != 0 && KeyY != 0 && */KeyCounter == 0 && map[KeyX, KeyY] == BlankSpace && SetKey == 0)
            {
                MapForest[KeyX, KeyY] = Key;
                SetKey = 1;
            }
            else if (KeyX != 0 && KeyY != 0 && KeyCounter == 0 && map[KeyX, KeyY] != BlankSpace)
            {
                KeyX = Rnd.Next(1, 38);
                KeyY = Rnd.Next(1, 18);
                KeySpawnForest(MapForest);
            }
            if (KeyCounter > 0)
            {
                MapForest[KeyX, KeyY] = Tree;
            }
        }
        //static void HealthPot(char[,] map)
        //{

        //}

        static void GoldSpawn(char[,] map)
        {
            if (map == MapCity && GoldAmount1 < HowManyGold)
            {
                for (int i = 0; i <= HowManyGold; i++)
                {
                    int GoldX = Rnd.Next(1, 38);
                    int GoldY = Rnd.Next(1, 18);
                    if (map[GoldX, GoldY] == BlankSpace)
                    {
                        map[GoldX, GoldY] = Gold;
                    }
                    GoldAmount1 = HowManyGold;
                }
            }
            if (map == MapForest && GoldAmount2 < HowManyGold)
            {
                for (int i = 0; i <= HowManyGold; i++)
                {
                    int GoldX = Rnd.Next(1, 38);
                    int GoldY = Rnd.Next(1, 18);

                    if (map[GoldX, GoldY] == BlankSpace)
                    {
                        map[GoldX, GoldY] = Gold;
                    }
                    GoldAmount2 = HowManyGold;
                }
            }
            if (map == MapGraveyard && GoldAmount3 < HowManyGold)
            {
                for (int j = 0; j <= HowManyGold; j++)
                {
                    int GoldX = Rnd.Next(1, 38);
                    int GoldY = Rnd.Next(1, 18);
                    if (map[GoldX, GoldY] == BlankSpace)
                    {
                        map[GoldX, GoldY] = Gold;
                    }
                    GoldAmount3 = HowManyGold;
                }
            }
        }
        static void TreeSpawn(char[,] map)
        {
            if (map == MapForest && TreeAmount < HowManyTrees)
            {
                for (int i = 0; i <= HowManyTrees; i++)
                {
                    int TreeX = Rnd.Next(1, 38);
                    int TreeY = Rnd.Next(1, 18);
                    if (map[TreeX, TreeY] == BlankSpace)
                    {
                        map[TreeX, TreeY] = Tree;
                    }
                    TreeAmount = HowManyTrees;
                }
            }
        }
        static void BlankSpaceWithoutTree()
        {
            for (int y = 0; y < MapForest.GetLength(1); y++)
            {
                for (int x = 0; x < MapForest.GetLength(0); x++)
                {
                    if (MapForest[x, y] == Tree)
                    {
                        MapForest[x, y] = BlankSpace;
                    }
                    if (y == 0 || x == 0 || x == MaxRight || y == MaxBot)
                    {
                        MapForest[x, y] = Tree;
                    }
                }
            }
        }

        static void Cheats(string read)
        {
            if (read == "Gold" || read == "gold")
            {
                GoldCounter = 99999;
            }
            else if (read == "Key" || read == "key")
            {
                KeyCounter = 99;
                MapForest[KeyX, KeyY] = BlankSpace;

            }
            else if (read == "Map1" || read == "map1")
            {

                if (MapLevel == 2)
                {
                    MapForest[PlayerX, PlayerY] = BlankSpace;
                    MapGraveyard[PlayerX, PlayerY] = BlankSpace;
                    MapLevel = 1;
                    PlayerX = 8;
                    PlayerY = 8;
                    WriteMapCity();
                }
                else if (MapLevel == 3)
                {
                    MapGraveyard[PlayerX, PlayerY] = BlankSpace;
                    MapForest[PlayerX, PlayerY] = BlankSpace;
                    MapLevel = 1;
                    PlayerX = 8;
                    PlayerY = 8;
                    WriteMapCity();
                }

            }
            else if (read == "Map2" || read == "map2")
            {

                if (MapLevel == 1)
                {
                    MapCity[PlayerX, PlayerY] = BlankSpace;
                    MapGraveyard[PlayerX, PlayerY] = BlankSpace;
                    MapLevel = 2;
                    PlayerX = 38;
                    PlayerY = 3;
                    WriteMapForest();
                }
                else if (MapLevel == 3)
                {
                    MapGraveyard[PlayerX, PlayerY] = BlankSpace;
                    MapCity[PlayerX, PlayerY] = BlankSpace;
                    MapLevel = 2;
                    PlayerX = 38;
                    PlayerY = 3;
                    WriteMapForest();
                }
            }
            else if (read == "Map3" || read == "map3")
            {

                if (MapLevel == 1)
                {
                    MapCity[PlayerX, PlayerY] = BlankSpace;
                    MapForest[PlayerX, PlayerY] = BlankSpace;
                    MapLevel = 3;
                    PlayerX = 3;
                    PlayerY = 18;
                    WriteMapGraveyard();
                }
                else if (MapLevel == 2)
                {
                    MapForest[PlayerX, PlayerY] = BlankSpace;
                    MapCity[PlayerX, PlayerY] = BlankSpace;
                    MapLevel = 3;
                    PlayerX = 3;
                    PlayerY = 18;
                    WriteMapGraveyard();
                }
            }
            if (MapLevel == 1)
            {
                WriteMapCity();
            } if (MapLevel == 2)
            {
                WriteMapForest();
            } if (MapLevel == 3)
            {
                WriteMapGraveyard();
            }
        }
        static void EnemyOrkSpawn(char[,] map)
        {
            if (enemyDead == false)
            {

                OrcOneX = Rnd.Next(1, 38);
                OrcOneY = Rnd.Next(1, 18);
                if (map[OrcOneX, OrcOneY] == BlankSpace)
                {
                    map[OrcOneX, OrcOneY] = Orc;
                }
            }
        }
        static void EnemyKI()
        {
            if (MapLevel == 2 && enemyDead == false)
            {
                //int OrcRun = rnd.Next(1, 5);

                if (OrcOneX < PlayerX && MapForest[OrcOneX + 1, OrcOneY] == BlankSpace) //&& OrcOneX - PlayerX < 10)
                {
                    Console.SetCursorPosition(OrcOneX, OrcOneY);
                    Console.Write(BlankSpace);
                    MapForest[OrcOneX, OrcOneY] = BlankSpace;
                    OrcOneX += 1;
                    MapForest[OrcOneX, OrcOneY] = Orc;
                    Console.SetCursorPosition(OrcOneX, OrcOneY);
                    ColorOrc(OrcOneX, OrcOneY, MapForest);
                    Console.SetCursorPosition(MapWidth, MapHeight);
                }
                if (OrcOneX > PlayerX && MapForest[OrcOneX - 1, OrcOneY] == BlankSpace) //&& OrcOneX - PlayerX > 10)
                {
                    Console.SetCursorPosition(OrcOneX, OrcOneY);
                    Console.Write(BlankSpace);
                    MapForest[OrcOneX, OrcOneY] = BlankSpace;
                    OrcOneX -= 1;
                    MapForest[OrcOneX, OrcOneY] = Orc;
                    Console.SetCursorPosition(OrcOneX, OrcOneY);
                    ColorOrc(OrcOneX, OrcOneY, MapForest);
                    Console.SetCursorPosition(MapWidth, MapHeight);
                }
                if (OrcOneY > PlayerY && MapForest[OrcOneX, OrcOneY - 1] == BlankSpace) //&& OrcOneY - PlayerY > 10)
                {
                    Console.SetCursorPosition(OrcOneX, OrcOneY);
                    Console.Write(BlankSpace);
                    MapForest[OrcOneX, OrcOneY] = BlankSpace;
                    OrcOneY -= 1;
                    MapForest[OrcOneX, OrcOneY] = Orc;
                    Console.SetCursorPosition(OrcOneX, OrcOneY);
                    ColorOrc(OrcOneX, OrcOneY, MapForest);
                    Console.SetCursorPosition(MapWidth, MapHeight);
                }
                if (OrcOneX < PlayerX && MapForest[OrcOneX, OrcOneY + 1] == BlankSpace) //&& OrcOneY - PlayerY < 10)
                {
                    Console.SetCursorPosition(OrcOneX, OrcOneY);
                    Console.Write(BlankSpace);
                    MapForest[OrcOneX, OrcOneY] = BlankSpace;
                    OrcOneY += 1;
                    MapForest[OrcOneX, OrcOneY] = Orc;
                    Console.SetCursorPosition(OrcOneX, OrcOneY);
                    ColorOrc(OrcOneX, OrcOneY, MapForest);
                    Console.SetCursorPosition(MapWidth, MapHeight);
                }

                //else
                //{
                //    int OrcRun = Rnd.Next(1, 5);
                //    if (OrcRun == 1 && MapForest[OrcOneX - 1, OrcOneY] == BlankSpace)
                //    {
                //        Console.SetCursorPosition(OrcOneX, OrcOneY);
                //        Console.Write(BlankSpace);
                //        MapForest[OrcOneX, OrcOneY] = BlankSpace;
                //        OrcOneX -= 1;
                //        MapForest[OrcOneX, OrcOneY] = Orc;
                //        Console.SetCursorPosition(OrcOneX, OrcOneY);
                //        ColorOrc(OrcOneX, OrcOneY, MapForest);
                //        Console.SetCursorPosition(MapWidth, MapHeight);
                //    }
                //    if (OrcRun == 2 && MapForest[OrcOneX + 1, OrcOneY] == BlankSpace)
                //    {
                //        Console.SetCursorPosition(OrcOneX, OrcOneY);
                //        Console.Write(BlankSpace);
                //        MapForest[OrcOneX, OrcOneY] = BlankSpace;
                //        OrcOneX += 1;
                //        MapForest[OrcOneX, OrcOneY] = Orc;
                //        Console.SetCursorPosition(OrcOneX, OrcOneY);
                //        ColorOrc(OrcOneX, OrcOneY, MapForest);
                //        Console.SetCursorPosition(MapWidth, MapHeight);
                //    }
                //    if (OrcRun == 3 && MapForest[OrcOneX, OrcOneY - 1] == BlankSpace)
                //    {
                //        Console.SetCursorPosition(OrcOneX, OrcOneY);
                //        Console.Write(BlankSpace);
                //        MapForest[OrcOneX, OrcOneY] = BlankSpace;
                //        OrcOneY -= 1;
                //        MapForest[OrcOneX, OrcOneY] = Orc;
                //        Console.SetCursorPosition(OrcOneX, OrcOneY);
                //        ColorOrc(OrcOneX, OrcOneY, MapForest);
                //        Console.SetCursorPosition(MapWidth, MapHeight);
                //    }
                //    if (OrcRun == 4 && MapForest[OrcOneX, OrcOneY + 1] == BlankSpace)
                //    {
                //        Console.SetCursorPosition(OrcOneX, OrcOneY);
                //        Console.Write(BlankSpace);
                //        MapForest[OrcOneX, OrcOneY] = BlankSpace;
                //        OrcOneY += 1;
                //        MapForest[OrcOneX, OrcOneY] = Orc;
                //        Console.SetCursorPosition(OrcOneX, OrcOneY);
                //        ColorOrc(OrcOneX, OrcOneY, MapForest);
                //        Console.SetCursorPosition(MapWidth, MapHeight);
                //    }
                //}
            }
        }
        static void PlayerMap()
        {
            if (MapLevel == 1)
            {
                Move(MapCity);
                EnemyKI();
            }
            else if (MapLevel == 2)
            {
                Move(MapForest);
                EnemyKI();
            }
            else if (MapLevel == 3)
            {
                Move(MapGraveyard);
                EnemyKI();
            }
        }
        static void Hud()
        {
            if (MapLevel == 1)
            {
                MapName = "City";
            }
            if (MapLevel == 2)
            {
                MapName = "Forest";
            }
            if (MapLevel == 3)
            {
                MapName = "Graveyard";
            }
            if (Moving == true)
            {
                Console.Write("\n" + "Key:" + KeyCounter + "  Map:" + MapName + "  Gold:" + GoldCounter + "  ");
                ColorHealth(HealthCounter);
                Console.Write(" FPS:  " + Rnd.Next(125, 145));
            }
        }
        static void FixPositions()
        {
            MapCity[0, 3] = PortalTo2;
            if (KeyCounter == 0)
            {
                MapCity[10, 0] = PortalTo3Close;
            }
            if (KeyCounter > 0)
            {
                MapCity[10, 0] = PortalTo3Open;
            }

            MapForest[MaxRight, 3] = PortalTo1;
            MapGraveyard[3, MaxBot] = PortalTo1;

            // Map 1

            MapCity[7, 7] = Roof;
            MapCity[8, 7] = Roof;
            MapCity[9, 7] = Roof;
            MapCity[10, 7] = Roof;
            MapCity[8, 6] = Roof;
            MapCity[9, 6] = Roof;

            MapCity[6, 8] = House;
            MapCity[6, 9] = House;
            MapCity[6, 10] = House;
            MapCity[7, 8] = House;
            MapCity[7, 9] = House;
            MapCity[7, 10] = House;
            MapCity[8, 8] = House;
            MapCity[8, 9] = House;
            MapCity[8, 10] = House;
            MapCity[9, 8] = House;
            MapCity[9, 9] = House;
            MapCity[9, 10] = House;
            MapCity[10, 8] = House;
            MapCity[10, 9] = House;
            //map1[10, 10] = House;   // Door
            MapCity[11, 8] = House;
            MapCity[11, 9] = House;
            MapCity[11, 10] = House;


            // Map 2
            MapForest[1, 1] = Water;
            MapForest[1, 2] = Water;
            MapForest[1, 3] = Water;
            MapForest[1, 4] = Water;
            MapForest[1, 5] = Water;
            MapForest[1, 6] = Water;
            MapForest[2, 1] = Water;
            MapForest[2, 2] = Water;
            MapForest[2, 3] = Water;
            MapForest[2, 4] = Water;
            MapForest[2, 5] = Water;
            MapForest[3, 1] = Water;
            MapForest[3, 2] = Water;
            MapForest[3, 3] = Water;
            MapForest[3, 4] = Water;
            MapForest[4, 1] = Water;
            MapForest[4, 2] = Water;
            MapForest[4, 3] = Water;
            MapForest[5, 1] = Water;
            MapForest[5, 2] = Water;
            MapForest[6, 1] = Water;

            // Map 3
            MapGraveyard[3, 5] = Graveyard;
            MapGraveyard[3, 10] = Graveyard;
            MapGraveyard[3, 15] = Graveyard;
            MapGraveyard[8, 5] = Graveyard;
            MapGraveyard[8, 10] = Graveyard;
            MapGraveyard[8, 15] = Graveyard;
            MapGraveyard[13, 5] = Graveyard;
            MapGraveyard[13, 10] = Graveyard;
            MapGraveyard[13, 15] = Graveyard;
            MapGraveyard[18, 5] = Graveyard;
            MapGraveyard[18, 10] = Graveyard;
            MapGraveyard[18, 15] = Graveyard;
            MapGraveyard[23, 5] = Graveyard;
            MapGraveyard[23, 10] = Graveyard;
            MapGraveyard[23, 15] = Graveyard;
            MapGraveyard[28, 5] = Graveyard;
            MapGraveyard[28, 10] = Graveyard;
            MapGraveyard[28, 15] = Graveyard;
            MapGraveyard[33, 5] = Graveyard;
            MapGraveyard[33, 10] = Graveyard;
            MapGraveyard[33, 15] = Graveyard;

        }
        static void PressH()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.H)
            {
                Console.Clear();
                Console.WriteLine("\n\n\n\n FPS: " + Fps + "\n\n\n\nPress \"WASD\" to Move \n\n \"I\" for Inventory\n\n \"M\" for Map");

                Console.ReadKey(true);
                if (MapLevel == 1)
                {
                    WriteMapCity();
                } if (MapLevel == 2)
                {
                    WriteMapForest();
                } if (MapLevel == 3)
                {
                    WriteMapGraveyard();
                }
            }
        }
        static void Welcome()
        {
            Console.WriteLine("WW             WW  EEEEE  LL      CCCC    OOOOO    MMM       MMM  EEEEE   ");
            Console.WriteLine(" WW           WW   EE     LL    CCC      OO   OO   MMMM     MMMM  EE      ");
            Console.WriteLine("  WW   WW    WW    EEEEE  LL    CC      OO     OO  MM MM   MM MM  EEEEE   ");
            Console.WriteLine("   WW WW WW WW     EE     LL    CCC      OO   OO   MM  MM MM  MM  EE      ");
            Console.WriteLine("     WW   WW       EEEEE  LLLLL   CCCC    OOOOO    MM   MMM   MM  EEEEE   ");
            Console.WriteLine("                                                                          ");
            Console.WriteLine("                                  to                                      ");
            Console.WriteLine("                                                                          ");
            Console.WriteLine("            RRRRR     OOOOO    UU     UU   GGGGGG   YY    YY              ");
            Console.WriteLine("            RR RR    OO   OO   UU     UU  GGG        YY  YY               ");
            Console.WriteLine("            RRRRR   OO     OO  UU     UU  GG   GGG    YYYY                ");
            Console.WriteLine("            RR  RR   OO   OO    UU   UU   GGG   GG     YY                 ");
            Console.WriteLine("            RR   RR   OOOOO      UUUUU      GGGGGG     YY                 ");
            Console.WriteLine("                                                                          ");
            Console.WriteLine("                                                     Press \"H\" for Help)");
            PressH();
            Console.Clear();
        }
        static void PlayerKilledEnemy(char[,] map, char enemy, ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.A && map[PlayerX - 1, PlayerY] == enemy)
            {
                enemyDead = true;
                Console.SetCursorPosition(OrcOneX, OrcOneY);
                MapForest[OrcOneX, OrcOneY] = BlankSpace;
                Console.Write(BlankSpace);
                HealthCounter--;
            }
            else if (key.Key == ConsoleKey.S && map[PlayerX, PlayerY + 1] == enemy)
            {
                enemyDead = true;
                Console.SetCursorPosition(OrcOneX, OrcOneY);
                MapForest[OrcOneX, OrcOneY] = BlankSpace;
                Console.Write(BlankSpace);
                HealthCounter--;
            }
            else if (key.Key == ConsoleKey.D && map[PlayerX + 1, PlayerY] == enemy)
            {
                enemyDead = true;
                Console.SetCursorPosition(OrcOneX, OrcOneY);
                MapForest[OrcOneX, OrcOneY] = BlankSpace;
                Console.Write(BlankSpace);
                HealthCounter--;
            }
            else if (key.Key == ConsoleKey.W && map[PlayerX, PlayerY - 1] == enemy)
            {
                enemyDead = true;
                Console.SetCursorPosition(OrcOneX, OrcOneY);
                MapForest[OrcOneX, OrcOneY] = BlankSpace;
                Console.Write(BlankSpace);
                HealthCounter--;
            }
        }

        static void DeadOrc(char[,] map, char enemy, ConsoleKeyInfo key)
        {
            if (OrcOneX < PlayerX && MapForest[OrcOneX + 1, OrcOneY] == Player && key.Key != ConsoleKey.D) //&& OrcOneX - PlayerX < 10)
            {
                enemyDead = true;
                Console.SetCursorPosition(OrcOneX, OrcOneY);
                MapForest[OrcOneX, OrcOneY] = BlankSpace;
                Console.Write(BlankSpace);
                HealthCounter--;
            }
            if (OrcOneX > PlayerX && MapForest[OrcOneX - 1, OrcOneY] == Player && key.Key != ConsoleKey.A) //&& OrcOneX - PlayerX > 10)
            {
                enemyDead = true;
                Console.SetCursorPosition(OrcOneX, OrcOneY);
                map[OrcOneX, OrcOneY] = BlankSpace;
                Console.Write(BlankSpace);
                HealthCounter--;
            }
            if (OrcOneY > PlayerY && MapForest[OrcOneX, OrcOneY - 1] == Player && key.Key != ConsoleKey.W) //&& OrcOneY - PlayerY > 10)
            {
                enemyDead = true;
                Console.SetCursorPosition(OrcOneX, OrcOneY);
                map[OrcOneX, OrcOneY] = BlankSpace;
                Console.Write(BlankSpace);
                HealthCounter--;
            }
            if (OrcOneX < PlayerX && MapForest[OrcOneX, OrcOneY + 1] == Player && key.Key != ConsoleKey.S) //&& OrcOneY - PlayerY < 10)
            {
                enemyDead = true;
                Console.SetCursorPosition(OrcOneX, OrcOneY);
                map[OrcOneX, OrcOneY] = BlankSpace;
                Console.Write(BlankSpace);
                HealthCounter--;
            }

        }
    }
}
