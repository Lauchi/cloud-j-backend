using System.Web.Http;
using AudioEngine.Domain;

namespace cloud_j_backend.Controllers.Transport
{
    [Route("unload")]
    public class UnLoadController : ApiController
    {
        public IMixer Mixer { get; }

        public UnLoadController(IMixer mixer)
        {
            Mixer = mixer;
        }

        [HttpPost]
        public IHttpActionResult Pause(int channelId)
        {
            Mixer.Unload(channelId - 1);
            return Ok("unloaded");
        }
    }
}