using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MiniEngine;

namespace RougyMon
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map;
        Player player;
        Camera camera;

        SpriteFont arial;
        SpriteFont comicSans;

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

            map = new Map(Content.Load<Texture2D>("Map/Tiles"));
            //map.LoadMapFromTextfile(Content.RootDirectory + "/Map/Map.txt", 42, 24);
            map.LoadMapFromImage(Content.Load<Texture2D>("Map/UnitedMapBMP"));


            player = new Player(new Vector2(1000, 50), map);
            new Key(new Vector2(555, 100));

            arial = Content.Load<SpriteFont>("Fonts/Arial");
            comicSans = Content.Load<SpriteFont>("Fonts/ComicSansMS");
            new UITimer(player);
            camera = new Camera(GraphicsDevice.Viewport);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            EventManager.Update(gameTime);
            camera.OnUpdate(player.transform.Position, 97 * 32, 54 * 32);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.matrix);
            map.RenderMap(spriteBatch);
            EventManager.Render(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
