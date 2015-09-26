using GameStateManagement;
using GameStateManagementSample;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniEngine;

namespace RougyMon
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont arial;
        SpriteFont comicSans;
        private ScreenManager screenManager;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            IsMouseVisible = true;
            graphics.IsFullScreen = true;
        }

        protected override void LoadContent()
        {
            Managers.Content = this.Content;
            Managers.Graphics = this.graphics;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            arial = Content.Load<SpriteFont>("Fonts/Arial");
            comicSans = Content.Load<SpriteFont>("Fonts/ComicSansMS");

            screenManager = new ScreenManager(this);
            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            EventManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            EventManager.Render(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
