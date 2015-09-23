//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework.Graphics;
//using System.IO;
//using Microsoft.Xna.Framework;

//namespace RougyMon
//{
//    class NewMap
//    {
//        List<Definition> definition = new List<Definition>();

//        public void ReadDefinition(string path)
//        {
//            StreamReader reader = new StreamReader(path);
//            while (!reader.EndOfStream)
//            {
//                string line = reader.ReadLine();
//                string[] firstPass = line.Split('=');
//                string name = firstPass[0];
//                string[] secondPass = firstPass[1].Split(';');

//                uint color = Convert.ToUInt32(secondPass[0], 16) + 0xff000000;
//                string tileName = secondPass[1];
//                bool isPassable = bool.Parse(secondPass[2]);
//                int moveSpeed = int.Parse(secondPass[3]);

//                definition.Add(new Definition(name, color, tileName, isPassable, moveSpeed));
//            }
//            reader.Close();
//        }

//        public void ReadTexture(Texture2D map)
//        {
//            Color[] data = GetColorData(map);
//            // 1D-Index -> 2D-Index (X,Y)
//            // x = i % Width
//            //  i = 5, Width = 2, x = 1
//            // y = i / Width % Höhe (Optional)
//            //  i = 5, Width = 2, y = 2
//            // data[i] Vergleich mit Color in definition-List
//            // data[i].PackedValue == definitionListItem.Color
//            // Map[width,height]
//            // Position = new Vector2(x, y) * TileSize
//            // Map[x,y] = new Tile(Position, Sprite, Passable, MoveSpeed)
//            for (int i = 0; i < data.Length; i++)
//            {
//                int x = i % map.Width;
//                int y = i / map.Width;

//                if (data[i].PackedValue == definition)
//                {
                    
//                }
//            }
//        }
//        private Color[] GetColorData(Texture2D image)
//        {
//            Color[] data = new Color[image.Width * image.Height];
//            image.GetData<Color>(data);
//            return data;
//        }

//    }
//    class Definition
//    {
//        public string Name { get; private set; }
//        public uint Color { get; private set; }
//        public string TileName { get; private set; }
//        public bool IsPassable { get; private set; }
//        public int MoveSpeed { get; private set; }
//        public Texture2D Sprite { get; private set; }

//        public Definition(string name, uint color, string tileName, bool isPassable, int moveSpeed)
//        {
//            Name = name;
//            Color = color;
//            TileName = tileName;
//            IsPassable = isPassable;
//            MoveSpeed = moveSpeed;
//            Sprite = Managers.Content.Load<Texture2D>("Tiles/" + tileName);
//        }
//    }
//}
