#region File Description
#endregion
#region Using Statements
using System;
using Microsoft.Xna.Framework;
#endregion
namespace GameStateManagementSample
{
    class PlayerIndexEventArgs : EventArgs
    {
        public PlayerIndexEventArgs(PlayerIndex playerIndex)
        {
            this.playerIndex = playerIndex;
        }
        public PlayerIndex PlayerIndex
        {
            get { return playerIndex; }
        }
        PlayerIndex playerIndex;
    }
}
