using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rougy_v._2._0
{
    static class MapCity
    {

        public const int MaxWidth = 40;
        public const int MaxHeight = 20;
        public const string Name = "City";


        public static MapField[,] cityFields = new MapField[MaxWidth, MaxHeight];
        static MapCity()
        {
            Map.Level = 1;
            InitFields();
            CreateMap();
        }
        public static void DrawMap()
        {
            Console.Clear();

            for (int y = 0; y < MaxHeight; y++)
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
            return cityFields[position.X, position.Y];
        }
        public static void CreateMap()
        {
            for (int y = 0; y < MaxHeight; y++)
                for (int x = 0; x < MaxWidth; x++)
                {
                    cityFields[x, y].Content = new CityWall(new Vector2i(x, y)); ;
                }
            for (int y = 1; y < MaxHeight - 1; y++)
                for (int x = 1; x < MaxWidth - 1; x++)
                {
                    cityFields[x, y].Content = new BlankSpace(new Vector2i(x, y));

                }
            cityFields[0, 3].Content = new PortalToForest(new Vector2i(0, 3));
            cityFields[10, 0].Content = new PortalToGraveyard(new Vector2i(10, 0));
            Gold.InitGold(cityFields);

        }
        public static void CreateAfterPort()
        {
            for (int y = 1; y < MaxHeight - 1; y++)
                for (int x = 1; x < MaxWidth - 1; x++)
                {
                    if (cityFields[x, y].Content is Player)
                    {
                        cityFields[x, y].Content = new BlankSpace(new Vector2i(x, y));
                    }
                }
            

        }

        public static void InitFields()
        {
            for (int y = 0; y < MaxHeight; y++)
            {
                for (int x = 0; x < MaxWidth; x++)
                {
                    cityFields[x, y] = new MapField(new Vector2i(x, y));
                }
            }
        }

    }
}
