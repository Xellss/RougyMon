using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniEngine;

namespace RougyMon
{
    class DoorForest : GameObject
    {
        public Transform transform;
        Renderer renderer;
        public RectangleF Bounds;
        BoxCollider collider;

        public DoorForest(Vector2 position)
        {
            Tag = "DoorForest";

            transform = AddComponent<Transform>();
            transform.Position = position;

            renderer = AddComponent<Renderer>();
            renderer.SetImage(Managers.Content.Load<Texture2D>("Sprites/Objects/DoorForest"));
            //renderer.Pivot = new Vector2(renderer.ImageWidth / 2, renderer.ImageHeight / 2f);

            collider = AddComponent<BoxCollider>();
            collider.OnCollisionEnter += OnCollisionEnter;

            EventManager.OnUpdate += OnUpdate;
        }

        private void OnCollisionEnter(BoxCollider other)
        {
            //if (other.GameObject.Tag == "Player")
            //{
            //    if (player.HasKey1)
            //        Destroy();
            //    else
            //    {
            //        Vector2 oldPlayerPosition = player.transform.Position;
            //        player.transform.Position = new Vector2(oldPlayerPosition.X, oldPlayerPosition.Y + 3);
            //    }

            //}
        }

        private void OnUpdate(GameTime gameTime)
        {
            Bounds = new RectangleF(transform.Position.X / 32, transform.Position.Y / 32, 1, 1);
        }

        public override void Destroy()
        {
            EventManager.OnUpdate -= OnUpdate;

            base.Destroy();
        }
    }
}
