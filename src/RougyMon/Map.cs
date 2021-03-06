﻿using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RougyMon
{
    class Map
    {
        public int TileWidth = 32;
        public int TileHeight = 32;

        //passable tiles
        public Color Grass = new Color(255, 255, 255);
        public Color Moor = new Color(255, 106, 0);
        public Color DarkMoor = new Color(0, 0, 255);
        //public Color Path = new Color(0, 0, 255);
        public Color Sand = new Color(209, 255, 245);

        //not passable tiles
        public Color GraveyardWall = new Color(255, 0, 0);
        public Color Tree = new Color(0, 255, 33);
        public Color Water = new Color(0, 33, 255);
        public Color StoneWall = new Color(0, 0, 0);
        public Color BlackBackground = new Color(255, 76, 249);
        public Color House1 = new Color(0, 0, 1);
        public Color House2 = new Color(0, 0, 2);
        public Color House3 = new Color(0, 0, 3);
        public Color House4 = new Color(0, 0, 4);
        public Color House5 = new Color(0, 0, 5);
        public Color House6 = new Color(0, 0, 6);
        public Color House7 = new Color(0, 0, 7);
        public Color House8 = new Color(0, 0, 8);
        public Color House9 = new Color(0, 0, 9);
        public Color House10 = new Color(0, 0, 10);
        public Color House11 = new Color(0, 0, 11);
        public Color House12 = new Color(0, 0, 12);
        public Color Fence_Horizontal = new Color(254, 244, 0);
        public Color Fence_Vertical = new Color(254, 100, 0);
        public Color Fence_LL = new Color(110, 50, 155);
        public Color Fence_LR = new Color(110, 255, 155);
        public Color Fence_UL = new Color(127, 255, 246);
        public Color Fence_UR = new Color(145, 168, 101);

        //public Color Graveyard = new Color(0, 0, 255);

        public int MapWidth;
        public int MapHeight;

        public Tile[,] tiles;
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
            FillTileData(data);
        }

        public void RenderMap(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    Tile tile = tiles[x, y];
                    Vector2 position = new Vector2(x * TileWidth, y * TileHeight);
                    Rectangle rect = new Rectangle(x * TileWidth, y * TileHeight, TileWidth, TileHeight);
                    Rectangle sourceRect = new Rectangle((int)tile.Type * TileWidth + 1, 1, TileWidth - 2, TileHeight - 2);
                    spriteBatch.Draw(tileSet, rect, sourceRect, Color.White);
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
        private void FillTileData(Color[] data)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                for (int x = 0; x < MapWidth; x++)
                {
                    int index = y * MapWidth + x;
                    Color tileType = data[index];
                    tiles[x, y] = GetTileByType(tileType);
                }
            }
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
        private Tile GetTileByType(Color color)
        {
            if (color == Grass)
                return new Tile(Tile.Types.Grass);
            else if (color == Moor)
                return new Tile(Tile.Types.Moor);
            //else if (color == DarkMoor)
            //    return new Tile(Tile.Types.DarkMoor);
            else if (color == Sand)
                return new Tile(Tile.Types.Sand);

            else if (color == Tree)
                return new Tile(Tile.Types.Tree);
            else if (color == StoneWall)
                return new Tile(Tile.Types.WallSide);
            else if (color == Water)
                return new Tile(Tile.Types.Water);
            else if (color == BlackBackground)
                return new Tile(Tile.Types.BlackBackground);
            else if (color == GraveyardWall)
                return new Tile(Tile.Types.GraveyardWall);
            else if (color == House1)
                return new Tile(Tile.Types.House1);
            else if (color == House2)
                return new Tile(Tile.Types.House2);
            else if (color == House3)
                return new Tile(Tile.Types.House3);
            else if (color == House4)
                return new Tile(Tile.Types.House4);
            else if (color == House5)
                return new Tile(Tile.Types.House5);
            else if (color == House6)
                return new Tile(Tile.Types.House6);
            else if (color == House7)
                return new Tile(Tile.Types.House7);
            else if (color == House8)
                return new Tile(Tile.Types.House8);
            else if (color == House9)
                return new Tile(Tile.Types.House9);
            else if (color == House10)
                return new Tile(Tile.Types.House10);
            else if (color == House11)
                return new Tile(Tile.Types.House11);
            else if (color == House12)
                return new Tile(Tile.Types.House12);
            else if (color == Fence_Horizontal)
                return new Tile(Tile.Types.Fence_Horizontal);
            else if (color == Fence_Vertical)
                return new Tile(Tile.Types.Fence_Vertical);
            else if (color == Fence_LL)
                return new Tile(Tile.Types.Fence_LL);
            else if (color == Fence_LR)
                return new Tile(Tile.Types.Fence_LR);
            else if (color == Fence_UR)
                return new Tile(Tile.Types.Fence_UR);
            else if (color == Fence_UL)
                return new Tile(Tile.Types.Fence_UL);


            else
                return null;
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
