#region File Description
#endregion
using System;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace GameStateManagementSample
{
    class BooleanButton : Button
    {
        private string option;
        private bool value;
        public BooleanButton(string option, bool value)
            : base(option)
        {
            this.option = option;
            this.value = value;
            GenerateText();
        }
        protected override void OnTapped()
        {
            value = !value;
            GenerateText();
            base.OnTapped();
        }
        private void GenerateText()
        {
            Text = string.Format("{0}: {1}", option, value ? "On" : "Off");
        }
    }
    class Button
    {
        public string Text = "Button";
        public Vector2 Position = Vector2.Zero;
        public Vector2 Size = new Vector2(250, 75);
        public int BorderThickness = 4;
        public Color BorderColor = new Color(200, 200, 200);
        public Color FillColor = new Color(100, 100, 100) * .75f;
        public Color TextColor = Color.White;
        public float Alpha = 0f;
        public event EventHandler<EventArgs> Tapped;
        public Button(string text)
        {
            Text = text;
        }
        protected virtual void OnTapped()
        {
            if (Tapped != null)
                Tapped(this, EventArgs.Empty);
        }
        public bool HandleTap(Vector2 tap)
        {
            if (tap.X >= Position.X &&
                tap.Y >= Position.Y &&
                tap.X <= Position.X + Size.X &&
                tap.Y <= Position.Y + Size.Y)
            {
                OnTapped();
                return true;
            }
            return false;
        }
    }
}
