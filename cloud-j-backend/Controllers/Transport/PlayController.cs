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

        [HttpGet]
        public IHttpActionResult getPosition(int channelId)
        {
            var sourcePosition = Mixer.Channels[channelId - 1].Source.Position;
            return Ok(sourcePosition);
        }

        [HttpPost]
        public IHttpActionResult Play(int channelId)
        {
            Mixer.Volumes[channelId - 1].Volume = 1f;
            Mixer.Channels[channelId - 1].Play();
            return Ok("play");
        }
    }
}