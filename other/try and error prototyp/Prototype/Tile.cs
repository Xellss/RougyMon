using System;

namespace Prototype
{
    class Tile
    {
        public enum Types { Path, ClosedDoor, OpenDoorTop, OpenDoorLeft, Tree, WallTop, WallSide, WallTopRight, WallButtomRight, Moor, };
        public Types Type { get; set; }

        public bool IsPassable;

        public Tile(Types tileType)
        {
            Type = tileType;
            IsPassable = tileType == Types.Path || tileType == Types.Moor;
        }
    }
}
