#region File Description
#endregion
using System;
namespace GameStateManagement
{
    public interface IScreenFactory
    { 
        GameScreen CreateScreen(Type screenType);
    }
}
