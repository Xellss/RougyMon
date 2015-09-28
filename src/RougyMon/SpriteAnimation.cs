using System;
using System.Xml;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RougyMon
{
    class SpriteAnimation
    {
        public string Name { get; private set; }
        public int FrameDelay { get; set; }
        public Texture2D Image { get; private set; }
        public SpriteAnimationFrame CurrentFrame { get; private set; }

        private string currentAnimationName;
        private int currentFrameCount;
        private List<SpriteAnimationFrame> allFrames = new List<SpriteAnimationFrame>();
        private List<SpriteAnimationFrame> currentFrames = new List<SpriteAnimationFrame>();
        private float timer = 0f;

        public SpriteAnimation(string name, Texture2D atlas, string dataPath)
        {
            Name = name;
            Image = atlas;
            LoadFrames(dataPath);
        }

        public void UpdateAnimation(GameTime gameTime)
        {
            UpdateAnimationFrame(gameTime);
        }

        public void PlayAnimation(string name)
        {
            if (currentAnimationName == name)
                return;

            currentFrames = allFrames.FindAll((SpriteAnimationFrame frame) => frame.Name.Contains(name));
            currentAnimationName = name;
            currentFrameCount = 0;

            if (currentFrames.Count == 0)
                throw new Exception(string.Format("No animationFrames found for {0}", name));

            CurrentFrame = currentFrames[0];
        }

        private void UpdateAnimationFrame(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            if (timer >= FrameDelay)
            {
                timer = 0;
                if (currentFrameCount < currentFrames.Count - 1)
                    currentFrameCount++;
                else
                    currentFrameCount = 0;
                CurrentFrame = currentFrames[currentFrameCount];
            }
        }

        private void LoadFrames(string path)
        {
            XmlReader xmlReader = XmlReader.Create(path);
            while (xmlReader.Read())
            {
                if (xmlReader.IsStartElement("sprite"))
                {
                    string name = xmlReader.GetAttribute("n");
                    if (name.Contains(Name))
                    {
                        SpriteAnimationFrame animationFrame = new SpriteAnimationFrame();
                        animationFrame.Name = name;
                        animationFrame.Bounds.X = Convert.ToInt32(xmlReader.GetAttribute("x"));
                        animationFrame.Bounds.Y = Convert.ToInt32(xmlReader.GetAttribute("y"));
                        animationFrame.Bounds.Width = Convert.ToInt32(xmlReader.GetAttribute("w"));
                        animationFrame.Bounds.Height = Convert.ToInt32(xmlReader.GetAttribute("h"));

                        allFrames.Add(animationFrame);
                    }
                }
            }
        }
    }
}
