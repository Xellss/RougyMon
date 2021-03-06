﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniEngine;

namespace RougyMon
{
    class Key_2 : GameObject
    {
        Transform transform;
        Renderer renderer;
        BoxCollider collider;

        public Key_2(Vector2 position)
        {
            Tag = "Key_2";

            transform = AddComponent<Transform>();
            transform.Position = position;

            renderer = AddComponent<Renderer>();
            renderer.SetImage(Managers.Content.Load<Texture2D>("Sprites/Objects/Key_2"));
            renderer.Pivot = new Vector2(renderer.ImageWidth / 2, renderer.ImageHeight / 2f);

            collider = AddComponent<BoxCollider>();
            collider.OnCollisionEnter += OnCollisionEnter;

            EventManager.OnUpdate += OnUpdate;

        }

        private void OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject.Tag == "Player")
                Destroy();
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
