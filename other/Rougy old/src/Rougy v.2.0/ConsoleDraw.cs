using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    static class ConsoleDraw
    {
        
        public static void draw(Maps board, Player player)
        {
            Console.OutputEncoding = System.Text.Encoding.GetEncoding(1252);
            int hudPositionX = 45;
            int hudPositionY = 0;
            for (int i = 0; i < board.maxWidth; i++)
            {
                for (int j = 0; j < board.maxHeight; j++)
                {
                    if (board.map[i, j] == Maps.objects.CITYWALL)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write('#');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.EMPTY)
                    {
                        Console.Write(' ');
                    }
                    if (board.map[i, j] == Maps.objects.ENEMYORK1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write((char)190);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.ENEMYORK2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write((char)190);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.ENEMYSKELETON1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write((char)42);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.ENEMYSKELETON2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write((char)42);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.ENEMYSKELETON3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write((char)42);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.ENEMYSKELETONKING)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write((char)15);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.FORESTWALL)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('B');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.GOLD)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write('$');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.GRAVESTONE)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write((char)197);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.GRAVEYARDWALL)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write((char)204);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.HOUSEROOF)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write('o');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.TRIFORCE)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write((char)127);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.HOUSEWALL)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write('H');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.KEY)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write((char)12);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.PLAYER)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write((char)2);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.PORTALTOCITY)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write('O');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.PORTALTOFOREST)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write('O');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.PORTALTOGRAVEYARDCLOSED)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write('X');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.PORTALTOGRAVEYARDOPEN)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.Write('O');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.TREE)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write('B');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.WATER)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write((char)178);
                        Console.ResetColor();
                    }


                }
                Console.WriteLine();
            }
            //Console.WriteLine("\n" + );

            Console.SetCursorPosition(hudPositionX, hudPositionY);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write((char)175); Console.Write((char)175); Console.Write((char)175); Console.Write((char)175); Console.Write((char)175); Console.Write((char)175);
            Console.Write((char)174); Console.Write((char)174); Console.Write((char)174); Console.Write((char)174); Console.Write((char)174); Console.Write((char)174);
            Console.ResetColor();


            // Hud Hearts
            Console.SetCursorPosition(hudPositionX, ++hudPositionY);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write((char)178 + " ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write((char)3);
            Console.Write((char)3);
            Console.Write((char)3);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(": " + player.healthCounter);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(hudPositionX + 11, hudPositionY);
            Console.Write((char)178);

            // Hud Gold
            Console.SetCursorPosition(hudPositionX, ++hudPositionY);
            Console.Write((char)178 + " ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("$$$");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(": " + player.goldCounter);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(hudPositionX + 11, hudPositionY);
            Console.Write((char)178);

            Console.SetCursorPosition(hudPositionX, ++hudPositionY);
            Console.Write((char)178 + " ");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("KEY");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(": " + ((player.hasKey) ? "YES" : "NO"));
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(hudPositionX + 11, hudPositionY);
            Console.Write((char)178);

            Console.SetCursorPosition(hudPositionX, ++hudPositionY);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write((char)175); Console.Write((char)175); Console.Write((char)175); Console.Write((char)175); Console.Write((char)175); Console.Write((char)175);
            Console.Write((char)174); Console.Write((char)174); Console.Write((char)174); Console.Write((char)174); Console.Write((char)174); Console.Write((char)174);
            Console.ResetColor();

            Console.SetCursorPosition(hudPositionX, hudPositionY +=2);
            Console.WriteLine("(Press \"H\" for Help)");

            
        }
        public static void Welcome()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            int b = 5;
            Console.SetCursorPosition(16, ++b); Console.WriteLine("                              +-+-+-+-+-+   ");
            Console.SetCursorPosition(16, ++b); Console.WriteLine("                              |R|o|u|g|y|   ");
            Console.SetCursorPosition(16, ++b); Console.WriteLine("                              +-+-+-+-+-+   ");
            Console.SetCursorPosition(16, ++b); Console.WriteLine(" ██▀███   ▒█████   █    ██   ▄████▓██   ██▓ ");
            Console.SetCursorPosition(16, ++b); Console.WriteLine("▓██ ▒ ██▒▒██▒  ██▒ ██  ▓██▒ ██▒ ▀█▒▒██  ██▒ ");
            Console.SetCursorPosition(16, ++b); Console.WriteLine("▓██ ░▄█ ▒▒██░  ██▒▓██  ▒██░▒██░▄▄▄░ ▒██ ██░ ");
            Console.SetCursorPosition(16, ++b); Console.WriteLine("▒██▀▀█▄  ▒██   ██░▓▓█  ░██░░▓█  ██▓ ░ ▐██▓░ ");
            Console.SetCursorPosition(16, ++b); Console.WriteLine("░██▓ ▒██▒░ ████▓▒░▒▒█████▓ ░▒▓███▀▒ ░ ██▒▓░ ");
            Console.SetCursorPosition(16, ++b); Console.WriteLine("░ ▒▓ ░▒▓░░ ▒░▒░▒░ ░▒▓▒ ▒ ▒  ░▒   ▒   ██▒▒▒  ");
            Console.SetCursorPosition(16, ++b); Console.WriteLine("  ░▒ ░ ▒░  ░ ▒ ▒░ ░░▒░ ░ ░   ░   ░ ▓██ ░▒░  ");
            Console.SetCursorPosition(16, ++b); Console.WriteLine("  ░░   ░ ░ ░ ░ ▒   ░░░ ░ ░ ░ ░   ░ ▒ ▒ ░░   ");
            Console.SetCursorPosition(16, ++b); Console.WriteLine("   ░         ░ ░     ░           ░ ░ ░      ");

            Console.ReadKey(true);
            Console.Clear();
        }
        public static void Intro()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Hey you! Wake up! I just got mugged!!\n\n");
            Console.Write("You gonna help me, right? RIGHT?? ");
            Console.Write("Bye the way, whats ur name??\n\n ");

            string name = Convert.ToString(Console.ReadLine());

            Console.WriteLine("\nUhm.. " + name + " is a strange name.. I will just call u Link. ");
            Console.WriteLine("\nSo Link, please help me! Go out there and bring back my artefact!");
            Console.WriteLine();
            Console.ResetColor();
            Console.ReadKey(true);
            Console.Clear();
        }
        public static void victory(Maps board, Player player)
        {
            Console.SetCursorPosition(0, 0);
            int hudPositionX = 45;
            int hudPositionY = 0;
            for (int i = 0; i < board.maxWidth; i++)
            {
                for (int j = 0; j < board.maxHeight; j++)
                {
                    if (board.map[i, j] == Maps.objects.CITYWALL)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write('#');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.EMPTY)
                    {
                        Console.Write(' ');
                    }
                    if (board.map[i, j] == Maps.objects.ENEMYORK1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write((char)190);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.ENEMYORK2)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write((char)190);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.ENEMYSKELETON1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write((char)42);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.ENEMYSKELETON2)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write((char)42);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.ENEMYSKELETON3)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write((char)42);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.ENEMYSKELETONKING)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write((char)15);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.FORESTWALL)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write('B');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.GOLD)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write('$');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.GRAVESTONE)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write((char)197);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.GRAVEYARDWALL)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write((char)204);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.HOUSEROOF)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write('o');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.TRIFORCE)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write((char)127);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.HOUSEWALL)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write('H');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.KEY)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write((char)12);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.PLAYER)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write((char)2);
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.PORTALTOCITY)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write('O');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.PORTALTOFOREST)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write('O');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.PORTALTOGRAVEYARDCLOSED)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write('X');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.PORTALTOGRAVEYARDOPEN)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write('O');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.TREE)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write('B');
                        Console.ResetColor();
                    }
                    if (board.map[i, j] == Maps.objects.WATER)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write((char)178);
                        Console.ResetColor();
                    }

                }
                Console.WriteLine();
            }

            Console.SetCursorPosition(hudPositionX, hudPositionY);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write((char)175); Console.Write((char)175); Console.Write((char)175); Console.Write((char)175); Console.Write((char)175); Console.Write((char)175);
            Console.Write((char)174); Console.Write((char)174); Console.Write((char)174); Console.Write((char)174); Console.Write((char)174); Console.Write((char)174);

            // Hud Hearts
            Console.SetCursorPosition(hudPositionX, ++hudPositionY);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write((char)178 + " ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write((char)3);
            Console.Write((char)3);
            Console.Write((char)3);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(": " + player.healthCounter);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.SetCursorPosition(hudPositionX + 11, hudPositionY);
            Console.Write((char)178);

            // Hud Gold
            Console.SetCursorPosition(hudPositionX, ++hudPositionY);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write((char)178 + " ");
            Console.Write("$$$");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(": " + player.goldCounter);
            Console.SetCursorPosition(hudPositionX + 11, hudPositionY);
            Console.Write((char)178);

            Console.SetCursorPosition(hudPositionX, ++hudPositionY);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write((char)178 + " ");
            Console.Write("KEY");
            Console.Write(": " + ((player.hasKey) ? "YES" : "NO"));
            Console.SetCursorPosition(hudPositionX + 11, hudPositionY);
            Console.Write((char)178);

            Console.SetCursorPosition(hudPositionX, ++hudPositionY);
            Console.Write((char)175); Console.Write((char)175); Console.Write((char)175); Console.Write((char)175); Console.Write((char)175); Console.Write((char)175);
            Console.Write((char)174); Console.Write((char)174); Console.Write((char)174); Console.Write((char)174); Console.Write((char)174); Console.Write((char)174);
            Console.ResetColor();
            int b = 6;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(4, ++b); Console.WriteLine(" ▄█    █▄   ▄█   ▄████████     ███      ▄██████▄     ▄████████ ▄██   ▄  ");
            Console.SetCursorPosition(4, ++b); Console.WriteLine("███    ███ ███  ███    ███ ▀█████████▄ ███    ███   ███    ███ ███   ██▄ ");
            Console.SetCursorPosition(4, ++b); Console.WriteLine("███    ███ ███▌ ███    █▀     ▀███▀▀██ ███    ███   ███    ███ ███▄▄▄███ ");
            Console.SetCursorPosition(4, ++b); Console.WriteLine("███    ███ ███▌ ███            ███   ▀ ███    ███  ▄███▄▄▄▄██▀ ▀▀▀▀▀▀███ ");
            Console.SetCursorPosition(4, ++b); Console.WriteLine("███    ███ ███▌ ███            ███     ███    ███ ▀▀███▀▀▀▀▀   ▄██   ███ ");
            Console.SetCursorPosition(4, ++b); Console.WriteLine("███    ███ ███  ███    █▄      ███     ███    ███ ▀███████████ ███   ███ ");
            Console.SetCursorPosition(4, ++b); Console.WriteLine("███    ███ ███  ███    ███     ███     ███    ███   ███    ███ ███   ███ ");
            Console.SetCursorPosition(4, ++b); Console.WriteLine(" ▀██████▀  █▀   ████████▀     ▄████▀    ▀██████▀    ███    ███  ▀█████▀  ");
            Console.SetCursorPosition(4, ++b); Console.WriteLine("                                                    ███    ███           ");
            Console.ReadKey(true);

        }
        public static void gameover()
        {
            int b = 8;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(8, ++b); Console.WriteLine("  ▄▀  ██   █▀▄▀█ ▄███▄       ████▄     ▄   ▄███▄   █▄▄▄▄   ▄ ");
            Console.SetCursorPosition(8, ++b); Console.WriteLine("▄▀    █ █  █ █ █ █▀   ▀      █   █      █  █▀   ▀  █  ▄▀  █  ");
            Console.SetCursorPosition(8, ++b); Console.WriteLine("█ ▀▄  █▄▄█ █ ▄ █ ██▄▄        █   █ █     █ ██▄▄    █▀▀▌  █   ");
            Console.SetCursorPosition(8, ++b); Console.WriteLine("█   █ █  █ █   █ █▄   ▄▀     ▀████  █    █ █▄   ▄▀ █  █  █   ");
            Console.SetCursorPosition(8, ++b); Console.WriteLine(" ███     █    █  ▀███▀               █  █  ▀███▀     █      ");
            Console.SetCursorPosition(8, ++b); Console.WriteLine("        █    ▀                        █▐            ▀    ▀   ");
            Console.SetCursorPosition(8, ++b); Console.WriteLine("       ▀                              ▐                     ");
            Console.ReadKey(true);

        }
    }
}
