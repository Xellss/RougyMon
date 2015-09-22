using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class Enemy
    {
        public Random rnd = new Random();
        public bool alive;
        public int x, y, i;
        public Maps.mapType type;
        public Maps currentMap;
        public Maps.objects enemy;
        public bool canSpawn;
        Player player = new Player();
        public Enemy(Maps.objects enemy, Maps currentMap, Maps.mapType type)
        {
            this.canSpawn = true;
            this.type = (Maps.mapType)type;
            this.enemy = enemy;
            this.currentMap = currentMap;
            this.alive = true;
            newSpawnPoint();
            this.i = 0;
        }
        public void newSpawnPoint()
        {
            if (canSpawn)
            {
                x = rnd.Next(2, 18);
                y = rnd.Next(2, 38);
                i = rnd.Next(1, 5);
            }
        }
        public int spawn(Maps currentMap)
        {
            newSpawnPoint();
            canSpawn = false;
            while (alive)
            {
            Maps.objects feedback;

            i = rnd.Next(1, 5);
            //if (player.x > x)
            //{
            //    if ((feedback = currentMap.update(x, y, x - 1, y, 1)) == Maps.objects.CANTWALK)//|| feedback == Maps.objects.PLAYER)
            //    {
            //        this.x -= 1;
            //        return adjustEnemy(feedback, currentMap);
            //    }
            //}
            //else if (player.x < x)
            //{
            //    if ((feedback = currentMap.update(x, y, x + 1, y, 1)) == Maps.objects.CANTWALK)//|| feedback == Maps.objects.PLAYER)
            //    {
            //        this.x += 1;
            //        return adjustEnemy(feedback, currentMap);
            //    }
            //}
            //else if (player.y > y)
            //{
            //    if ((feedback = currentMap.update(x, y, x, y - 1, 1)) == Maps.objects.CANTWALK)//|| feedback == Maps.objects.PLAYER)
            //    {
            //        this.y -= 1;
            //        return adjustEnemy(feedback, currentMap);
            //    }
            //}
            //else if (player.y < y)
            //{
            //    if ((feedback = currentMap.update(x, y, x, y + 1, 1)) == Maps.objects.CANTWALK)//|| feedback == Maps.objects.PLAYER)
            //    {
            //        this.y += 1;
            //        return adjustEnemy(feedback, currentMap);
            //    }
            //}

            switch (i)
            {
                case 1:
                    if ((feedback = currentMap.update(x, y, x - 1, y, (int)type)) == Maps.objects.EMPTY || feedback == Maps.objects.PLAYER)
                    {
                            this.x--;
                        return adjustEnemy(feedback, currentMap);
                    }
                    break;
                case 2:
                    if ((feedback = currentMap.update(x, y, x + 1, y, (int)type)) == Maps.objects.EMPTY || feedback == Maps.objects.PLAYER)
                    {
                            this.x++;
                        return adjustEnemy(feedback, currentMap);
                    }
                    break;
                case 3:
                    if ((feedback = currentMap.update(x, y, x, y - 1, (int)type)) == Maps.objects.EMPTY || feedback == Maps.objects.PLAYER)
                    {
                            this.y--;
                        return adjustEnemy(feedback, currentMap);
                    }

                    break;
                case 4:
                    if ((feedback = currentMap.update(x, y, x, y + 1, (int)type)) == Maps.objects.EMPTY || feedback == Maps.objects.PLAYER)
                    {
                            this.y++;
                        return adjustEnemy(feedback, currentMap);
                    }
                    break;

                default:
                    return 5;
            }
            return 5;
            }
            return 1100;
        }
        public int adjustEnemy(Maps.objects feedback, Maps currentMap)
        {

            if (feedback == Maps.objects.PLAYER)
            {
                alive = false; ;
            }

            return 4;
        }
    }
}
