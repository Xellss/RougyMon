using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    static class MapGraveyard 
    {
        public const int MaxWidth = 40;
        public const int MaxHeight = 20;
        public const string Name = "City";
        static bool goldSpawn = true;
        static bool skeletonSpawn = true;
        static bool skeletonKingSpawn = true;
        public static MapField[,] graveyardFields = new MapField[MaxWidth, MaxHeight];
        static MapGraveyard()
        {
            Map.Level = 3;
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
            return graveyardFields[position.X, position.Y];
        }
        private static void CreateMap()
        {
            for (int y = 0; y < MaxHeight; y++)
                for (int x = 0; x < MaxWidth; x++)
                    graveyardFields[x, y].Content = new GraveyardWall(new Vector2i(x, y)); ;

            for (int y = 1; y < MaxHeight - 1; y++)
                for (int x = 1; x < MaxWidth - 1; x++)
                    graveyardFields[x, y].Content = new BlankSpace(new Vector2i(x, y));

            graveyardFields[10, MaxHeight-1].Content = new PortalToCity(new Vector2i(10, MaxHeight-1));
        }
        public static void CreateAfterPort()
        {
            for (int y = 1; y < MaxHeight - 1; y++)
                for (int x = 1; x < MaxWidth - 1; x++)
                {
                    if (graveyardFields[x, y].Content is Player)
                    {
                        graveyardFields[x, y].Content = new BlankSpace(new Vector2i(x, y));
                    }
                }
            if (goldSpawn == true)
            {
                Gold.InitGold(graveyardFields);
                goldSpawn = false;

            }
            if (skeletonSpawn == true)
            {
                Skeleton.skeletonSpawn(graveyardFields);
                skeletonSpawn = false;
            }
            if (skeletonKingSpawn == true)
            {
                SkeletonKing.skeletonKingSpawn(graveyardFields);
                skeletonKingSpawn = false;
            }
            Gravestone.SpawnGravestone(graveyardFields);
            
        }
        private static void InitFields()
        {
            for (int y = 0; y < MaxHeight; y++)
            {
                for (int x = 0; x < MaxWidth; x++)
                {
                    graveyardFields[x, y] = new MapField(new Vector2i(x, y));
                }
            }
        }
       
    }
}
