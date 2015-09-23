#region File Description






#endregion

#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagement;
using RougyMon;
#endregion

namespace GameStateManagementSample
{














    class LoadingScreen : GameScreen
    {
        #region Fields

        bool loadingIsSlow;
        bool otherScreensAreGone;

        GameScreen[] screensToLoad;

        #endregion

        #region Initialization




        private LoadingScreen(ScreenManager screenManager, bool loadingIsSlow,
                              GameScreen[] screensToLoad)
        {
            this.loadingIsSlow = loadingIsSlow;
            this.screensToLoad = screensToLoad;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
        }



        public static void Load(ScreenManager screenManager, bool loadingIsSlow,
                                PlayerIndex? controllingPlayer,
                                params GameScreen[] screensToLoad)
        {

            foreach (GameScreen screen in screenManager.GetScreens())
                screen.ExitScreen();

            LoadingScreen loadingScreen = new LoadingScreen(screenManager,
                                                            loadingIsSlow,
                                                            screensToLoad);

            screenManager.AddScreen(loadingScreen, controllingPlayer);
        }


        #endregion

        #region Update and Draw



        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);


            if (otherScreensAreGone)
            {
                ScreenManager.RemoveScreen(this);

                foreach (GameScreen screen in screensToLoad)
                {
                    if (screen != null)
                    {
                        ScreenManager.AddScreen(screen, ControllingPlayer);
                    }
                }




            }
        }



        public override void Draw(SpriteBatch spriteBatch)
        {





            if ((ScreenState == ScreenState.Active) &&
                (ScreenManager.GetScreens().Length == 1))
            {
                otherScreensAreGone = true;
            }






            if (loadingIsSlow)
            {
                SpriteFont font = ScreenManager.Font;

                const string message = "Loading...";

                Viewport viewport = Managers.Graphics.GraphicsDevice.Viewport;
                Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
                Vector2 textSize = font.MeasureString(message);
                Vector2 textPosition = (viewportSize - textSize) / 2;

                Color color = Color.White * TransitionAlpha;

                spriteBatch.DrawString(font, message, textPosition, color);
            }
        }


        #endregion
    }
}
