using CSCore.Streams;
using System.Collections.Generic;
using CSCore;

namespace AudioEngine.Domain
{
    public interface IMixer
    {
        IList<VolumeSource> Volumes { get; }
        IList<Channel> Channels { get; }
    }

    public class Channel
    {
        public IWaveSource Source { get; }
        public long PausePosition { get; set; }

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