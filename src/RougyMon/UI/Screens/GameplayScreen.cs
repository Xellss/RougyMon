#region File Description

#endregion

#region Using Statements
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
using RougyMon;
using RougyMon.UI.Screens;
using MiniEngine;
#endregion

namespace GameStateManagementSample
{
    class GameplayScreen : GameScreen
    {
        #region Fields

        ContentManager content;
        SpriteFont gameFont;

        Random random = new Random();

        float pauseAlpha;

        InputAction pauseAction;
        private Camera camera;
        private Player player;
        private Map map;

        public NewTimer timer;

        bool debug;
        InputAction debugAction;
        private Key key_1;
        Key_2 key_2;
        OrkGraveyard orkGraveyard;
        OrkForest orkForest;
        Spider spider;
        Skeleton skeleton;
        SkeletonKing skeletonKing;

        #endregion

        #region Initialization

        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            pauseAction = new InputAction(
                new Buttons[] { Buttons.Start, Buttons.Back },
                new Keys[] { Keys.Escape },
                true);
            debugAction = new InputAction(
                new Buttons[] { Buttons.A },
                new Keys[] { Keys.E },
                true);

            timer = new NewTimer();
            timer.Time = new TimeSpan(0, 10, 0);
        

        }

        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = Managers.Content;

                gameFont = content.Load<SpriteFont>("Fonts/ComicSansMS");

                Thread.Sleep(1000);
            }

#if WINDOWS_PHONE
            if (Microsoft.Phone.Shell.PhoneApplicationService.Current.State.ContainsKey("PlayerPosition"))
            {
                playerPosition = (Vector2)Microsoft.Phone.Shell.PhoneApplicationService.Current.State["PlayerPosition"];
                enemyPosition = (Vector2)Microsoft.Phone.Shell.PhoneApplicationService.Current.State["EnemyPosition"];
            }
#endif

            map = new Map(content.Load<Texture2D>("Map/Tiles"));
            map.LoadMapFromImage(content.Load<Texture2D>("Map/MainMap"));

            orkGraveyard = new OrkGraveyard(new Vector2(4460, 2280), map, new Vector2(5015, 2280), timer);
            orkForest = new OrkForest(new Vector2(4460, 2380), map, new Vector2(5015, 2380), timer);
            spider = new Spider(new Vector2(4460, 2480), map, new Vector2(5015, 2480), timer);
            skeleton = new Skeleton(new Vector2(4460, 2680), map, new Vector2(5015, 2680), timer);
            skeletonKing = new SkeletonKing(new Vector2(4460, 2880), map, new Vector2(5015, 2880), timer);
            skeletonKing.moveSpeed = 3;

            key_1 = new Key(new Vector2(2258, 4175));
            key_2 = new Key_2(new Vector2(2416, 3683));


            player = new Player(new Vector2(4400, 2230), map);

      

            //Soldier soldier = new Soldier();
            //soldier.Animation = new SpriteAnimation(
            //    "Soldier",
            //    Content.Load<Texture2D>("Soldier"),
            //    Content.RootDirectory + "/Soldier.xml"
            //    );
            //soldier.Animation.FrameDelay = 100;




            camera = new Camera(Managers.Graphics.GraphicsDevice.Viewport);
            timer.Start();
        }

        public override void Deactivate()
        {
#if WINDOWS_PHONE
            Microsoft.Phone.Shell.PhoneApplicationService.Current.State["PlayerPosition"] = playerPosition;
            Microsoft.Phone.Shell.PhoneApplicationService.Current.State["EnemyPosition"] = enemyPosition;
#endif

            base.Deactivate();
        }

        public override void Unload()
        {
            content.Unload();

#if WINDOWS_PHONE
            Microsoft.Phone.Shell.PhoneApplicationService.Current.State.Remove("PlayerPosition");
            Microsoft.Phone.Shell.PhoneApplicationService.Current.State.Remove("EnemyPosition");
#endif
        }

        #endregion

        #region Update and Draw

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            camera.OnUpdate(player.transform.Position, 97 * 32, 54 * 32);
            timer.OnUpdate(gameTime);

            if (timer.Time <= TimeSpan.Zero || debug || ScreenManager.WinScreen)
            {
                orkGraveyard.Destroy();
                orkForest.Destroy();
                spider.Destroy();
                skeleton.Destroy();
                skeletonKing.Destroy();
                key_1.Destroy();
                key_2.Destroy();
                player.Destroy();
                timer.Stop();
                ScreenManager.Clear();



                if (ScreenManager.WinScreen)
                {
                    ScreenManager.AddScreen(new PlayerWonScreen(timer.Time), null);
                }
                if (debug)
                {
                    ScreenManager.AddScreen(new PlayerWonScreen(timer.Time), null);
                }
                else if (timer.Time <= TimeSpan.Zero)
                {
                    ScreenManager.AddScreen(new GameOverScreen(), null);
                }

            }
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            PlayerIndex player;
            if (pauseAction.Evaluate(input, ControllingPlayer, out player) || gamePadDisconnected)
            {
#if WINDOWS_PHONE
                ScreenManager.AddScreen(new PhonePauseScreen(), ControllingPlayer);
#else
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
#endif
            }

            if (debugAction.Evaluate(input, ControllingPlayer, out player))
            {
                debug = true;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            Managers.Graphics.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);
            spriteBatch.Begin();

            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.matrix);
            map.RenderMap(spriteBatch);
        }

        public override void LateDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            spriteBatch.Begin();
            string text = string.Format("Time left: {0:mm\\:ss}", timer.Time);
            float size = Fonts.Arial.MeasureString(text).X;
            spriteBatch.DrawString(Fonts.Arial, text, new Vector2(Managers.Graphics.GraphicsDevice.Viewport.Width - size, 10), Color.White); base.LateDraw(spriteBatch);
            spriteBatch.End();
        }

        #endregion
    }
}
