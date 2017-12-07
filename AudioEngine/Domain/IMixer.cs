using CSCore.Streams;
using System.Collections.Generic;

namespace AudioEngine.Domain
{
    public interface IMixer
    {
        IList<VolumeSource> Volumes { get; }
    }
}