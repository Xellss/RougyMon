using System;
using MiniEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RougyMon
{
    class UITimer : GameObject
    {
        public NewTimer timer;
        private UILabel uiTimer;
        private UILabel uiGold;

        public UITimer(NewTimer timer)
        {
            this.timer = timer;


            uiGold = new UILabel(Fonts.ComicSans, new Vector2(1500, 30));
            uiTimer = new UILabel(Fonts.ComicSans, new Vector2(1500, 10));
            uiTimer.TextRenderer.Scale = 0.2f;
            uiGold.TextRenderer.Scale = 0.2f;
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
            uiTimer.TextRenderer.Text = string.Format("Time left: {0:mm\\:ss}", timer.Time);
            uiGold.TextRenderer.Text = string.Format("Gold Amount: {0}", Player.GoldCounter);
        }
    }
}
