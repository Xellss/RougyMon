using GameStateManagementSample;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RougyMon.UI.Screens
{

    class PlayerWonScreen : MenuScreen
    {
         Texture2D backgroundTexture;


         public PlayerWonScreen(TimeSpan time)
            : base("Game Over")
        {
            MenuEntry playGameMenuEntry = new MenuEntry("Play Game");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            exitMenuEntry.Selected += OnCancel;
            playGameMenuEntry.Selected += PlayGameMenuEntrySelected;

            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(exitMenuEntry);

            FileInfo highscoreFile = new FileInfo("Highscore.txt");
            using (Stream filestream = highscoreFile.Open(FileMode.Append, FileAccess.Write))
            using (StreamWriter write = new StreamWriter(filestream))
            {
                write.WriteLine("{0};{1:c}", Managers.PlayerName, time);
            }
        }

        public override void Activate(bool instancePreserved)
        {
            backgroundTexture = Managers.Content.Load<Texture2D>("Images/Outro.jpg");

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
