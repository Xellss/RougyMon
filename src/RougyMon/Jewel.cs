using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniEngine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace RougyMon
{
    class Jewel : GameObject
    {
        Transform transform;
        Renderer renderer;
        BoxCollider collider;

        public Jewel(Vector2 position)
        {
            Tag = "Jewel";

            transform = AddComponent<Transform>();
            transform.Position = position;

            renderer = AddComponent<Renderer>();
            renderer.SetImage(Managers.Content.Load<Texture2D>("Sprites/Objects/Jewel"));
            renderer.Pivot = new Vector2(renderer.ImageWidth / 2, renderer.ImageHeight / 2f);

            collider = AddComponent<BoxCollider>();
            collider.OnCollisionEnter += OnCollisionEnter;


        }

        private void OnCollisionEnter(BoxCollider other)
        {
            //if (other.GameObject.Tag == "Player")
                //Destroy();
        }

        public override void Destroy()
        {

            base.Destroy();
        }
    }
}

