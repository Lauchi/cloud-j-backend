using System;
using CSCore.Streams;
using System.Collections.Generic;

namespace AudioEngine.Domain
{
    public interface IMixer
    {
        IList<VolumeSource> Volumes { get; }
        IList<Channel> Channels { get; }
        void Unload(int channelId);
        void Load(int channelId, string fileName);
        void ApplyLowPass(int channelId, int freq);
    }
}