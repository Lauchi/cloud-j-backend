using System;
using CSCore.Streams;
using System.Collections.Generic;
using CSCore;

namespace AudioEngine.Domain
{
    public interface IMixer
    {
        IList<VolumeSource> Volumes { get; }
        IList<Channel> Channels { get; }
        void Unload(int channelId);
        void Load(int channelId, string fileName);
    }

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