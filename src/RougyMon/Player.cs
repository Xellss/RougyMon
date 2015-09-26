using System;
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
        public MoveWithInput moveWithInput;
        BoxCollider collider;
        Map map;

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


            EventManager.OnUpdate += OnUpdate;
        }

        void OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject.Tag == "Key")
                HasKey = true;
        }
        void OnUpdate(GameTime gameTime)
        {
            RectangleF newRectangle = new RectangleF(moveWithInput.NextPosition.X / map.TileWidth - 0.25f, moveWithInput.NextPosition.Y / map.TileHeight - 0.25f, 0.5f, 0.25f);
            moveWithInput.NextFiledIsPassable = CanMoveTo(newRectangle);

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
            Tile nextTile = map.GetTile(transform.Position/32);
            if (nextTile == null)
                return;


            if (nextTile.Type == Tile.Types.Moor)
                moveWithInput.Speed = 1;
            else if (nextTile.Type == Tile.Types.DarkMoor)
                moveWithInput.Speed = 1;
            else if (nextTile.Type == Tile.Types.Sand)
                moveWithInput.Speed = 7;
            else
            moveWithInput.Speed = 5;
        }

        public override void Destroy()
        {
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }
    }
}
