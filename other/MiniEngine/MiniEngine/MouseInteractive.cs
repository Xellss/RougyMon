using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MiniEngine
{
    public class MouseInteractive : Component
    {
        // Callbacks
        public delegate void MouseEvent(int x, int y);
        public MouseEvent OnClick, OnHold;

        // Components
        private Transform transform;

        // Fields
        private Rectangle bounds = new Rectangle();
        private bool isPressed = false;

        void Start()
        {
            transform = GameObject.GetComponent<Transform>();
            if (transform == null)
                throw new Exception("GameObject needs a Transform component");

            EventManager.OnUpdate += OnUpdate;
        }

        void OnUpdate(GameTime gameTime)
        {
            UpdatePosition();
            CheckMouseInput();
        }

        public void SetSize(int width, int height)
        {
            bounds.Width = width;
            bounds.Height = height;
        }

        private void CheckMouseInput()
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (bounds.Contains(mouseState.X, mouseState.Y))
                {
                    if (!isPressed)
                    {
                        isPressed = true;
                        if (OnClick != null)
                            OnClick(mouseState.X, mouseState.Y);
                    }
                    else
                    {
                        if (OnHold != null)
                            OnHold(mouseState.X, mouseState.Y);
                    }
                }
            }
            else if (mouseState.LeftButton == ButtonState.Released)
            {
                isPressed = false;
            }
        }

        private void UpdatePosition()
        {
            bounds.X = (int)transform.Position.X;
            bounds.Y = (int)transform.Position.Y;
        }

        public override void Destroy()
        {
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }
    }
}
