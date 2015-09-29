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
        public enum Types { Grass, ClosedDoor, OpenDoorTop, OpenDoorLeft, Tree, WallTop, 
                            WallSide, WallTopRight, WallButtomRight, Moor, DarkMoor, Water, 
                            Sand, BlackBackground, GraveyardWall, House1, House2, House3, House4, 
                            House5, House6, House7, House8, House9, House10, House11, House12,
                            Fence_Horizontal, Fence_Vertical, Fence_LL, Fence_LR, Fence_UL, Fence_UR};
        public Types Type { get; set; }

        public bool IsPassable;

        public Tile(Types tileType)
        {
            Tag = tileType.ToString();

            Type = tileType;
            IsPassable = tileType == Types.Grass || tileType == Types.Moor || tileType == Types.Sand || tileType == Types.Moor || tileType == Types.DarkMoor || tileType == Types.OpenDoorLeft || tileType == Types.OpenDoorTop;

        }
    }
}
