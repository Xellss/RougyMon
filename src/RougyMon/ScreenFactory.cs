#region File Description
#endregionusing System;
using GameStateManagement;namespace GameStateManagementSample
{
    public class ScreenFactory : IScreenFactory
    {
        public GameScreen CreateScreen(Type screenType)
        {            return Activator.CreateInstance(screenType) as GameScreen;
        }
    }
}
