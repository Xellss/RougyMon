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
        public enum Types { Grass, ClosedDoor, OpenDoorTop, OpenDoorLeft, Tree, WallTop, WallSide, WallTopRight, WallButtomRight, Moor, DarkMoor, Water, Sand, BlackBackground, GraveyardWall };
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
