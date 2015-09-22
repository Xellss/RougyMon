using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prototype
{
    class Map
    {
        public const int TileWidth = 32;
        public const int TileHeight = 32;

        public Color PureGreen = new Color(0, 255, 0);
        public Color PureBlue = new Color(0, 0, 255);

        public int MapWidth;
        public int MapHeight;

        private Tile[,] tiles;
        private Texture2D tileSet;

        public Map(Texture2D tileSet)
        {
            this.tileSet = tileSet;
        }

        public void LoadMapFromTextfile(string path, int mapWidth, int mapHeight)
        {
            InitMapSize(mapWidth, mapHeight);
            string cleanContent = GetCleanedStringFromFile(path);
            FillTileData(cleanContent);
        }

        public void LoadMapFromImage(Texture2D image)
        {
            InitMapSize(image.Width, image.Height);
            Color[] data = GetColorData(image);
        }

        public void RenderMap(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    Tile tile = tiles[x, y];
                    Vector2 position = new Vector2(x * TileWidth, y * TileHeight);
                    Rectangle sourceRect = new Rectangle((int)tile.Type * TileWidth, 0, TileWidth, TileHeight);
                    spriteBatch.Draw(tileSet, position, sourceRect, Color.White);
                }
            }
        }

        public Tile GetTile(Vector2 position)
        {
            if (position.X < 0 || position.Y < 0 || position.X > (MapWidth - 1) || position.Y > (MapHeight - 1))
                return null;

            return tiles[(int)position.X, (int)position.Y];
        }
        public Tile GetTile(int x, int y)
        {
            if (x < 0 || y < 0 || x > (MapWidth - 1) || y > (MapHeight - 1))
                return null;
            return tiles[x, y];
        }

        private void InitMapSize(int width, int height)
        {
            MapWidth = width;
            MapHeight = height;
            tiles = new Tile[width, height];
        }

        private string GetCleanedStringFromFile(string path)
        {
            StreamReader reader = new StreamReader(path);
            string fileContent = reader.ReadToEnd();
            reader.Close();

            fileContent = fileContent.Replace("\r\n", "");
            return fileContent;
        }

        private Color[] GetColorData(Texture2D image)
        {
            Color[] data = new Color[image.Width * image.Height];
            image.GetData<Color>(data);
            return data;
        }

        private void FillTileData(string data)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    int index = y * MapWidth + x;
                    char tileType = data[index];
                    tiles[x, y] = GetTileByType(tileType);
                }
            }
        }

        private Tile GetTileByType(char id)
        {
            int tileID = id - '0';
            return new Tile((Tile.Types)tileID);
        }

        public bool IsPassable(Vector2 positionV)
        {
            Tile tile = GetTile(positionV);
            if (tile == null)
            {
                return false;
            }
            else
                return tile.IsPassable;
        }
        public bool IsPassable(int x, int y)
        {
            Tile tile = GetTile(x, y);
            if (tile == null)
            {
                return false;
            }
            else
                return tile.IsPassable;
        }
    }
}
