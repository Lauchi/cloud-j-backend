using CSCore.Streams;
using System.Collections.Generic;

namespace AudioEngine.Domain
{
    public interface IMixer
    {
        IList<VolumeSource> Volumes { get; }
        IList<Channel> Channels { get; }
    }

    public class Channel
    {
        public void Play()
        {
            throw new System.NotImplementedException();
        }
    }
}