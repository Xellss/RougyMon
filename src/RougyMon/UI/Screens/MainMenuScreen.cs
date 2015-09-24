#region File Description

#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using RougyMon;
using RougyMon.UI.Screens;
#endregion

namespace GameStateManagementSample
{

    class MainMenuScreen : MenuScreen
    {
        #region Initialization

        TextMenuEntry name;

        public MainMenuScreen()
            : base("Main Menu")
        {

            name = new TextMenuEntry("Name", Managers.PlayerName);
            MenuEntry playGameMenuEntry = new MenuEntry("Play Game");
            MenuEntry highscoreMenuEntry = new MenuEntry("Highscore");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            MenuEntries.Add(name);
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;

            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            highscoreMenuEntry.Selected += HighscoreMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(highscoreMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }

        #endregion

        #region Handle Input

        void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new GameplayScreen());
        }

        void HighscoreMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new HighscoreMenuScreen(), e.PlayerIndex);
        }



        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
        }

        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to exit this sample?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }

        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }

        public override void Unload()
        {
            Managers.PlayerName = name.Value;
            base.Unload();
        }

        #endregion
    }
}
