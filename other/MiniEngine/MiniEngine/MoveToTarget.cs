using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MiniEngine
{
    /// <summary>
    /// The Object who take this Component
    /// Move from the SpawnPosition to the TargetPosition
    /// </summary>
    public class MoveToTarget : Component
    {
        public float Speed = 1;

        public bool NextFiledIsPassable = true;
        public Vector2 NextPosition = Vector2.Zero;

        private Transform transform;
        private Vector2 target = Vector2.Zero;
        private Vector2 oldTarget = Vector2.Zero;
        private Vector2 moveDirection = Vector2.Zero;

        void Start()
        {
            transform = GameObject.GetComponent<Transform>();
            if (transform == null)
                throw new Exception("GameObject needs a Transform component");

            EventManager.OnUpdate += OnUpdate;
        }

        void OnUpdate(GameTime gameTime)
        {
            Move();
        }

        public void SetTarget(Vector2 target)
        {
            this.target = target;
            moveDirection = target - transform.Position;
            moveDirection.Normalize();
            takeNextPosition(moveDirection);
            wayIsBlocked();
        }

        public void Move()
        {
            if (target != Vector2.Zero)
            {
                float distance = Vector2.Distance(transform.Position, target);
                if (distance > 2)
                    transform.Translate(moveDirection * Speed);
                else
                    transform.Position = target;
            }
        }
        private void takeNextPosition(Vector2 moveDirection)
        {
            NextPosition = transform.Position + moveDirection;
        }
        private void wayIsBlocked()
        {
            if (!NextFiledIsPassable)
                Stop();
            else
                target = oldTarget;
        }
        public void Stop()
        {
            oldTarget = target;
            target = Vector2.Zero;
        }

        public override void Destroy()
        {
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }
    }
}
