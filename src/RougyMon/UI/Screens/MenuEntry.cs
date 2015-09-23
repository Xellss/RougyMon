#region File Description
#endregion
#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameStateManagement;
#endregion
namespace GameStateManagementSample
{
    class MenuEntry
    {
        #region Fields   
        string text;
        float selectionFade;
        Vector2 position;     
        #endregion       
        #region Properties     
        public string Text
        {
            get { return text; }
            set { text = value; }
        }   
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        #endregion 
        #region Events  
        public event EventHandler<PlayerIndexEventArgs> Selected;  
        protected internal virtual void OnSelectEntry(PlayerIndex playerIndex)
        {
            if (Selected != null)
                Selected(this, new PlayerIndexEventArgs(playerIndex));
        }
        #endregion   
        #region Initialization  
        public MenuEntry(string text)
        {
            this.text = text;
        }
        #endregion   
        #region Update and Draw 
        public virtual void Update(MenuScreen screen, bool isSelected, GameTime gameTime)
        {
#if WINDOWS_PHONE
            isSelected = false;
#endif           
            float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;  
            if (isSelected)
                selectionFade = Math.Min(selectionFade + fadeSpeed, 1);
            else
                selectionFade = Math.Max(selectionFade - fadeSpeed, 0);
        }   
        public virtual void Draw(MenuScreen screen, bool isSelected, SpriteBatch spriteBatch)
        {
#if WINDOWS_PHONE
            isSelected = false;
#endif       
            Color color = isSelected ? Color.Yellow : Color.White;   
            color *= screen.TransitionAlpha;   
            ScreenManager screenManager = screen.ScreenManager;
            SpriteFont font = screenManager.Font; 
            Vector2 origin = new Vector2(0, font.LineSpacing / 2); 
            spriteBatch.DrawString(font, text, position, color, 0,
                                   origin, 1, SpriteEffects.None, 0);
        }    
        public virtual int GetHeight(MenuScreen screen)
        {
            return screen.ScreenManager.Font.LineSpacing;
        }    
        public virtual int GetWidth(MenuScreen screen)
        {
            return (int)screen.ScreenManager.Font.MeasureString(Text).X;
        }
        #endregion
    }
}
