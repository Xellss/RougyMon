using Microsoft.Xna.Framework;

namespace MiniEngine
{
    class DestroyAfterDelay : Component
    {
        public delegate void DestroyEvent();
        public event DestroyEvent OnDestroy;

        public float Delay = 0;

        private float timer = 0;

        void Start()
        {
            EventManager.OnUpdate += OnUpdate;
        }

        private void OnUpdate(GameTime gameTime)
        {
            UpdateTimer(gameTime);
        }

        private void UpdateTimer(GameTime gameTime)
        {
            if (Delay == 0)
                return;

            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer >= Delay)
            {
                if (OnDestroy != null)
                    OnDestroy();

                GameObject.Destroy();
            }
        }

        public override void Destroy()
        {
            EventManager.OnUpdate -= OnUpdate;

            base.Destroy();
        }
    }
}
