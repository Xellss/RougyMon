//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.Xna.Framework;
//using MiniEngine;

//namespace RougyMon
//{
//    public class Timer : Component
//    {
//        public float Seconds = 60;
//        public float Minutes = 0;

//        public float TimerSeconds = 0;
//        public float TimerMinutes = 0;

//        public bool StartTimer = false;
//        public bool TimerEnd = false;

//        Transform transform;

//        void Start()
//        {
//            transform = GameObject.GetComponent<Transform>();
//            if (transform == null)
//                throw new Exception("GameObject needs a Transform component");

//            EventManager.OnUpdate += OnUpdate;
//        }

//        private void OnUpdate(GameTime gameTime)
//        {
//            runTimer(gameTime);
//        }

//        private void runTimer(GameTime gameTime)
//        {

//            if (StartTimer)
//            {
//                TimerSeconds = Seconds - (float)gameTime.TotalGameTime.Seconds;

//                if (Minutes > 0)
//                    TimerMinutes = (Minutes -1) - (float)gameTime.TotalGameTime.Minutes;
//            }

//            if (TimerSeconds <= 0 && TimerMinutes <= 0)
//            {
//                StartTimer = false;
//                TimerEnd = true;
//            }
//        }
//        public override void Destroy()
//        {
//            EventManager.OnUpdate -= OnUpdate;

//            base.Destroy();
//        }
//    }
//}
