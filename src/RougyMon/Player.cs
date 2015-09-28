using System;
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
        public SpriteAnimation Animation;
        //public Rectangle Source;

        public bool HasKey1 = false;
        public bool HasKey2 = false;
        public bool HasJewel = false;


        public Player(Vector2 position, Map map)
        {
            this.map = map;

            Tag = "Player";

            transform = AddComponent<Transform>();
            transform.Position = position;

            renderer = AddComponent<ViewRenderer>();
            renderer.SetImage(Managers.Content.Load<Texture2D>("Sprites/Sprite_Sheet/RougyMon"), 32, 32);
            renderer.Pivot = new Vector2(renderer.ImageWidth / 2, renderer.ImageHeight / 2f);

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
        }
        void OnUpdate(GameTime gameTime)
        {

            RectangleF newRectangle = new RectangleF(moveWithInput.NextPosition.X / map.TileWidth - 0.25f, moveWithInput.NextPosition.Y / map.TileHeight - 0.25f, 0.5f, 0.25f);
            moveWithInput.NextFiledIsPassable = CanMoveTo(newRectangle);

            CheckCurrentTile();
            //Console.WriteLine(transform.Position);
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
            EventManager.OnLateUpdate -= OnLateUpdate;
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }
    }
}
