using MiniEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RougyMon
{
   public class NewTimer 
    {
        public TimeSpan Time;
        bool isRunning = false;


        public void OnUpdate(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (isRunning)
            {
                Time = Time.Subtract(gameTime.ElapsedGameTime);
            }
        }

        public void Start()
        {
            isRunning = true;
        }

        public void Stop()
        {
            isRunning = false;
        }

    }
}
