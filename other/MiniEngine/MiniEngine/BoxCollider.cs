using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MiniEngine
{
    public class BoxCollider : Component
    {
        public delegate void CollisionEvent(BoxCollider other);
        public event CollisionEvent OnCollisionEnter, OnCollisionStay, OnCollisionExit;

        private int width, height;
        private Rectangle bounds;

        private List<BoxCollider> collisions = new List<BoxCollider>();

        private Transform transform;

        void Start()
        {
            transform = GameObject.GetComponent<Transform>();
            if (transform == null)
                throw new Exception("GameObject needs a Transform component");

            UpdatePosition();

            CollisionManager.AddCollider(this);
            BoundsToRendereSize();

            EventManager.OnLateUpdate += OnLateUpdate;
        }

        private void OnLateUpdate(GameTime gameTime)
        {
            UpdatePosition();
        }

        public void SetSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void CheckCollision(BoxCollider other)
        {
            if (bounds.Intersects(other.bounds))
            {
                if (!collisions.Contains(other))
                {
                    collisions.Add(other);
                    if (OnCollisionEnter != null)
                        OnCollisionEnter(other);
                }
                else
                {
                    if (OnCollisionStay != null)
                        OnCollisionStay(other);
                }
            }
            else
            {
                BoxCollider exitCollider = collisions.Find((BoxCollider collider) => collider == other);
                if (exitCollider != null)
                {
                    collisions.Remove(exitCollider);
                    if (OnCollisionExit != null)
                        OnCollisionExit(other);
                }
            }
        }

        private void UpdatePosition()
        {
            bounds.X = (int)transform.Position.X;
            bounds.Y = (int)transform.Position.Y;
        }

        private void BoundsToRendereSize()
        {
            IRender renderer = GameObject.GetComponent<Renderer>();

            if (renderer == null)
            {
                renderer = GameObject.GetComponent<ViewRenderer>();
            }

            if (renderer != null)
            {
                bounds.Width = renderer.ImageWidth;
                bounds.Height = renderer.ImageHeight;
            }
        }

        public override void Destroy()
        {
            CollisionManager.RemoveCollider(this);
            EventManager.OnLateUpdate -= OnLateUpdate;
            base.Destroy();
        }
    }
}
