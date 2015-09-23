﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MiniEngine;

namespace RougyMon
{
    class Player : GameObject
    {
        public Transform transform;
        Renderer renderer;
        MoveWithInput moveWithInput;
        BoxCollider collider;
        Map map;
        public NewTimer Timer;

        public bool HasKey = false;

        public Player(Vector2 position, Map map)
        {
            this.map = map;

            Tag = "Player";

            transform = AddComponent<Transform>();
            transform.Position = position;

            renderer = AddComponent<Renderer>();
            renderer.SetImage(Managers.Content.Load<Texture2D>("Sprites/Brunhilde/Brunhilde_Down_0"));
            renderer.Pivot = new Vector2(renderer.ImageWidth / 2, renderer.ImageHeight / 1f);

            moveWithInput = AddComponent<MoveWithInput>();
            moveWithInput.Speed = 5;

            collider = AddComponent<BoxCollider>();
            collider.OnCollisionEnter += OnCollisionEnter;

            Timer = AddComponent<NewTimer>();
            Timer.Time = TimeSpan.FromSeconds(120);

            EventManager.OnUpdate += OnUpdate;
        }


        void OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject.Tag == "Key")
                HasKey = true;
        }
        void OnUpdate(GameTime gameTime)
        {
            //CheckCurrentTile();
            //CheckNextTile();
            RectangleF newRectangle = new RectangleF(transform.Position.X, transform.Position.Y + 0.5f, 0.5f, 0.5f);
            moveWithInput.NextFiledIsPassable = CanMoveTo(newRectangle);
        }

        public bool CanMoveTo(RectangleF recCollider)
        {

            Vector2 v;
            for (v.X = recCollider.Location.X; v.X <= recCollider.Right; v.X += 0.25f)
            {
                for (v.Y = recCollider.Location.Y; v.Y <= recCollider.Bottom; v.Y += 0.25f)
                {
                    if (!map.IsPassable(v))
                    {
                        return false;
                    }
                }
            }
            return true;
        }














        //private void CheckCurrentTile()
        //{
        //    //moveWithInput.NextFiledIsPassable = true;
        //    int x = (int)transform.Position.X / map.TileHeight;
        //    int y = (int)transform.Position.Y / map.TileWidth;
        //    //Console.WriteLine("Current Tile:{0} Position:{1}", map.tiles[x, y].Type.ToString(), transform.Position / 32);
        //    //if (map.tiles[x, y].Type == Tile.Types.Moor)
        //    //moveWithInput.Speed = 3;
        //    //else
        //    //moveWithInput.Speed = 5;
        //}
        //private void CheckNextTile()
        //{
        //    int x = (int)moveWithInput.NextPosition.X / map.TileHeight;
        //    int y = (int)moveWithInput.NextPosition.Y / map.TileWidth;
        //    //Console.WriteLine("Next Tile:{0} Position:{1}", map.tiles[x, y].Type.ToString(), moveWithInput.NextPosition / 32);
        //    //if (map.tiles[x, y].Type == Tile.Types.Tree)
        //    //moveWithInput.NextFiledIsPassable = false;
        //}

        public override void Destroy()
        {
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }
    }
}
