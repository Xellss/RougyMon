using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prototype
{
    class Player
    {
        public Vector2 Position { get; private set; }
        public Texture2D Image { get; private set; }
        public Game1 Game { get; private set; }
        public Tile tile;
        public float MoveSpeed;

        private Vector2 direction;

        public Player(Texture2D image, Game1 game)
        {
            Game = game;
            Image = image;
        }

        public void Update(GameTime gameTime)
        {
            Vector2 position;
            position = Position + direction * MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            RectangleF newRectangle = new RectangleF(position.X, position.Y + 0.25f, 0.5f, 0.75f);

            if (Game.CanMoveTo(newRectangle))
            {
                Position = position;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 mapPosition = new Vector2(Position.X * Map.TileWidth, Position.Y * Map.TileHeight);
            spriteBatch.Draw(Image, mapPosition, Color.White);
        }

        public void Move(Vector2 direction)
        {
            Position += direction;
        }

        public void SetDirection(Vector2 direction)
        {
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
            this.direction = direction;
        }
    }
}
