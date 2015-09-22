using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class Engine
    {
        Maps city;
        Maps forest;
        Maps graveyard;
        Player player;
        Enemy ork1;
        Enemy ork2;
        Enemy skeleton1;
        Enemy skeleton2;
        Enemy skeleton3;

        public static bool running;
        int currentMap = 0;
        public Engine()
        {

            running = true;
            player = new Player();
            ork1 = new Enemy(Maps.objects.ENEMYORK1, forest, (Maps.mapType)2);
            ork2 = new Enemy(Maps.objects.ENEMYORK1, forest, (Maps.mapType)2);
            skeleton1 = new Enemy(Maps.objects.ENEMYSKELETON1, graveyard, (Maps.mapType)3);
            skeleton2 = new Enemy(Maps.objects.ENEMYSKELETON2, graveyard, (Maps.mapType)3);
            skeleton3 = new Enemy(Maps.objects.ENEMYSKELETON3, graveyard, (Maps.mapType)3);
            city = new Maps(40, 20, 0, player);
            forest = new Maps(40, 20, 1, player);
            graveyard = new Maps(40, 20, 2, player);
            currentMap = 0;

            run();
        }
        public void run()
        {
            ConsoleDraw.Welcome();
            ConsoleDraw.Intro();

            int tmp = 0;
            while (running)
            {

                Console.SetCursorPosition(0, 0);
                if (currentMap == 0)
                {
                    city.refresh(player);
                    ConsoleDraw.draw(city, player);
                    tmp = player.keyInput(city);
                }
                else if (currentMap == 1)
                {
                    forest.refresh(player);
                    ConsoleDraw.draw(forest, player);
                    tmp = player.keyInput(forest);
                    ork1.spawn(forest);
                    ork2.newSpawnPoint();
                    ork2.spawn(forest);

                }
                else if (currentMap == 2)
                {
                    graveyard.refresh(player);
                    ConsoleDraw.draw(graveyard, player);
                    tmp = player.keyInput(graveyard);
                    skeleton1.spawn(graveyard);
                    skeleton2.newSpawnPoint();
                    skeleton2.spawn(graveyard);
                    skeleton3.newSpawnPoint();
                    skeleton3.spawn(graveyard);

                }
                // from forest to city
                if (tmp == 0)
                {
                    currentMap = 0;
                    player.x = 3;
                    player.y = 1;
                }
                // from graveyard to city
                if (tmp == 1)
                {
                    currentMap = 0;
                    player.x = 1;
                    player.y = 10;
                }
                //from city to forest
                if (tmp == 2)
                {
                    currentMap = 1;
                    player.x = 3;
                    player.y = 38;
                }
                //from city to graveyard
                if (tmp == 3)
                {
                    currentMap = 2;
                    player.x = 18;
                    player.y = 3;
                }

                if (player.hasTriforce)
                {
                    running = false;
                    ConsoleDraw.victory(graveyard, player);
         
                }
                if (player.healthCounter < 1)
                {
                    running = false;
                    ConsoleDraw.gameover();
                 
                }
            }
        }
    }
}
