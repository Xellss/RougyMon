using System;
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

            Tag = "OrkForest";

            transform = AddComponent<Transform>();
            transform.Position = position;

<<<<<<< HEAD
            renderer = AddComponent<Renderer>();
            renderer.SetImage(Managers.Content.Load<Texture2D>("Sprites/SkeletonKing/SkeletonKing_Front_0"));
            //renderer.Pivot = new Vector2(renderer.ImageWidth / 2, renderer.ImageHeight / 1f);
=======
            renderer = AddComponent<ViewRenderer>();
            renderer.SetImage(Managers.Content.Load<Texture2D>("Sprites/Sprite_Sheet/RougyMon"), 32, 32);
            renderer.Pivot = new Vector2(renderer.ImageWidth / 2, renderer.ImageHeight / 1f);
>>>>>>> 48279a0d0417d94bf78eba7acd00cbc7eb081356

            patrol = AddComponent<Patrol>();
            patrol.PatrolToTarget(patrolTarget);
            moveSpeed = 3;


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
                if (Tag == "Jewel")
                {
                    Destroy();
                    ScreenManager.WinScreen = true;
                }
                timer.Time = timer.Time.Subtract(new TimeSpan(0, 0, 30));
                renderer.SetImage(Managers.Content.Load<Texture2D>("Sprites/Objects/Jewel"), 22, 32);
                transform.Position = new Vector2(transform.Position.X + 20, transform.Position.Y - 20);
                patrol.CanPatrol = false;
                Tag = "Jewel";
                //timer.Time.TotalSeconds -= 10f;
                //Destroy();
            }
        }
        void OnUpdate(GameTime gameTime)
        {
            renderer.Pivot = new Vector2(32, 32);

            RectangleF newRectangle = new RectangleF(patrol.NextPosition.X / map.TileWidth , patrol.NextPosition.Y / map.TileHeight - 1f, 1f, 1f);
            patrol.NextFiledIsPassable = CanMoveTo(newRectangle);

            CheckCurrentTile();
        }
        void OnLateUpdate(GameTime gameTime)
        {
            //Animation.PlayAnimation(string.Format("OrkGraveyard_{0}", moveWithInput.Direction));
            Animation.PlayAnimation(string.Format("SkeletonKing_{0}", patrol.Direction));

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
            else if (nextTile.Type == Tile.Types.DarkMoor)
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

