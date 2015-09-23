#region File Description

#endregion

#region Using Statements
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.IO;
using System.IO.IsolatedStorage;
using MiniEngine;
using RougyMon;
#endregion

namespace GameStateManagement
{
    public class ScreenManager : GameObject
    {
        #region Fields

        private const string StateFilename = "ScreenManagerState.xml";

        List<GameScreen> screens = new List<GameScreen>();
        List<GameScreen> tempScreensList = new List<GameScreen>();

        InputState input = new InputState();

        SpriteFont font;
        Texture2D blankTexture;

        bool isInitialized;

        bool traceEnabled;
        private Game game;

        public Game Game
        {
            get { return game; }
            set { game = value; }
        }

        #endregion

        #region Properties

        public SpriteFont Font
        {
            get { return font; }
        }

        public bool TraceEnabled
        {
            get { return traceEnabled; }
            set { traceEnabled = value; }
        }

        public Texture2D BlankTexture
        {
            get { return blankTexture; }
        }

        #endregion

        #region Initialization

        public ScreenManager(Game game)
        {
            this.game = game;

            TouchPanel.EnabledGestures = GestureType.None;

            ContentManager content = Managers.Content;

            font = content.Load<SpriteFont>("Fonts/Arial");
            blankTexture = content.Load<Texture2D>("blank");

            foreach (GameScreen screen in screens)
            {
                screen.Activate(false);
            }

            EventManager.OnUpdate += Update;
            EventManager.OnRender += Draw;
            EventManager.OnLateRender += LateDraw;

            isInitialized = true;
        }

        #endregion

        #region Update and Draw

        public void Update(GameTime gameTime)
        {

            input.Update();

            tempScreensList.Clear();

            foreach (GameScreen screen in screens)
                tempScreensList.Add(screen);

            bool otherScreenHasFocus = false;
            bool coveredByOtherScreen = false;

            while (tempScreensList.Count > 0)
            {

                GameScreen screen = tempScreensList[tempScreensList.Count - 1];

                tempScreensList.RemoveAt(tempScreensList.Count - 1);

                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.TransitionOn ||
                    screen.ScreenState == ScreenState.Active)
                {

                    if (!otherScreenHasFocus)
                    {
                        screen.HandleInput(gameTime, input);

                        otherScreenHasFocus = true;
                    }

                    if (!screen.IsPopup)
                        coveredByOtherScreen = true;
                }
            }

            if (traceEnabled)
                TraceScreens();
        }

        void TraceScreens()
        {
            List<string> screenNames = new List<string>();

            foreach (GameScreen screen in screens)
                screenNames.Add(screen.GetType().Name);

            Debug.WriteLine(string.Join(", ", screenNames.ToArray()));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw(spriteBatch);
            }
        }

        public void LateDraw(SpriteBatch spriteBatch)
        {
            foreach (GameScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.LateDraw(spriteBatch);
            }
        }

        #endregion

        #region Public Methods

        public void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer)
        {
            screen.ControllingPlayer = controllingPlayer;
            screen.ScreenManager = this;
            screen.IsExiting = false;

            if (isInitialized)
            {
                screen.Activate(false);
            }

            screens.Add(screen);

            TouchPanel.EnabledGestures = screen.EnabledGestures;
        }

        public void RemoveScreen(GameScreen screen)
        {

            if (isInitialized)
            {
                screen.Unload();
            }

            screens.Remove(screen);
            tempScreensList.Remove(screen);

            if (screens.Count > 0)
            {
                TouchPanel.EnabledGestures = screens[screens.Count - 1].EnabledGestures;
            }
        }
        public void Clear()
        {
            //for (int i = screens.Count - 1; i >= 0; i--)
            //{
            //    RemoveScreen(screens[i]);
            //}
            while (screens.Count > 0)
            {
                RemoveScreen(screens[0]);
            }
        }

        public GameScreen[] GetScreens()
        {
            return screens.ToArray();
        }

        public void FadeBackBufferToBlack(float alpha)
        {

        }

        public void Deactivate()
        {
#if !WINDOWS_PHONE
            return;
#else

            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {

                XDocument doc = new XDocument();
                XElement root = new XElement("ScreenManager");
                doc.Add(root);

                tempScreensList.Clear();
                foreach (GameScreen screen in screens)
                    tempScreensList.Add(screen);

                foreach (GameScreen screen in tempScreensList)
                {

                    if (screen.IsSerializable)
                    {

                        string playerValue = screen.ControllingPlayer.HasValue
                            ? screen.ControllingPlayer.Value.ToString()
                            : "";

                        root.Add(new XElement(
                            "GameScreen",
                            new XAttribute("Type", screen.GetType().AssemblyQualifiedName),
                            new XAttribute("ControllingPlayer", playerValue)));
                    }

                    screen.Deactivate();
                }

                using (IsolatedStorageFileStream stream = storage.CreateFile(StateFilename))
                {
                    doc.Save(stream);
                }
            }
#endif
        }

        public bool Activate(bool instancePreserved)
        {
#if !WINDOWS_PHONE
            return false;
#else

            if (instancePreserved)
            {

                tempScreensList.Clear();

                foreach (GameScreen screen in screens)
                    tempScreensList.Add(screen);

                foreach (GameScreen screen in tempScreensList)
                    screen.Activate(true);
            }

            else
            {

                IScreenFactory screenFactory = Game.Services.GetService(typeof(IScreenFactory)) as IScreenFactory;
                if (screenFactory == null)
                {
                    throw new InvalidOperationException(
                        "Game.Services must contain an IScreenFactory in order to activate the ScreenManager.");
                }

                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {

                    if (!storage.FileExists(StateFilename))
                        return false;

                    using (IsolatedStorageFileStream stream = storage.OpenFile(StateFilename, FileMode.Open))
                    {
                        XDocument doc = XDocument.Load(stream);

                        foreach (XElement screenElem in doc.Root.Elements("GameScreen"))
                        {

                            Type screenType = Type.GetType(screenElem.Attribute("Type").Value);
                            GameScreen screen = screenFactory.CreateScreen(screenType);

                            PlayerIndex? controllingPlayer = screenElem.Attribute("ControllingPlayer").Value != ""
                                ? (PlayerIndex)Enum.Parse(typeof(PlayerIndex), screenElem.Attribute("ControllingPlayer").Value, true)
                                : (PlayerIndex?)null;
                            screen.ControllingPlayer = controllingPlayer;

                            screen.ScreenManager = this;
                            screens.Add(screen);
                            screen.Activate(false);

                            TouchPanel.EnabledGestures = screen.EnabledGestures;
                        }
                    }
                }
            }

            return true;
#endif
        }

        #endregion
    }
}
