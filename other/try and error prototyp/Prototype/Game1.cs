#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace Prototype
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Map map;
        Player player;
        Key key;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
            graphics.IsFullScreen = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            map = new Map(Content.Load<Texture2D>("Tiles"));
            map.LoadMapFromTextfile(Content.RootDirectory + "/Map.txt", 200, 200);
            //map.LoadMapFromTextfile(Content.RootDirectory + "/BigMap.txt", 200, 200);


            //map.LoadMapFromImage(Content.Load<Texture2D>("MapImageBMP"));
            player = new Player(Content.Load<Texture2D>("Player"), this);
            key = new Key(Content.Load<Texture2D>("key"));
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            ProcessPlayerInput();
            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            map.RenderMap(spriteBatch);
            key.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ProcessPlayerInput()
        {
            Vector2 moveDirection = Vector2.Zero;
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                moveDirection.X++;
            }
            if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            {
                moveDirection.X--;
            }
            if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            {
                moveDirection.Y++;
            }
            if (state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                moveDirection.Y--;
            }
            player.SetDirection(moveDirection);
            if (moveDirection != Vector2.Zero)
            {
                MovePlayer(moveDirection);
            }
        }

        private void MovePlayer(Vector2 direction)
        {
            Tile nextTile = map.GetTile(player.Position);
            if (nextTile == null)
                return;

            if (nextTile.Type == Tile.Types.Moor)
            {
                player.MoveSpeed = 1;
            }
            else
            {
                player.MoveSpeed = 10;
            }
        }

        public bool CanMoveTo(RectangleF recCollider)
        {
            Vector2 v;
            for (v.X = recCollider.Location.X; v.X <= recCollider.Right; v.X += 0.25f)
            {
                for (v.Y = recCollider.Location.Y; v.Y <= recCollider.Bottom; v.Y += 0.25f)
                {
                    if (!map.IsPassable(v))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Vector2 ConvertScreenToWorldPoint(int x, int y)
        {
            int tileX = x / Map.TileWidth;
            int tileY = y / Map.TileHeight;

            return new Vector2(tileX, tileY);
        }
    }
}
