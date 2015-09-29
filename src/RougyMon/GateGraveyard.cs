using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniEngine;

namespace RougyMon
{
    class GateGraveyard : GameObject
    {
        public Transform transform;
        public RectangleF Bounds;
        Renderer renderer;
        BoxCollider collider;

        public GateGraveyard(Vector2 position)
        {
            Tag = "GateGraveyard";

            transform = AddComponent<Transform>();
            transform.Position = position;

            renderer = AddComponent<Renderer>();
            renderer.SetImage(Managers.Content.Load<Texture2D>("Sprites/Objects/GateGraveyard"));
            //renderer.Pivot = new Vector2(renderer.ImageWidth / 2, renderer.ImageHeight / 2f);

            collider = AddComponent<BoxCollider>();

            EventManager.OnUpdate += OnUpdate;
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
