using System.Web.Http;
using AudioEngine.Domain;

namespace cloud_j_backend.Controllers.Transport
{
    [Route("pause")]
    public class PauseController : ApiController
    {
        public IMixer Mixer { get; }

        public PauseController(IMixer mixer)
        {
            Mixer = mixer;
        }

        [HttpPost]
        public IHttpActionResult Pause(int channelId)
        {
            Mixer.Volumes[channelId - 1].Volume = 0f;
            var mixerChannel = Mixer.Channels[channelId - 1];
            mixerChannel.PausePosition = mixerChannel.Position;
            return Ok(mixerChannel.PausePosition);
        }
    }
}