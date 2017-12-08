using System.Web.Http;
using AudioEngine.Domain;

namespace cloud_j_backend.Controllers.Transport
{
    [Route("play")]
    public class PlayController : ApiController
    {
        public IMixer Mixer { get; }

        public PlayController(IMixer mixer)
        {
            Mixer = mixer;
        }

        [HttpPost]
        public IHttpActionResult ChangeVolume(int channelId)
        {
            Mixer.Channels[channelId - 1].Play();
            return Ok("play");
        }
    }
}