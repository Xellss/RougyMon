using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MiniEngine
{
    public class AnimationCollider : Component
    {
        public delegate void CollisionEvent(AnimationCollider other);
        public event CollisionEvent OnCollisionEnterAnimation, OnCollisionStayAnimation, OnCollisionExitAnimation;

        private int width, height;
        private Rectangle bounds;

        private List<AnimationCollider> collisionsAnimation = new List<AnimationCollider>();

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

        public void CheckCollision(AnimationCollider other)
        {
            if (bounds.Intersects(other.bounds))
            {
                if (!collisionsAnimation.Contains(other))
                {
                    collisionsAnimation.Add(other);
                    if (OnCollisionEnterAnimation != null)
                        OnCollisionEnterAnimation(other);
                }
                else
                {
                    if (OnCollisionStayAnimation != null)
                        OnCollisionStayAnimation(other);
                }
            }
            else
            {
                AnimationCollider exitCollider = collisionsAnimation.Find((AnimationCollider collider) => collider == other);
                if (exitCollider != null)
                {
                    collisionsAnimation.Remove(exitCollider);
                    if (OnCollisionExitAnimation != null)
                        OnCollisionExitAnimation(other);
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
            Renderer renderer = GameObject.GetComponent<Renderer>();
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
