using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MiniEngine
{
    public static class EventManager
    {
        public delegate void RenderEvent(SpriteBatch spriteBatch);
        public static event RenderEvent OnRender;

        public delegate void UpdateEvent(GameTime gameTime);
        public static event UpdateEvent OnUpdate, OnLateUpdate;

        public static void Render(SpriteBatch spriteBatch)
        {
            if (OnRender != null)
                OnRender(spriteBatch);
        }

        public static void Update(GameTime gameTime)
        {
            if (OnUpdate != null)
                OnUpdate(gameTime);

            if (OnLateUpdate != null)
                OnLateUpdate(gameTime);
        }
    }
}
