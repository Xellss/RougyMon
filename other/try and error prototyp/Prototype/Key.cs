using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prototype
{
    class Key
    {
        public Vector2 Position { get; private set; }
        public Texture2D Image { get; private set; }
        public RectangleF KeyCollider = new RectangleF();
        
        public Key(Texture2D image)
        {
            KeyCollider.Height = 32;
            KeyCollider.Width = 25;
            Image = image;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 mapPosition = new Vector2(Position.X + 540, Position.Y + 80);
            spriteBatch.Draw(Image, mapPosition, Color.White);
        }
    }
}
