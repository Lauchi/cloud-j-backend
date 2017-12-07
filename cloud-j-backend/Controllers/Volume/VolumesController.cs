using System.Web.Http;
using AudioEngine.Domain;

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