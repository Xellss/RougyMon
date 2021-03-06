﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace RougyMon
{
    class Skeleton : GameObject
    {
        Transform transform;
        ViewRenderer renderer;
        BoxCollider collider;
        Map map;
        Patrol patrol;
        public int moveSpeed;
        NewTimer timer;
        SpriteAnimation Animation;

        public Skeleton(Vector2 position, Map map, Vector2 patrolTarget, NewTimer timer)
        {
            this.map = map;
            this.timer = timer;
            Tag = "OrkForest";

            transform = AddComponent<Transform>();
            transform.Position = position;

            renderer = AddComponent<ViewRenderer>();
            renderer.SetImage(Managers.Content.Load<Texture2D>("Sprites/Sprite_Sheet/RougyMon"), 32, 32);
            renderer.Pivot = new Vector2(renderer.ImageWidth / 2, renderer.ImageHeight / 1f);

            patrol = AddComponent<Patrol>();
            patrol.PatrolToTarget(patrolTarget);
            moveSpeed = 1;

            collider = AddComponent<BoxCollider>();
            collider.OnCollisionEnter += OnCollisionEnter;

            Animation = new SpriteAnimation(
            string.Empty,
            Managers.Content.Load<Texture2D>("Sprites/Sprite_Sheet/RougyMon"),
            Path.Combine(Managers.Content.RootDirectory, "Sprites", "Sprite_Sheet", "RougyMon.xml"));
            Animation.FrameDelay = 100;

            EventManager.OnLateUpdate += OnLateUpdate;
            EventManager.OnUpdate += OnUpdate;
        }
        
        void OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject.Tag == "Player")
            {
                timer.Time = timer.Time.Subtract(new TimeSpan(0, 0, 20));

                Destroy();
            }
        }
        void OnUpdate(GameTime gameTime)
        {
            renderer.Pivot = new Vector2(32, 32);

            RectangleF newRectangle = new RectangleF(patrol.NextPosition.X / map.TileWidth - 0.75f, patrol.NextPosition.Y / map.TileHeight - 0.25f, 0.5f, 0.25f);
            patrol.NextFiledIsPassable = CanMoveTo(newRectangle);

            CheckCurrentTile();
        }
        void OnLateUpdate(GameTime gameTime)
        {
            Animation.PlayAnimation(string.Format("Skeleton_{0}", patrol.Direction));
            Animation.UpdateAnimation(gameTime);
            renderer.Source = Animation.CurrentFrame.Bounds;
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
        private void CheckCurrentTile()
        {
            Tile nextTile = map.GetTile(transform.Position / 32);
            if (nextTile == null)
                return;


            if (nextTile.Type == Tile.Types.Moor)
                patrol.Speed = 1;
            else if (nextTile.Type == Tile.Types.Sand)
                patrol.Speed = 7;
            else
                patrol.Speed = moveSpeed;
        }
        public override void Destroy()
        {
            EventManager.OnLateUpdate -= OnLateUpdate;
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }
    }
}

