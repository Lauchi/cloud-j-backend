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

        public Channel(IWaveSource source)
        {
            Source = source;
        }

        public void ResumeToStart()
        {
            Source.Position = 0;
        }
    }
}