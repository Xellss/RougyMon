#region File Description
#endregion
using System;
using GameStateManagement;
namespace GameStateManagementSample
{
    public class ScreenFactory : IScreenFactory
    {
        public GameScreen CreateScreen(Type screenType)
        {
            return Activator.CreateInstance(screenType) as GameScreen;
        }
    }
}
