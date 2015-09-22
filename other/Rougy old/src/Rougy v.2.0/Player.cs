using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class Player
    {
        public int x, y;
        public int healthCounter;
        public int goldCounter;
        public bool hasKey;
        public bool hasTriforce;

        public Player()
        {
            this.x = 10;
            this.y = 10;
            this.healthCounter = 5;
            this.goldCounter = 0;
            this.hasKey = false;
            this.hasTriforce = false;
        }

        public int keyInput(Maps currentMap)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Maps.objects feedback;
            switch (keyInfo.Key)
            {
                case ConsoleKey.W:
                    if ((feedback = currentMap.update(x, y, x - 1, y, 0)) != Maps.objects.CANTWALK)
                    {
                        if (feedback != Maps.objects.ENEMYSKELETONKING)
                        {
                            this.x--;

                        }
                        return adjustPlayer(feedback, currentMap);
                    }
                    break;
                case ConsoleKey.S:
                    if ((feedback = currentMap.update(x, y, x + 1, y, 0)) != Maps.objects.CANTWALK)
                    {
                        if (feedback != Maps.objects.ENEMYSKELETONKING)
                        {
                            this.x++;

                        }
                        return adjustPlayer(feedback, currentMap);
                    }
                    break;
                case ConsoleKey.A:
                    if ((feedback = currentMap.update(x, y, x, y - 1, 0)) != Maps.objects.CANTWALK)
                    {
                        if (feedback != Maps.objects.ENEMYSKELETONKING)
                        {
                            this.y--;

                        }
                        return adjustPlayer(feedback, currentMap);
                    }
                    break;
                case ConsoleKey.D:
                    if ((feedback = currentMap.update(x, y, x, y + 1, 0)) != Maps.objects.CANTWALK)
                    {
                        if (feedback != Maps.objects.ENEMYSKELETONKING)
                        {
                            this.y++;

                        }

                        return adjustPlayer(feedback, currentMap);
                    }
                    break;

                case ConsoleKey.Escape:

                    Engine.running = false;

                    break;

                case ConsoleKey.H:
                    Console.Clear();
                    Console.WriteLine("                                                                           ");
                    Console.WriteLine("         Press \"W\" to Move Up                                            ");
                    Console.WriteLine("         Press \"S\" to Move Down                                          ");
                    Console.WriteLine("         Press \"A\" to Move Left                                          ");
                    Console.WriteLine("         Press \"D\" to Move Right                                         ");
                    Console.WriteLine("         Press \"B\" to Buy one Health for 10 Gold                         ");
                    Console.WriteLine("         Press \"O\" to insert your Cheat                                  ");
                    Console.WriteLine("         Press \"ESC\" to Exit the game                                    ");
                    Console.WriteLine("                                                                           ");
                    Console.WriteLine("         Rougy is created by Julian Hopp and Daniel Lause.                 ");
                    Console.ReadKey(true);
                    Console.Clear();

                    break;

                case ConsoleKey.B:
                    if (goldCounter >= 10)
                    {
                        healthCounter++;
                        goldCounter -= 10;
                    }

                    break;

                case ConsoleKey.O:
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
                    if (read == "key")
                    {
                        hasKey = true;
                    }
                    if (read == "gold")
                    {
                        goldCounter = 999;
                    }
                    if (read == "killurself")
                    {
                        healthCounter = 0;
                    }
                    if (read == "winwin")
                    {
                        hasTriforce = true;
                    }

                    break;

                default:
                    return 5;
            }
            return 5;
        }

        public int adjustPlayer(Maps.objects feedback, Maps currentMap)
        {
            if (feedback == Maps.objects.ENEMYORK1)
            {
                healthCounter--;
            }
            if (feedback == Maps.objects.ENEMYORK2)
            {
                healthCounter--;
            }
            if (feedback == Maps.objects.ENEMYSKELETON1)
            {
                healthCounter--;
            }
            if (feedback == Maps.objects.ENEMYSKELETON2)
            {
                healthCounter--;
            }
            if (feedback == Maps.objects.ENEMYSKELETON3)
            {
                healthCounter--;
            }
            if (feedback == Maps.objects.ENEMYSKELETONKING)
            {
                healthCounter -= 2;
            }
            if (feedback == Maps.objects.KEY)
            {
                hasKey = true;
            }
            if (feedback == Maps.objects.GOLD)
            {
                goldCounter++;
            }
            if (feedback == Maps.objects.PLAYER)
            {
                healthCounter--;
            }
            if (feedback == Maps.objects.TRIFORCE)
            {
                hasTriforce = true;
            }
            if (feedback == Maps.objects.PORTALTOCITY && currentMap.type == Maps.mapType.FOREST)
            {
                return 0;
            }
            if (feedback == Maps.objects.PORTALTOCITY && currentMap.type == Maps.mapType.GRAVEYARD)
            {
                return 1;
            }
            if (feedback == Maps.objects.PORTALTOFOREST)
            {
                return 2;
            }
            if (feedback == Maps.objects.PORTALTOGRAVEYARDOPEN)
            {
                return 3;
            }
            return 4;
        }
    }
}
