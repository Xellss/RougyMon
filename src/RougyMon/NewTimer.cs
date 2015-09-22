using MiniEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RougyMon
{
    class NewTimer : Component
    {
        public TimeSpan Time;
        bool isRunning = false;

        public NewTimer()
        {
            EventManager.OnUpdate += OnUpdate;
        }

        private void OnUpdate(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (isRunning)
            {
                Time = Time.Subtract(gameTime.ElapsedGameTime);
            }
        }

        void Start()
        {
            isRunning = true;
        }

        void Stop()
        {
            isRunning = false;
        }

        public override void Destroy()
        {
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }
    }
}
