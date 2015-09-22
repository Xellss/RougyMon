using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MiniEngine
{
    public static class CollisionManager
    {
        private static List<BoxCollider> colliders = new List<BoxCollider>();
        private static List<BoxCollider> addedColliders = new List<BoxCollider>();
        private static List<BoxCollider> removedColliders = new List<BoxCollider>();

        static CollisionManager()
        {
            EventManager.OnUpdate += OnUpdate;
        }

        static void OnUpdate(GameTime gameTime)
        {
            UpdateColliders();
            CheckCollisions();
        }

        public static void AddCollider(BoxCollider collider)
        {
            if (!colliders.Contains(collider))
                colliders.Add(collider);
        }

        public static void RemoveCollider(BoxCollider collider)
        {
            if (!removedColliders.Contains(collider))
                removedColliders.Add(collider);
        }

        private static void CheckCollisions()
        {
            foreach (var colliderA in colliders)
            {
                foreach (var colliderB in colliders)
                {
                    if(colliderA != colliderB)
                        colliderA.CheckCollision(colliderB);
                }
            }
        }

        private static void UpdateColliders()
        {
            if (addedColliders.Count > 0)
            {
                colliders.AddRange(addedColliders);
                addedColliders.Clear();
            }

            if (removedColliders.Count > 0)
            {
                foreach (var removedCollider in removedColliders)
                    colliders.Remove(removedCollider);

                removedColliders.Clear();
            }
        }
    }
}
