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
        Transform transform;
        Renderer renderer;
        BoxCollider collider;
        Player player;

        public GateGraveyard(Vector2 position, Player player)
        {
            this.player = player;
            Tag = "GateGraveyard";

            transform = AddComponent<Transform>();
            transform.Position = position;

            renderer = AddComponent<Renderer>();
            renderer.SetImage(Managers.Content.Load<Texture2D>("Sprites/Objects/GateGraveyard"));
            renderer.Pivot = new Vector2(renderer.ImageWidth / 2, renderer.ImageHeight / 2f);

            collider = AddComponent<BoxCollider>();
            collider.OnCollisionEnter += OnCollisionEnter;

            EventManager.OnUpdate += OnUpdate;
        }

        private void OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject.Tag == "Player")
            {
                if (player.HasKey1)
                    Destroy();
                else
                {
                    Vector2 oldPlayerPosition = player.transform.Position;
                    player.transform.Position = new Vector2(oldPlayerPosition.X, oldPlayerPosition.Y + 10);
                }

            }
        }

        private void OnUpdate(GameTime gameTime)
        {
        }

        public override void Destroy()
        {
            EventManager.OnUpdate -= OnUpdate;

            base.Destroy();
        }
    }
}
