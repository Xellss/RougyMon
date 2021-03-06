﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MiniEngine;
using GameStateManagementSample;
using System.IO;

namespace RougyMon
{
    class Player : GameObject
    {
        public Transform transform;
        ViewRenderer renderer;
        public MoveWithInput moveWithInput;
        BoxCollider collider;
        Map map;
        DoorForest doorForest;
        GateGraveyard gateGraveyard;
        public SpriteAnimation Animation;
        //public Rectangle Source;
        private int moveSpeed = 3;
        public bool HasKey1 = false;
        public bool HasKey2 = false;
        public bool HasJewel = false;
        public static int GoldCounter = 0;


        public Player(Vector2 position, Map map, DoorForest doorForest, GateGraveyard gateGraveyard)
        {
            this.gateGraveyard = gateGraveyard;
            this.doorForest = doorForest;
            this.map = map;

            Tag = "Player";

            transform = AddComponent<Transform>();
            transform.Position = position;

            renderer = AddComponent<ViewRenderer>();
            renderer.SetImage(Managers.Content.Load<Texture2D>("Sprites/Sprite_Sheet/RougyMon"), 32, 32);
            //renderer.Pivot = new Vector2(renderer.ImageWidth / 2, renderer.ImageHeight / 1f);
            renderer.Pivot = new Vector2(16, 32);

            moveWithInput = AddComponent<MoveWithInput>();
            moveWithInput.Speed = 5;
            moveWithInput.MoveWithArrow = OptionsMenuScreen.MoveArrows;

            collider = AddComponent<BoxCollider>();
            collider.OnCollisionEnter += OnCollisionEnter;

            Animation = new SpriteAnimation(
                string.Empty,
                Managers.Content.Load<Texture2D>("Sprites/Sprite_Sheet/RougyMon"),
                Path.Combine(Managers.Content.RootDirectory, "Sprites", "Sprite_Sheet", "RougyMon.xml"));
            Animation.FrameDelay = 100;



            EventManager.OnUpdate += OnUpdate;
            EventManager.OnLateUpdate += OnLateUpdate;
        }

        void OnLateUpdate(GameTime gameTime)
        {
            Animation.PlayAnimation(string.Format("Brunhilde_{0}", moveWithInput.Direction));
            Animation.UpdateAnimation(gameTime);
            renderer.Source = Animation.CurrentFrame.Bounds;
        }

        void OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject.Tag == "Key_1")
                HasKey1 = true;
            if (other.GameObject.Tag == "Key_2")
                HasKey2 = true;
            if (other.GameObject.Tag == "Jewel")
                HasJewel = true;
            if (other.GameObject.Tag == "Gold")
                GoldCounter++;
            if (other.GameObject.Tag == "DoorForest")
                if (HasKey1)
                    other.GameObject.Destroy();
            //else
            //    transform.Position = new Vector2(transform.Position.X, transform.Position.Y + 10);

            if (other.GameObject.Tag == "GateGraveyard")
                if (HasKey2)
                    other.GameObject.Destroy();
            //else
            //    transform.Position = new Vector2(transform.Position.X, transform.Position.Y + 10);
        }
        void OnUpdate(GameTime gameTime)
        {
            renderer.Pivot = new Vector2(32, 32);

            RectangleF newRectangle = new RectangleF(moveWithInput.NextPosition.X / map.TileWidth - 0.75f, moveWithInput.NextPosition.Y / map.TileHeight - 0.25f, 0.5f, 0.25f);

            moveWithInput.NextFiledIsPassable = CanMoveTo(newRectangle);

            //if (newRectangle.Location.Y == doorForest.transform.Position.Y || newRectangle.Location.Y == gateGraveyard.transform.Position.Y)
            //{
            //    moveWithInput.NextFiledIsPassable = false;
            //}
            CheckCurrentTile();

            Console.WriteLine(transform.Position);
        }

        public bool CanMoveTo(RectangleF recCollider)
        {

            Vector2 v;
            float pos = doorForest.transform.Position.Y / map.TileHeight;
            float xpos = doorForest.transform.Position.X / map.TileHeight;
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

            if (gateGraveyard.Bounds.Intersects(recCollider) & !HasKey2) return false;
            if (doorForest.Bounds.Intersects(recCollider) & !HasKey1) return false;

            return true;
        }
        private void CheckCurrentTile()
        {
            Tile nextTile = map.GetTile(transform.Position / 32);
            if (nextTile == null)
                return;

            if (nextTile.Type == Tile.Types.Moor)
                moveWithInput.Speed = 1;
            else if (nextTile.Type == Tile.Types.Sand)
                moveWithInput.Speed = 5;
            else
                moveWithInput.Speed = moveSpeed;
        }

        public override void Destroy()
        {
            EventManager.OnLateUpdate -= OnLateUpdate;
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }
    }
}
