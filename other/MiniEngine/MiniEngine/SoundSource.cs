using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;


namespace MiniEngine
{
    public class SoundSource : Component
    {
        public SoundEffect SoundEffect
        {
            set
            {
                soundInstance = value.CreateInstance();
            }
        }

        public float Pitch
        {
            get { return soundInstance.Pitch; }
            set { soundInstance.Pitch = value; }
        }

        public float Volume
        {
            get { return soundInstance.Volume; }
            set { soundInstance.Volume = value; }
        }

        private SoundEffectInstance soundInstance;

        public void Play()
        {
            soundInstance.Play();
        }

        public void Stop()
        {
            soundInstance.Stop();
        }
    }
}
