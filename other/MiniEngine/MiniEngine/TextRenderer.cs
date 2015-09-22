using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniEngine
{
    public class TextRenderer : Component
    {
        public SpriteFont Font;
        public string Text;
        public Color TextColor = Color.White;
        public float Rotation = 0;
        public float Scale = 1;
        public float Depth = 0;

        private Transform transform;

        void Start()
        {
            transform = GameObject.GetComponent<Transform>();
            if (transform == null)
                throw new Exception("GameObject needs a Transform component");

            EventManager.OnRender += OnRender;
        }

        private void OnRender(SpriteBatch spriteBatch)
        {
            if (Font == null)
                return;

            spriteBatch.DrawString(Font, Text, transform.Position, TextColor, Rotation, Vector2.Zero, Scale, SpriteEffects.None, Depth);
        }

        public override void Destroy()
        {
            EventManager.OnRender -= OnRender;

            base.Destroy();
        }
    }
}
