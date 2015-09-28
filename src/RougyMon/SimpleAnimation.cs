using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MiniEngine;

namespace RougyMon

{
    class SimpleAnimation : GameObject
    {
        private const int SpriteWidth = 64;
        private const int SpriteHeight = 64;
        private const int MaxFrames = 11;

        private Texture2D image;
        private int currentFrame;
        private float frameDelay = 100;
        private Rectangle sourceRect = Rectangle.Empty;
        private float timer; 

        public SimpleAnimation()
        {
            //image = GameManager.Content.Load<Texture2D>("RougyMon");
            sourceRect.Width = SpriteWidth;
            sourceRect.Height = SpriteHeight;
        }

        //public override void Update(GameTime gameTime)
        //{
        //    CalculateFrame(gameTime);
        //}

        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    CalculateSourceRect();
        //    spriteBatch.Draw(image, Position, sourceRect, Color.White, 0, Vector2.Zero, 2f, SpriteEffects.None, 0);
        //}

        private void CalculateFrame(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer >= frameDelay)
            {
                timer = 0;
                if (currentFrame < MaxFrames - 1)
                    currentFrame++;
                else
                    currentFrame = 0;
//IDLE
            }
        }

        private void CalculateSourceRect()
        {
            sourceRect.X = sourceRect.Width * currentFrame;
        }
    }
}
