#region File Description
#endregion
#region Using Statements
using Microsoft.Xna.Framework;
#endregion
namespace GameStateManagementSample
{
    class PauseMenuScreen : MenuScreen
    {
        #region Initialization
        public PauseMenuScreen()
            : base("Paused")
        {
            MenuEntry resumeGameMenuEntry = new MenuEntry("Resume Game");
            MenuEntry quitGameMenuEntry = new MenuEntry("Quit Game");
            resumeGameMenuEntry.Selected += OnCancel;
            quitGameMenuEntry.Selected += QuitGameMenuEntrySelected;
            MenuEntries.Add(resumeGameMenuEntry);
            MenuEntries.Add(quitGameMenuEntry);
        }
        #endregion
        #region Handle Input
        void QuitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            const string message = "Are you sure you want to quit this game?";
            MessageBoxScreen confirmQuitMessageBox = new MessageBoxScreen(message);
            confirmQuitMessageBox.Accepted += ConfirmQuitMessageBoxAccepted;
            ScreenManager.AddScreen(confirmQuitMessageBox, ControllingPlayer);
        }
        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
        }
        #endregion
    }
}
