using System;
using MiniEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RougyMon
{
    class UITimer : GameObject
    {
        private Player player;
        private UILabel uiTimer;

        public UITimer(Player player)
        {
            this.player = player;

            uiTimer = new UILabel(Fonts.ComicSans, new Vector2(1500, 10));
            uiTimer.TextRenderer.Scale = 0.2f;
            UpdateUILabel();
            EventManager.OnUpdate += OnUpdate;
        }

        void OnUpdate(GameTime gameTime)
        {
            UpdateUILabel();
        }

        public override void Destroy()
        {
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }

        private void UpdateUILabel()
        {
            uiTimer.TextRenderer.Text = string.Format("Time left: {0:mm\\:ss}",player.Timer.Time);
            
        }
    }
}
