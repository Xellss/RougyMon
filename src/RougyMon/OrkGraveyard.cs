using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RougyMon
{
    class OrkGraveyard : GameObject
    {
        Transform transform;
        Renderer renderer;
        BoxCollider collider;
        Map map;
        Patrol patrol;
        NewTimer timer;


        public OrkGraveyard(Vector2 position, Map map, Vector2 patrolTarget)
        {
            this.map = map;

            Tag = "OrkGraveyard";

            transform = AddComponent<Transform>();
            transform.Position = position;

            renderer = AddComponent<Renderer>();
            renderer.SetImage(Managers.Content.Load<Texture2D>("Sprites/OrkGraveyard/OrkGraveyard_Back_0"));
            renderer.Pivot = new Vector2(renderer.ImageWidth / 2, renderer.ImageHeight / 1f);

            patrol = AddComponent<Patrol>();
            patrol.Speed = 1;
            patrol.PatrolToTarget(patrolTarget);


            collider = AddComponent<BoxCollider>();
            collider.OnCollisionEnter += OnCollisionEnter;


            EventManager.OnUpdate += OnUpdate;
        }

        void OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject.Tag == "Player")
            {
                //timer.Time.TotalSeconds -= 10f;
                Destroy();
            }
        }
        void OnUpdate(GameTime gameTime)
        {
            RectangleF newRectangle = new RectangleF(patrol.NextPosition.X / map.TileWidth - 0.25f, patrol.NextPosition.Y / map.TileHeight - 0.25f, 0.5f, 0.25f);
            patrol.NextFiledIsPassable = CanMoveTo(newRectangle);

            CheckCurrentTile();
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
                patrol.Speed = 5;
        }
        public override void Destroy()
        {
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }
    }
}
