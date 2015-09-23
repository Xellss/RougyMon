using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MiniEngine;

namespace RougyMon
{
    class Camera
    {
        public Matrix matrix;

        Vector2 center;
        Viewport viewport;

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
        }

        public void OnUpdate(Vector2 position, int xOffset, int yOffset)
        {

            if (position.X < viewport.Width)
                center.X = viewport.Width;
            else if (position.X > xOffset - (viewport.Width / 1.2f))
                center.X = xOffset - (viewport.Width / 1.2f);
            else center.X = position.X;

            if (position.Y < viewport.Height)
                center.Y = viewport.Height;
            else if (position.Y > yOffset - (viewport.Height / 1.5f))
                center.Y = yOffset - (viewport.Height / 1.5f);
            else center.Y = position.Y;

            matrix = Matrix.CreateTranslation(new Vector3(-position, 0)) * Matrix.CreateScale(2f) * Matrix.CreateTranslation(new Vector3(viewport.Bounds.Size.ToVector2() / 2, 0));

            //matrix = Matrix.CreateTranslation(new Vector3(-center.X + (viewport.Width),
            //                                                -center.Y + (viewport.Height), 0));
        }
    }
}
