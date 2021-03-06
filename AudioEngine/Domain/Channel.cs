﻿using CSCore;

namespace AudioEngine.Domain
{
    public class Channel
    {
        private IWaveSource Source { get; }
        public long PausePosition { get; set; }

        public long Position
        {
            get { return Source.Position; }
            set { Source.Position = value; }
        }

        public Channel(IWaveSource source)
        {
            Source = source;
        }

        public void Play()
        {
            Source.Position = PausePosition;
        }
    }
}