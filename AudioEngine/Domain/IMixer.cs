using CSCore.Streams;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AudioEngine.Domain
{
    public interface IMixer
    {
        IList<VolumeSource> Volumes { get; }
    }
}