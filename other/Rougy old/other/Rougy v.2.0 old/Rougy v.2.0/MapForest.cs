using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    static class MapForest
    {
        public const int MaxWidth = 40;
        public const int MaxHeight = 20;
        public const string Name = "Forest";
        static bool goldSpawn = true;
        public static bool keySpawn = true;
        static bool orkSpawn = true;

        public static MapField[,] forestFields = new MapField[MaxWidth, MaxHeight];
        static MapForest()
        {
            Map.Level = 2;
            InitFields();

            CreateMap();

        }
        
        public static void DrawMap()
        {
            Console.Clear();
            

            for (int y = 0; y < MaxHeight ; y++)
            {
                for (int x = 0; x < MaxWidth; x++)
                {
                    Objects mapObject = GetMapField(new Vector2i(x, y)).Content;
                    if (mapObject != null)
                        Console.Write(mapObject.Icon);
                    else
                        Console.Write(' ');
                }
                Console.WriteLine();
            }

        }
        public static MapField GetMapField(Vector2i position)
        {
            return forestFields[position.X, position.Y];
        }
        private static void CreateMap()
        {
            for (int y = 0; y < MaxHeight; y++)
                for (int x = 0; x < MaxWidth; x++)
                    forestFields[x, y].Content = new ForestWall(new Vector2i(x, y)); ;

            for (int y = 1; y < MaxHeight - 1; y++)
                for (int x = 1; x < MaxWidth - 1; x++)
                    forestFields[x, y].Content = new BlankSpace(new Vector2i(x, y));
            
            forestFields[MaxWidth - 1, 3].Content = new PortalToCity(new Vector2i(MaxWidth - 1, 3));

        }
        public static void CreateAfterPort()
        {
            for (int y = 1; y < MaxHeight - 1; y++)
                for (int x = 1; x < MaxWidth - 1; x++)
                {
                    if (forestFields[x, y].Content is Player ||forestFields[x,y].Content is Trees)
                    {
                        forestFields[x, y].Content = new BlankSpace(new Vector2i(x, y));
                    }
                }
            
            if (goldSpawn == true)
            {
                Gold.InitGold(forestFields);
                goldSpawn = false;

            }
            if (orkSpawn == true)
            {
                Ork.OrkSpawn(forestFields);
                orkSpawn = false;                
            }
            Trees.DrawRandomTree(forestFields);
            Water.SpawnWater(forestFields);
            if (keySpawn == true)
            {
                Key.KeySpawn(forestFields);
                keySpawn = false;
            }
            




        }
        private static void InitFields()
        {
            for (int y = 0; y < MaxHeight; y++)
            {
                for (int x = 0; x < MaxWidth; x++)
                {
                    forestFields[x, y] = new MapField(new Vector2i(x, y));
                }
            }
        }
    }
}
