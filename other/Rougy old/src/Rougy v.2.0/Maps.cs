using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    class Maps
    {
        public enum mapType
        {
            CITY,          // 0
            FOREST,        // 1
            GRAVEYARD      // 2
        };


        public enum objects
        {
            PLAYER,                  // nr all maps
            ENEMYORK1,               // r forest
            ENEMYORK2,               // r forest
            ENEMYSKELETON1,          // r graveyard
            ENEMYSKELETON2,          // r graveyard
            ENEMYSKELETON3,          // r graveyard
            ENEMYSKELETONKING,       // nr graveyard
            CITYWALL,                // nr city
            GRAVEYARDWALL,           // nr graveyard
            TREE,                    // r forest
            FORESTWALL,              // nr forest
            GRAVESTONE,              // nr graveyard
            WATER,                   // nr forest
            HOUSEWALL,               // nr city
            HOUSEROOF,               // nr city
            KEY,                     // nr forest
            GOLD,                    // r all maps
            PORTALTOCITY,            // r forest, graveyard
            PORTALTOFOREST,          // r city
            PORTALTOGRAVEYARDOPEN,   // r city 
            PORTALTOGRAVEYARDCLOSED, // r city  
            EMPTY,
            TRIFORCE,                // r
            CANTWALK
        };

        public int maxHeight;
        public int maxWidth;
        public mapType type;
        public objects[,] map;
        private objects[] walls = new objects[3];
        //public int enemyCounter;

        // width & heigt > 5
        public Maps(int width, int height, int type, Player player)
        {
            this.maxHeight = width;
            this.maxWidth = height;
            this.type = (mapType)type;
            walls[0] = objects.CITYWALL;
            walls[1] = objects.FORESTWALL;
            walls[2] = objects.GRAVEYARDWALL;
            map = new objects[maxWidth, maxHeight];
            //enemyCounter = 0;
            this.init(player);
        }
        public void init(Player player)
        {
            Random rnd = new Random();
            bool keySpawned = false;
            int orkCounter = 0;
            int skeletonCounter = 0;
            bool skeletonKingSpawned = false;

            // fill board
            for (int i = 0; i < maxWidth; i++)
            {
                for (int j = 0; j < maxHeight; j++)
                {
                    int seed = rnd.Next(1, 100);
                    // fill empty
                    map[i, j] = objects.EMPTY;
                    // fill walls
                    if (i == 0 || j == maxHeight - 1 || j == 0 || i == maxWidth - 1)
                        map[i, j] = walls[(int)this.type];
                    else if (this.type == mapType.FOREST)
                    {

                        // fill gold
                        if (seed < 2 && map[i, j] == objects.EMPTY)
                            map[i, j] = objects.GOLD;
                        // fill tree
                        if (seed < 15 && map[i, j] == objects.EMPTY)
                            map[i, j] = objects.TREE;

                    }
                    else if (this.type == mapType.CITY)
                    {
                        if (seed < 2 && map[i, j] == objects.EMPTY)
                            map[i, j] = objects.GOLD;
                    }
                    else if (this.type == mapType.GRAVEYARD)
                    {
                        if (seed < 2 && map[i, j] == objects.EMPTY)
                            map[i, j] = objects.GOLD;
                    }
                }
            }

            // set player start position
            if (this.type == mapType.FOREST)
            {
                map[3, 38] = objects.PLAYER;
                // fill water
                for (int k = 1; k < 7; k++)
                    for (int l = 1; l < 7 - k - 1; l++)
                        map[k, l] = objects.WATER;
            }
            if (this.type == mapType.CITY)
            {
                // fill HouseWall
                for (int k = 6; k < 12; k++)
                    for (int l = 8; l < 11; l++)
                        map[l, k] = objects.HOUSEWALL;
                map[10, 10] = objects.PLAYER;
                // fill HouseRoof
                for (int k = 7; k < 11; k++)
                    map[7, k] = objects.HOUSEROOF;

                map[6, 8] = objects.HOUSEROOF;
                map[6, 9] = objects.HOUSEROOF;
            }
            if (this.type == mapType.GRAVEYARD)
            {
                map[18, 3] = objects.PLAYER;
                // fill GraveStone
                for (int k = 3; k < 34; k += 5)
                    for (int l = 5; l < 16; l += 5)
                        map[l, k] = objects.GRAVESTONE;
            }
            // spawn key
            while (!keySpawned && this.type == mapType.FOREST)
            {
                int y = rnd.Next(1, maxHeight - 1);
                int x = rnd.Next(1, maxWidth - 1);
                if (map[x, y] == objects.EMPTY)
                {
                    map[x, y] = objects.KEY;
                    keySpawned = true;
                }
            }
            // spawn ork1
            while (orkCounter < 3 && this.type == mapType.FOREST)
            {
                int y = rnd.Next(1, maxHeight - 1);
                int x = rnd.Next(1, maxWidth - 1);
                if (map[x, y] == objects.EMPTY)
                {
                    Enemy ork1 = new Enemy(objects.ENEMYORK1, this, (mapType)1);
                    //Enemy ork2 = new Enemy(objects.ENEMYORK2, this, (mapType)1);
                    //enemyCounter++;
                    orkCounter++;
                }
            }
            // spawn skeleton
            while (skeletonCounter < 3 && this.type == mapType.GRAVEYARD)
            {
                int y = rnd.Next(1, maxHeight - 1);
                int x = rnd.Next(1, maxWidth - 1);
                if (map[x, y] == objects.EMPTY)
                {
                    //map[x, y] = objects.ENEMYSKELETON1;
                    //map[x, y] = objects.ENEMYSKELETON2;
                    //map[x, y] = objects.ENEMYSKELETON2;
                    skeletonCounter++;
                }
            }
            //spawn skeleton king
            while (!skeletonKingSpawned && this.type == mapType.GRAVEYARD)
            {
                int y = rnd.Next(1, maxHeight - 1);
                int x = rnd.Next(1, maxWidth - 1);
                if (map[x, y] == objects.EMPTY)
                {
                    map[x, y] = objects.ENEMYSKELETONKING;
                    skeletonKingSpawned = true;
                }
            }
            refresh(player);

        }
        // updates character movement
        public objects update(int x1, int y1, int x2, int y2, int type)
        {
            if (map[x2, y2] == objects.EMPTY)
            {
                map[x1, y1] = objects.EMPTY;
                map[x2, y2] = (objects)type;
                return objects.EMPTY;
            }
            if (map[x2, y2] == objects.ENEMYORK1)
            {
                map[x1, y1] = objects.EMPTY;
                if (map[x2, y2] == Maps.objects.PLAYER)
                {
                    map[x2, y2] = (objects)type;

                }
                return objects.ENEMYORK1;
            }
            if (map[x2, y2] == objects.ENEMYORK2)
            {
                map[x1, y1] = objects.EMPTY;
                if (map[x2,y2] == Maps.objects.PLAYER)
                {
                    map[x2, y2] = (objects)type;
                    
                }
                return objects.ENEMYORK2;
            } if (map[x2, y2] == objects.ENEMYSKELETON1)
            {
                map[x1, y1] = objects.EMPTY;
                if (map[x2, y2] == Maps.objects.PLAYER)
                {
                    map[x2, y2] = (objects)type;

                }
                return objects.ENEMYSKELETON1;
            } if (map[x2, y2] == objects.ENEMYSKELETON2)
            {
                map[x1, y1] = objects.EMPTY;
                if (map[x2, y2] == Maps.objects.PLAYER)
                {
                    map[x2, y2] = (objects)type;

                }
                return objects.ENEMYSKELETON2;
            } if (map[x2, y2] == objects.ENEMYSKELETON3)
            {
                map[x1, y1] = objects.EMPTY;
                if (map[x2, y2] == Maps.objects.PLAYER)
                {
                    map[x2, y2] = (objects)type;

                }
                return objects.ENEMYSKELETON3;
            }
            
             if (map[x2, y2] == objects.ENEMYSKELETONKING)
            {
                //map[x1, y1] = (objects)type;
                map[x2, y2] = objects.TRIFORCE;

                return objects.ENEMYSKELETONKING;
            }
            else if (map[x2, y2] == objects.PLAYER)
            {
                map[x1, y1] = objects.EMPTY;
                //map[x2, y2] = objects.EMPTY; 


                return objects.PLAYER;
            }
            else if (map[x2, y2] == objects.GOLD)
            {
                map[x1, y1] = objects.EMPTY;
                map[x2, y2] = (objects)type;

                return objects.GOLD;
            }
            else if (map[x2, y2] == objects.KEY)
            {
                map[x1, y1] = objects.EMPTY;
                map[x2, y2] = (objects)type;

                return objects.KEY;
            }
            else if (map[x2, y2] == objects.TRIFORCE)
            {
                map[x1, y1] = objects.EMPTY;
                map[x2, y2] = (objects)type;

                return objects.TRIFORCE;
            }
            else if (map[x2, y2] == objects.PORTALTOCITY)
            {
                map[x1, y1] = objects.EMPTY;
                map[x2, y2] = (objects)type;

                return objects.PORTALTOCITY;
            }
            else if (map[x2, y2] == objects.PORTALTOFOREST)
            {
                map[x1, y1] = objects.EMPTY;
                map[x2, y2] = (objects)type;

                return objects.PORTALTOFOREST;
            }
            else if (map[x2, y2] == objects.PORTALTOGRAVEYARDOPEN)
            {
                map[x1, y1] = objects.EMPTY;
                map[x2, y2] = (objects)type;

                return objects.PORTALTOGRAVEYARDOPEN;
            }
            return objects.CANTWALK;
        }

        public void refresh(Player player)
        {
            // city portals
            if (this.type == mapType.CITY)
            {
                map[3, 0] = objects.PORTALTOFOREST;
                if (player.hasKey == true)
                    map[0, 10] = objects.PORTALTOGRAVEYARDOPEN;
                else
                    map[0, 10] = objects.PORTALTOGRAVEYARDCLOSED;
            }
            // forest portal
            if (this.type == mapType.FOREST)
                map[3, 39] = objects.PORTALTOCITY;
            // graveyard portal
            if (this.type == mapType.GRAVEYARD)
                map[19, 3] = objects.PORTALTOCITY;
        }

    } //END OF CLASS
} //END OF NAMESPACE
