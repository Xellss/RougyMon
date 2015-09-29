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
        // hi :)
        public TimeSpan Time;
        bool isRunning = false;
        private bool chrisC = false;
        private bool chrisH = false;
        private bool chrisR = false;
        private bool chrisI = false;
        public bool chrisS = false;

        public void OnUpdate(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (isRunning)
            {
                haxx();
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
        public void haxx()
        {

            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                chrisC = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.H) && chrisC)
            {
                chrisH = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.H) && chrisC && chrisH)
            {
                chrisR = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.H) && chrisC && chrisH && chrisR)
            {
                chrisI = true;
            } if (Keyboard.GetState().IsKeyDown(Keys.H) && chrisC && chrisH && chrisI)
            {
                chrisS = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D1) && chrisS)
            {
                Time = Time.Subtract(new TimeSpan(0, 0, 20));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2) && chrisS)
            {
                Time = Time.Add(new TimeSpan(0, 0, 20));
            }
        }
    }
}
