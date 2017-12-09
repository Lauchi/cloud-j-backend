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

        [HttpPost]
        public IHttpActionResult ChangeVolume(int channelId, [FromUri] float vol)
        {
            Mixer.Volumes[channelId - 1].Volume = vol;
            return Ok();
        }
    }
}