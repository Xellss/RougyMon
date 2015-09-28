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
        NewTimer timer;
        bool debug;
        InputAction debugAction;
        private Key key_1;
        Key_2 key_2;
        HealPotion[] healPotion = new HealPotion[14];
        Gold[] gold = new Gold[56];
        OrkGraveyard[] orkGraveyard = new OrkGraveyard[1];
        OrkForest[] orkForest = new OrkForest[1];
        Spider[] spider = new Spider[1];
        Skeleton[] skeleton = new Skeleton[1];
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
            //map.LoadMapFromTextfile(content.RootDirectory + "/Map/Map.txt", 42, 24);
            //map.LoadMapFromImage(content.Load<Texture2D>("Map/UnitedMapBMP"));
            map.LoadMapFromImage(content.Load<Texture2D>("Map/MainMap"));
            //Objects

            goldSpawn();
            healPotionSpawn();


            //orkGraveyard = new OrkGraveyard(new Vector2(4460, 2280), map, new Vector2(5015, 2280));
            //orkForest = new OrkForest(new Vector2(4460, 2380), map, new Vector2(5015, 2380));
            //spider = new Spider(new Vector2(4460, 2480), map, new Vector2(5015, 2480));
            //skeleton = new Skeleton(new Vector2(4460, 2680), map, new Vector2(5015, 2680));
            //skeletonKing = new SkeletonKing(new Vector2(4460, 2880), map, new Vector2(5015, 2880));
            //skeletonKing.moveSpeed = 3;

            key_1 = new Key(new Vector2(992, 2560));
            key_2 = new Key_2(new Vector2(1136, 2096));


            player = new Player(new Vector2(2976, 2240), map);
            //new UITimer(timer);
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
                destroyAllObjects();
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
        private void destroyAllObjects()
        {
            for (int i = 0; i < healPotion.GetLength(0); i++)
            {
                healPotion[i].Destroy();
            }
            for (int i = 0; i < orkForest.GetLength(0); i++)
            {
                orkForest[i].Destroy();
            }
            for (int i = 0; i < orkGraveyard.GetLength(0); i++)
            {
                orkGraveyard[i].Destroy();
            }
            for (int i = 0; i < spider.GetLength(0); i++)
            {
                spider[i].Destroy();
            }
            for (int i = 0; i < skeleton.GetLength(0); i++)
            {
                skeleton[i].Destroy();
            }


            skeletonKing.Destroy();
            key_1.Destroy();
            key_2.Destroy();
            player.Destroy();
        }
        private void goldSpawn()
        {
            int middleOfTile = 16;

            gold[0] = new Gold(new Vector2(2528 + middleOfTile, 2080 + middleOfTile));
            gold[1] = new Gold(new Vector2(2528 + middleOfTile, 2272 + middleOfTile));
            gold[2] = new Gold(new Vector2(2526 + middleOfTile, 2464 + middleOfTile));
            gold[3] = new Gold(new Vector2(2720 + middleOfTile, 2080 + middleOfTile));
            gold[4] = new Gold(new Vector2(2816 + middleOfTile, 2304 + middleOfTile));
            gold[5] = new Gold(new Vector2(2784 + middleOfTile, 2464 + middleOfTile));
            gold[6] = new Gold(new Vector2(3040 + middleOfTile, 2400 + middleOfTile));
            gold[7] = new Gold(new Vector2(3136 + middleOfTile, 2208 + middleOfTile));
            gold[8] = new Gold(new Vector2(3264 + middleOfTile, 2080 + middleOfTile));
            gold[9] = new Gold(new Vector2(3456 + middleOfTile, 2112 + middleOfTile));
            gold[10] = new Gold(new Vector2(3264 + middleOfTile, 2336 + middleOfTile));
            gold[11] = new Gold(new Vector2(3360 + middleOfTile, 2496 + middleOfTile));
            gold[12] = new Gold(new Vector2(3456 + middleOfTile, 2464 + middleOfTile));
            gold[13] = new Gold(new Vector2(3520 + middleOfTile, 2528 + middleOfTile));
            gold[14] = new Gold(new Vector2(3584 + middleOfTile, 2592 + middleOfTile));
            gold[15] = new Gold(new Vector2(3520 + middleOfTile, 2688 + middleOfTile));
            gold[16] = new Gold(new Vector2(4225 + middleOfTile, 4225 + middleOfTile));
            gold[17] = new Gold(new Vector2(1632 + middleOfTile, 2400 + middleOfTile));
            gold[18] = new Gold(new Vector2(1568 + middleOfTile, 2240 + middleOfTile));
            gold[19] = new Gold(new Vector2(1536 + middleOfTile, 2048 + middleOfTile));
            gold[20] = new Gold(new Vector2(1472 + middleOfTile, 2144 + middleOfTile));
            //gold[21] = new Gold(new Vector2(1024 + middleOfTile, 2080 + middleOfTile));
            gold[22] = new Gold(new Vector2(1344 + middleOfTile, 2432 + middleOfTile));
            gold[23] = new Gold(new Vector2(1280 + middleOfTile, 2272 + middleOfTile));
            gold[24] = new Gold(new Vector2(1216 + middleOfTile, 2336 + middleOfTile));
            gold[25] = new Gold(new Vector2(1184 + middleOfTile, 2112 + middleOfTile));
            gold[26] = new Gold(new Vector2(1184 + middleOfTile, 2016 + middleOfTile));
            gold[27] = new Gold(new Vector2(1056 + middleOfTile, 2016 + middleOfTile));
            gold[28] = new Gold(new Vector2(1056 + middleOfTile, 2112 + middleOfTile));
            gold[29] = new Gold(new Vector2(1024 + middleOfTile, 2240 + middleOfTile));
            gold[30] = new Gold(new Vector2(896 + middleOfTile, 2176 + middleOfTile));
            gold[31] = new Gold(new Vector2(704 + middleOfTile, 2208 + middleOfTile));
            gold[32] = new Gold(new Vector2(1024 + middleOfTile, 2304 + middleOfTile));
            gold[33] = new Gold(new Vector2(704 + middleOfTile, 2432 + middleOfTile));
            gold[34] = new Gold(new Vector2(736 + middleOfTile, 2592 + middleOfTile));
            gold[35] = new Gold(new Vector2(640 + middleOfTile, 2048 + middleOfTile));
            gold[36] = new Gold(new Vector2(2592 + middleOfTile, 1728 + middleOfTile));
            gold[37] = new Gold(new Vector2(2592 + middleOfTile, 1696 + middleOfTile));
            gold[38] = new Gold(new Vector2(2592 + middleOfTile, 1664 + middleOfTile));
            gold[39] = new Gold(new Vector2(2592 + middleOfTile, 1632 + middleOfTile));
            gold[40] = new Gold(new Vector2(2752 + middleOfTile, 1248 + middleOfTile));
            gold[41] = new Gold(new Vector2(2816 + middleOfTile, 1152 + middleOfTile));
            gold[42] = new Gold(new Vector2(2688 + middleOfTile, 1152 + middleOfTile));
            gold[43] = new Gold(new Vector2(2240 + middleOfTile, 1120 + middleOfTile));
            gold[44] = new Gold(new Vector2(2112 + middleOfTile, 1248 + middleOfTile));
            gold[45] = new Gold(new Vector2(2112 + middleOfTile, 1024 + middleOfTile));
            gold[46] = new Gold(new Vector2(2208 + middleOfTile, 864 + middleOfTile));
            gold[47] = new Gold(new Vector2(2112 + middleOfTile, 864 + middleOfTile));
            gold[48] = new Gold(new Vector2(2080 + middleOfTile, 704 + middleOfTile));
            gold[49] = new Gold(new Vector2(2048 + middleOfTile, 1184 + middleOfTile));
            gold[50] = new Gold(new Vector2(1888 + middleOfTile, 1184 + middleOfTile));
            gold[51] = new Gold(new Vector2(1696 + middleOfTile, 1024 + middleOfTile));
            gold[52] = new Gold(new Vector2(1632 + middleOfTile, 1184 + middleOfTile));
            gold[53] = new Gold(new Vector2(1440 + middleOfTile, 1088 + middleOfTile));
            gold[54] = new Gold(new Vector2(1376 + middleOfTile, 928 + middleOfTile));
            gold[55] = new Gold(new Vector2(1440 + middleOfTile, 704 + middleOfTile));
        }
        private void healPotionSpawn()
        {
            int middleOfTile = 16;

            healPotion[0] = new HealPotion(new Vector2(2976 + middleOfTile, 2304 + middleOfTile));
            healPotion[1] = new HealPotion(new Vector2(3648 + middleOfTile, 2432 + middleOfTile));
            healPotion[2] = new HealPotion(new Vector2(2464 + middleOfTile, 2688 + middleOfTile));
            healPotion[3] = new HealPotion(new Vector2(1504 + middleOfTile, 2560 + middleOfTile));
            healPotion[4] = new HealPotion(new Vector2(1760 + middleOfTile, 2688 + middleOfTile));
            healPotion[5] = new HealPotion(new Vector2(1120 + middleOfTile, 2016 + middleOfTile));
            healPotion[6] = new HealPotion(new Vector2(800 + middleOfTile, 2688 + middleOfTile));
            healPotion[7] = new HealPotion(new Vector2(512 + middleOfTile, 2016 + middleOfTile));
            healPotion[8] = new HealPotion(new Vector2(2592 + middleOfTile, 1568 + middleOfTile));
            healPotion[9] = new HealPotion(new Vector2(2752 + middleOfTile, 1152 + middleOfTile));
            healPotion[10] = new HealPotion(new Vector2(1632 + middleOfTile, 1120 + middleOfTile));
            healPotion[11] = new HealPotion(new Vector2(1312 + middleOfTile, 1312 + middleOfTile));
            healPotion[12] = new HealPotion(new Vector2(1696 + middleOfTile, 672 + middleOfTile));
            healPotion[13] = new HealPotion(new Vector2(3136 + middleOfTile, 608 + middleOfTile));
        }

    }

}
