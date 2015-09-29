using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniEngine
{
    public class Renderer : Component, IRender
    {
        public Texture2D Image;
        public Color ImageColor = Color.White;
        private int imageWidth = 10;
        private int imageHeight = 10;

        public int ImageWidth { get { return this.imageWidth; } set { this.imageWidth = value; } }
        public int ImageHeight { get { return this.imageHeight; } set { this.imageHeight = value; } }

        public int PositionZ = 0;
        public Vector2 Pivot = Vector2.Zero;
        public SpriteEffects Orientation = SpriteEffects.None;

        private Transform transform;



        void Start()
        {
            transform = GameObject.GetComponent<Transform>();
            if (transform == null)
                throw new Exception("GameObject needs a Transform component");

            EventManager.OnRender += OnRender;
        }

        void OnRender(SpriteBatch spriteBatch)
        {
            if (Image == null)
                return;

            spriteBatch.Draw(
                Image,
                new Rectangle((int)transform.Position.X, (int)transform.Position.Y, (int)(ImageWidth * transform.Scale.X), (int)(ImageHeight * transform.Scale.Y)),
                null, // sourceRect
                ImageColor,
                transform.Rotation,
                Pivot,
                Orientation,
                PositionZ);
        }

        public void SetImage(Texture2D image)
        {
            Image = image;
            ImageWidth = image.Width;
            ImageHeight = image.Height;
        }

        public void SetImage(Texture2D image, int width, int height)
        {
            Image = image;
            ImageWidth = width;
            ImageHeight = height;
        }

        public override void Destroy()
        {
            EventManager.OnRender -= OnRender;

            base.Destroy();
        }
    }
}
