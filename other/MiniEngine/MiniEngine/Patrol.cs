﻿using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MiniEngine
{
    /// <summary>
    /// The Object who take this Component
    /// patrol from the SpawnPosition to the TargetPosition
    /// and back.
    /// </summary>
    public class Patrol : Component
    {
        public float Speed = 1;
        public bool CanPatrol = true;

        public bool NextFiledIsPassable = true;
        public Vector2 NextPosition = Vector2.Zero;

        private bool patrolEnd = false;
        private Transform transform;
        private Vector2 target = Vector2.Zero;
        private Vector2 moveDirection = Vector2.Zero;
        private Vector2 oldDirection = Vector2.Zero;
        private Vector2 startPosition = Vector2.Zero;

        void Start()
        {
            transform = GameObject.GetComponent<Transform>();
            if (transform == null)
                throw new Exception("GameObject needs a Transform component");

            startPosition = transform.Position;
            EventManager.OnUpdate += OnUpdate;
        }

        void OnUpdate(GameTime gameTime)
        {
            Move();
        }

        public void PatrolToTarget(Vector2 target)
        {
            this.target = target;

            if (target == transform.Position)
                patrolEnd = true;
            else
                patrolEnd = false;

            if (!patrolEnd)
                moveDirection = target - transform.Position;
            else
                moveDirection = startPosition - transform.Position;

            moveDirection.Normalize();
            takeNextPosition(moveDirection);
            wayIsBlocked();

        }


        public void Move()
        {
            if (CanPatrol)
            {
                PatrolToTarget(target);

                if (target != Vector2.Zero && !patrolEnd)
                {
                    float distance = Vector2.Distance(transform.Position, target);
                    if (distance > 2)
                        transform.Translate(moveDirection * Speed);
                    else
                        transform.Position = target;
                }

                else if (startPosition != Vector2.Zero && patrolEnd)
                {
                    float distance = Vector2.Distance(transform.Position, startPosition);
                    if (distance > 2)
                        transform.Translate(moveDirection * Speed);
                    else
                        transform.Position = startPosition;
                }
            }
        }
        private void takeNextPosition(Vector2 moveDirection)
        {
            NextPosition = transform.Position + moveDirection;
        }
        private void wayIsBlocked()
        {
            if (!NextFiledIsPassable)
            {
                if (!patrolEnd)
                {
                    patrolEnd = true;
                    moveDirection = startPosition - transform.Position;
                }
                else
                {
                    patrolEnd = false;
                    moveDirection = target - transform.Position;
                }
                moveDirection.Normalize();
            }
        }
        public void Stop()
        {
            target = Vector2.Zero;
        }


        public override void Destroy()
        {
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }
    }
}
