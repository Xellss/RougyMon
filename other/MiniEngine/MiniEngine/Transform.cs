using System;
using Microsoft.Xna.Framework;

namespace MiniEngine
{
    public class Transform : Component
    {
        private Vector2 position = Vector2.Zero;
        private float rotation = 0;

        public Vector2 Scale = Vector2.One;

        public Vector2 Forward { get { return new Vector2((float)Math.Cos(Rotation - (float)(Math.PI / 2)), -(float)Math.Sin(Rotation + (float)(Math.PI / 2))); } }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float Rotation
        {
            get { return (float)(rotation * 180 / Math.PI); }
            set { rotation = (float)(value * Math.PI / 180); }
        }

        public float RotationRadians
        {
            get { return rotation; }
        }

        public void Translate(float x, float y)
        {
            position += new Vector2(x, y);
        }

        public void Translate(Vector2 translation)
        {
            position += translation;
        }

        public void LookAt(Vector2 target)
        {
            float distanceX = target.X - position.X;
            float distanceY = target.Y - position.Y;
            Rotation = (float)Math.Atan2(distanceY, distanceX) + (float)(Math.PI / 2);
        }

    }
}
