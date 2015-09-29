using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniEngine
{
    public class ViewRenderer : Component, IRender
    {
        public Texture2D Image;
        public Rectangle Source;
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
            EventManager.OnRender += OnRender;
        }

        private void OnRender(SpriteBatch spriteBatch)
        {
            if (Image == null)
                return;

            spriteBatch.Draw(
                Image,
                new Rectangle((int)transform.Position.X, (int)transform.Position.Y, (int)(ImageWidth * transform.Scale.X), (int)(ImageHeight * transform.Scale.Y)),
                Source, // sourceRect
                ImageColor,
                transform.Rotation,
                Pivot,
                Orientation,
                PositionZ);
        }
        public void SetImage(Texture2D image, int x, int y)
        {
            Image = image;
            ImageWidth = x;
            ImageHeight = y;
        }

        public override void Destroy()
        {
            EventManager.OnRender -= OnRender;

            base.Destroy();
        }
    }
}
