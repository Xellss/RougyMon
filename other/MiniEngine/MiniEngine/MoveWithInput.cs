using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MiniEngine
{
    /// <summary>
    /// Normaly you Move with WASD:
    /// W for up.
    /// A for left.
    /// S for down.
    /// D for right.
    /// The MoveSpeed is 1.
    /// </summary>
    public class MoveWithInput : Component
    {
        public float Speed = 1;
        public bool CanMove = true;
        public bool MoveWithArrow = false;
        public bool MoveWithArrowsAndWASD = false;

        public bool NextFiledIsPassable = true;
        public Vector2 NextPosition = Vector2.Zero;

        public string Direction = Down;
        public string State = Idle;

        // Directions
        private const string Down = "Front";
        private const string Up = "Back";
        private const string Left = "Left";
        private const string Right = "Right";
        // States
        private const string Idle = "Idle";
        private const string Walk = "Walk";


        private Transform transform;
        private Vector2 moveDirection = Vector2.Zero;
        private Vector2 oldDirection = Vector2.Zero;
        void Start()
        {
            transform = GameObject.GetComponent<Transform>();
            if (transform == null)
                throw new Exception("GameObject needs a Transform component");

            EventManager.OnUpdate += OnUpdate;
            EventManager.OnLateUpdate += OnLateUpdate;
        }

        void OnLateUpdate(GameTime gameTime)
        {
            if (NextFiledIsPassable)
            {
                transform.Translate(moveDirection * Speed);
                moveDirection.X = 0;
                moveDirection.Y = 0;
            }
            else
                moveDirection = oldDirection;
        }
        void OnUpdate(GameTime gameTime)
        {
            Move();
        }
        public void Move()
        {

            if (MoveWithArrowsAndWASD)
                MoveWithArrow = false;

            if (CanMove)
            {
                moveWithWASD();
                moveWithArrows();

                
            }

        }
        private void moveWithWASD()
        {
            KeyboardState key = Keyboard.GetState();

            if (!MoveWithArrow)
            {
                oldDirection = moveDirection;

                if (key.IsKeyDown(Keys.D))
                {
                    moveDirection.X++;
                    Direction = Right;
                    State = Walk;
                }
                else if (key.IsKeyDown(Keys.A))
                {
                    moveDirection.X--;
                    Direction = Left;
                    State = Walk;
                }
                else if (key.IsKeyDown(Keys.S))
                {
                    moveDirection.Y++;
                    Direction = Down;
                    State = Walk;
                }
                else if (key.IsKeyDown(Keys.W))
                {
                    moveDirection.Y--;
                    Direction = Up;
                    State = Walk;
                }
                 else
                    State = Idle;

                takeNextPosition(moveDirection);
            }



        }
        private void moveWithArrows()
        {
            KeyboardState key = Keyboard.GetState();

            if (MoveWithArrow || MoveWithArrowsAndWASD)
            {
                oldDirection = moveDirection;

                if (key.IsKeyDown(Keys.Right))
                {
                    moveDirection.X++;
                    Direction = Right;
                    State = Walk;
                }
                else if (key.IsKeyDown(Keys.Left))
                {
                    moveDirection.X--;
                    Direction = Left;
                    State = Walk;
                }
                else if (key.IsKeyDown(Keys.Down))
                {
                    moveDirection.Y++;
                    Direction = Down;
                    State = Walk;
                }
                else if (key.IsKeyDown(Keys.Up))
                {
                    moveDirection.Y--;
                    Direction = Up;
                    State = Walk;
                }
                else
                    State = Idle;

                takeNextPosition(moveDirection);
            }
        }
        private void takeNextPosition(Vector2 moveDirection)
        {
            NextPosition = transform.Position + (moveDirection * Speed);
        }


        public override void Destroy()
        {
            EventManager.OnUpdate -= OnUpdate;
            EventManager.OnLateUpdate -= OnLateUpdate;
            
            base.Destroy();
        }
    }
}
