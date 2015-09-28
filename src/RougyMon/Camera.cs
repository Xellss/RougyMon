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


        Viewport viewport;

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
        }

        public void OnUpdate(Vector2 position, int xOffset, int yOffset)
        {

            matrix = Matrix.CreateTranslation(new Vector3(-position, 0)) * Matrix.CreateScale(1.5f) * Matrix.CreateTranslation(new Vector3(viewport.Bounds.Size.ToVector2() / 2, 0));

        }
    }
}
