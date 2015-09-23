using MiniEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;namespace RougyMon
{
    class End : Component
    {
        public NewTimer timer;
        public End()
        {
            EventManager.OnUpdate += OnUpdate;
        }        private void OnUpdate(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (timer.Time.TotalSeconds >= 0)
            {
                Console.WriteLine("Death");
            }        }        public override void Destroy()
        {
            EventManager.OnUpdate -= OnUpdate;
            base.Destroy();
        }
    }
}
