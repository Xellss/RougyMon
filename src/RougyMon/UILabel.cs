using System;
using MiniEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RougyMon
{
    public class UILabel : GameObject
    {
        public TextRenderer TextRenderer { get; private set; }
        public Transform Transform { get; private set; }

        public UILabel(SpriteFont font, Vector2 position)
        {
            Transform = AddComponent<Transform>();
            Transform.Position = position;

            TextRenderer = AddComponent<TextRenderer>();
            TextRenderer.Font = font;
        }
    }
}
