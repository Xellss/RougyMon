using GameStateManagement;
using GameStateManagementSample;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RougyMon.UI.Screens
{
    class TextMenuEntry : MenuEntry
    {
        private string Prefix;
        public TextMenuEntry(string prefix, string value)
            : base(value)
        {
            Prefix = prefix;
        }
        public override string Text
        {
            get
            {
                return string.Format("{0}: {1}", Prefix, Value);
            }
            set
            {
                this.Value = value;
            }
        }
        public override void OnInput(InputState input)
        {
            Keys[] pressedKeys = input.NewPressedKeys().ToArray();
            for (int i = pressedKeys.Length - 1; i >= 0; i--)
            {
                Keys pressedKey = pressedKeys[i];
                int pressedKeyNum = (int)pressedKey;

                if (pressedKey >= Keys.A && pressedKey <= Keys.Z)
                {
                    Value += (char)((input.IsKeyPressed(Keys.LeftShift) || input.IsKeyPressed(Keys.RightShift)) ? pressedKeyNum : pressedKeyNum + 32);
                }
                else if (pressedKey == Keys.Space) Value += " ";
                else if (pressedKey == Keys.Back && Value.Length > 0) Value = Value.Substring(0, Value.Length - 1);

                //if (input.Mapping.Chars().ContainsKey(pressedKey)) Value += input.IsKeyPress(input.Mapping.Shift) ? input.Mapping.Chars()[pressedKey] : (char)(input.Mapping.Chars()[pressedKey] + 32);
                //else if (input.Mapping.Numbers().ContainsKey(pressedKey)) Value += input.Mapping.Numbers()[pressedKey].ToString();
                //else if (pressedKey == input.Mapping.Hyphen || pressedKey == Key.Minus) Value += input.IsKeyPress(Key.ShiftLeft) ? "_" : "-";
                //else if (pressedKey == input.Mapping.Point) Value += input.IsKeyPress(Key.ShiftLeft) ? ":" : ".";
                //else if (pressedKey == input.Mapping.Comma) Value += input.IsKeyPress(Key.ShiftLeft) ? ";" : ",";
                //else if (pressedKey == input.Mapping.Space) Value += " ";
                //else if (pressedKey == input.Mapping.Back && Value.Length > 0) Value = Value.Substring(0, Value.Length - 1);
            }
            base.OnInput(input);
        }

        public string Value { get; set; }
    }
}
