#region File Description
#endregionusing System;namespace GameStateManagement
{
    public interface IScreenFactory
    {        GameScreen CreateScreen(Type screenType);
    }
}
