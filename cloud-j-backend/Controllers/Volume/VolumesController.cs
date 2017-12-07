using System.Web.Http;
using AudioEngine.Domain;
using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using CSCore.Streams;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cloud_j_backend.Controllers.Volume
{
    [Route("volume")]
    public class VolumesController : ApiController
    {
        public IMixer Mixer { get; }

        public VolumesController(IMixer mixer)
        {
            Mixer = mixer;
        }

        [HttpGet]
        public IHttpActionResult Init()
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult ChangeVolume(int channelId, [FromBody] VolumeDto volumeDto)
        {
            Mixer.Volumes[channelId - 1].Volume = volumeDto.VolumeValue;
            return Ok(volumeDto);
        }
    }
}