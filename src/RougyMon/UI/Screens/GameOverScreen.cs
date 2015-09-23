using GameStateManagementSample;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RougyMon
{



    class GameOverScreen : MenuScreen
    {
        Texture2D backgroundTexture;
        public GameOverScreen()
            : base("Game Over")
        {
            MenuEntry playGameMenuEntry = new MenuEntry("Play Game");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            exitMenuEntry.Selected += OnCancel;
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;

            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(exitMenuEntry);

        }

        public override void Activate(bool instancePreserved)
        {
            backgroundTexture = Managers.Content.Load<Texture2D>("Images/Outro.png");

            base.Activate(instancePreserved);
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Viewport viewport = Managers.Graphics.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, fullscreen,
                             new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));

            spriteBatch.End();

            base.Draw(spriteBatch);
        }

        protected override void OnCancel(Microsoft.Xna.Framework.PlayerIndex playerIndex)
        {
            base.OnCancel(playerIndex);
        }

        private void PlayGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
        }
    }
}
