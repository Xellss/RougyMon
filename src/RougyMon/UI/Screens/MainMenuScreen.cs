#region File Description

#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using RougyMon.UI.Screens;
#endregion

namespace GameStateManagementSample
{

    class MainMenuScreen : MenuScreen
    {
        #region Initialization

        public MainMenuScreen()
            : base("Main Menu")
        {

            MenuEntry name = new TextMenuEntry("Name", "New Player");
            MenuEntry playGameMenuEntry = new MenuEntry("Play Game");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            MenuEntries.Add(name);
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            exitMenuEntry.Selected += OnCancel;

            MenuEntries.Add(playGameMenuEntry);
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

        #endregion
    }
}
