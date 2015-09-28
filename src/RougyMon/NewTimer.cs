using MiniEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;


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

                if (Keyboard.GetState().IsKeyDown(Keys.I))
                {
                    Time = Time.Subtract(new TimeSpan(0, 0, 20));                    
                }
                if (Keyboard.GetState().IsKeyDown(Keys.K))
                {
                    Time = Time.Add(new TimeSpan(0, 0, 20));
                }
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
