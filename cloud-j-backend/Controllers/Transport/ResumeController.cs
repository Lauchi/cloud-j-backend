using System.Web.Http;
using AudioEngine.Domain;

namespace cloud_j_backend.Controllers.Transport
{
    [Route("resume")]
    public class ResumeController : ApiController
    {
        public IMixer Mixer { get; }

        public ResumeController(IMixer mixer)
        {
            Mixer = mixer;
        }

        [HttpPost]
        public IHttpActionResult Resume(int channelId )
        {
            Mixer.Channels[channelId - 1].Position = 0;
            return Ok(Mixer.Channels[channelId - 1].Position);
        }
    }
}