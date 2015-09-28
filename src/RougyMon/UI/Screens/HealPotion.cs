using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniEngine;

namespace RougyMon
{
    class HealPotion : GameObject
    {
        Transform transform;
        Renderer renderer;
        BoxCollider collider;
        NewTimer timer;

        public HealPotion(Vector2 position, NewTimer timer)
        {
            Tag = "HealPotion";
            this.timer = timer;

            transform = AddComponent<Transform>();
            transform.Position = position;

            renderer = AddComponent<Renderer>();
            renderer.SetImage(Managers.Content.Load<Texture2D>("Sprites/Objects/Heal_Potion"));
            renderer.Pivot = new Vector2(renderer.ImageWidth / 2, renderer.ImageHeight / 2f);

            collider = AddComponent<BoxCollider>();
            collider.OnCollisionEnter += OnCollisionEnter;

            EventManager.OnUpdate += OnUpdate;

        }

        private void OnCollisionEnter(BoxCollider other)
        {
            if (other.GameObject.Tag == "Player")
            {
                timer.Time = timer.Time.Add(new TimeSpan(0, 0, 20));
                Destroy();
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
