﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagement;
using RougyMon.UI.Screens;
using System.IO;

namespace RougyMon
{
    class SkeletonKing : GameObject
    {
        Transform transform;
        ViewRenderer renderer;
        BoxCollider collider;
        Map map;
        Patrol patrol;
        public int moveSpeed;
        NewTimer timer;
        public SpriteAnimation Animation;

        public SkeletonKing(Vector2 position, Map map, Vector2 patrolTarget, NewTimer timer)
        {
            this.map = map;
            this.timer = timer;

            Tag = "SkeletonKing";

            transform = AddComponent<Transform>();
            transform.Position = position;


            renderer = AddComponent<ViewRenderer>();
            renderer.SetImage(Managers.Content.Load<Texture2D>("Sprites/SkeletonKing/SkeletonKing_Front_0"),40,40);
            renderer.SetImage(Managers.Content.Load<Texture2D>("Sprites/Sprite_Sheet/RougyMon"), 40, 40);
            //renderer.Pivot = new Vector2(renderer.ImageWidth / 2, renderer.ImageHeight / 1f);
            renderer.Pivot = new Vector2(16, 32);

            patrol = AddComponent<Patrol>();
            patrol.PatrolToTarget(patrolTarget);
            moveSpeed = 1;


            


            Animation = new SpriteAnimation("SkeletonKing", Managers.Content.Load<Texture2D>("Sprites/Sprite_Sheet/RougyMon"), Path.Combine(Managers.Content.RootDirectory, "Sprites", "Sprite_Sheet", "RougyMon.xml"));
            Animation.FrameDelay = 100;

            collider = AddComponent<BoxCollider>();
            collider.OnCollisionEnter += OnCollisionEnter;

            EventManager.OnLateUpdate += OnLateUpdate;
            EventManager.OnUpdate += OnUpdate;
        }
        void OnLateUpdate(GameTime gameTime)
        {
            //Animation.PlayAnimation(string.Format("SkeletonKing_{0}", patrol.Direction));
            //Animation.UpdateAnimation(gameTime);
            //renderer.Source = Animation.CurrentFrame.Bounds;
        }
        void OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject.Tag == "Player")
            {
                if (Tag == "Jewel")
                {
                    Destroy();
                    ScreenManager.WinScreen = true;
                }
                timer.Time = timer.Time.Subtract(new TimeSpan(0, 0, 30));

                renderer.SetImage(Managers.Content.Load<Texture2D>("Sprites/Objects/Jewel"), 22, 32);
                transform.Position = new Vector2(transform.Position.X + 20, transform.Position.Y - 20);
                patrol.CanPatrol = false;
                renderer.Source = renderer.Image.Bounds;
                Tag = "Jewel";
            }
        }
        void OnUpdate(GameTime gameTime)
        {
            if (Tag == "SkeletonKing")
            {
                Animation.PlayAnimation(string.Format("SkeletonKing_{0}", patrol.Direction));
                Animation.UpdateAnimation(gameTime);
                renderer.Source = Animation.CurrentFrame.Bounds;
                renderer.Pivot = new Vector2(32, 32);

                //RectangleF newRectangle = new RectangleF(patrol.NextPosition.X / map.TileWidth - 0.75f,patrol.NextPosition.Y / map.TileHeight - 0.25f, 0.5f, 0.25f);

                Rectangle rectangle = Animation.CurrentFrame.Bounds;
                patrol.NextFiledIsPassable = CanMoveTo(rectangle);

                CheckCurrentTile();
            }
            

            
        }
        
        public bool CanMoveTo(Rectangle recCollider)
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

