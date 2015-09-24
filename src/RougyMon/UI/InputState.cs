#region File Description
#endregion
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
namespace GameStateManagement
{
    public class InputState
    {
        public const int MaxInputs = 4;
        public readonly KeyboardState[] CurrentKeyboardStates;
        public readonly GamePadState[] CurrentGamePadStates;
        public readonly KeyboardState[] LastKeyboardStates;
        public readonly GamePadState[] LastGamePadStates;
        public readonly bool[] GamePadWasConnected;
        public TouchCollection TouchState;
        public readonly List<GestureSample> Gestures = new List<GestureSample>();
        public InputState()
        {
            CurrentKeyboardStates = new KeyboardState[MaxInputs];
            CurrentGamePadStates = new GamePadState[MaxInputs];
            LastKeyboardStates = new KeyboardState[MaxInputs];
            LastGamePadStates = new GamePadState[MaxInputs];
            GamePadWasConnected = new bool[MaxInputs];
        }
        public void Update()
        {
            for (int i = 0; i < MaxInputs; i++)
            {
                LastKeyboardStates[i] = CurrentKeyboardStates[i];
                LastGamePadStates[i] = CurrentGamePadStates[i];
                CurrentKeyboardStates[i] = Keyboard.GetState((PlayerIndex)i);
                CurrentGamePadStates[i] = GamePad.GetState((PlayerIndex)i);
                if (CurrentGamePadStates[i].IsConnected)
                {
                    GamePadWasConnected[i] = true;
                }
            }
            TouchState = TouchPanel.GetState();
            Gestures.Clear();
            while (TouchPanel.IsGestureAvailable)
            {
                Gestures.Add(TouchPanel.ReadGesture());
            }
        }
        public bool IsKeyPressed(Keys key)
        {
            return CurrentKeyboardStates[0].IsKeyDown(key);
        }
        public bool IsKeyPressed(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                playerIndex = controllingPlayer.Value;
                int i = (int)playerIndex;
                return CurrentKeyboardStates[i].IsKeyDown(key);
            }
            else
            {
                return (IsKeyPressed(key, PlayerIndex.One, out playerIndex) ||
                        IsKeyPressed(key, PlayerIndex.Two, out playerIndex) ||
                        IsKeyPressed(key, PlayerIndex.Three, out playerIndex) ||
                        IsKeyPressed(key, PlayerIndex.Four, out playerIndex));
            }
        }
        public bool IsButtonPressed(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                playerIndex = controllingPlayer.Value;
                int i = (int)playerIndex;
                return CurrentGamePadStates[i].IsButtonDown(button);
            }
            else
            {
                return (IsButtonPressed(button, PlayerIndex.One, out playerIndex) ||
                        IsButtonPressed(button, PlayerIndex.Two, out playerIndex) ||
                        IsButtonPressed(button, PlayerIndex.Three, out playerIndex) ||
                        IsButtonPressed(button, PlayerIndex.Four, out playerIndex));
            }
        }
        public bool IsNewKeyPress(Keys key)
        {
            return (CurrentKeyboardStates[0].IsKeyDown(key) && LastKeyboardStates[0].IsKeyUp(key));
        }
        public bool IsNewKeyPress(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                playerIndex = controllingPlayer.Value;
                int i = (int)playerIndex;
                return (CurrentKeyboardStates[i].IsKeyDown(key) && LastKeyboardStates[i].IsKeyUp(key));
            }
            else
            {
                return (IsNewKeyPress(key, PlayerIndex.One, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Two, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Three, out playerIndex) ||
                        IsNewKeyPress(key, PlayerIndex.Four, out playerIndex));
            }
        }
        public bool IsNewButtonPress(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                playerIndex = controllingPlayer.Value;
                int i = (int)playerIndex;
                return (CurrentGamePadStates[i].IsButtonDown(button) &&
                        LastGamePadStates[i].IsButtonUp(button));
            }
            else
            {
                return (IsNewButtonPress(button, PlayerIndex.One, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Two, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Three, out playerIndex) ||
                        IsNewButtonPress(button, PlayerIndex.Four, out playerIndex));
            }
        }

        public List<Keys> NewPressedKeys()
        {
            List<Keys> keys = new List<Keys>();
            Array arr = Enum.GetValues(typeof(Keys));
            foreach (Keys item in arr)
            {
                if (IsNewKeyPress(item))
                    keys.Add(item);
            }
            return keys;
        }
    }
}
