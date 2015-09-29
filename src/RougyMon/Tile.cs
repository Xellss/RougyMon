using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RougyMon
{
    class Tile : GameObject
    {
        public enum Types
        {
            Grass, Fence_UR, Fence_UL, Fence_LR, Tree, WallTop,
            WallSide, BlackBackground, Fence_Horizontal, Moor, Fence_LL, Water,
            Sand, Fence_Vertical, GraveyardWall, House1, House2, House3, House4, 
                            House5, House6, House7, House8, House9, House10, House11, House12};
        public Types Type { get; set; }

        public bool IsPassable;

        public Tile(Types tileType)
        {
            Tag = tileType.ToString();

            Type = tileType;
            IsPassable = tileType == Types.Grass || tileType == Types.Sand || tileType == Types.Moor ;

        }
    }
}
