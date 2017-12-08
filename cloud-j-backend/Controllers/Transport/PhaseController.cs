using System.Web.Http;
using AudioEngine.Domain;

namespace cloud_j_backend.Controllers.Transport
{
    [Route("phase")]
    public class PhaseController : ApiController
    {
        public IMixer Mixer { get; }

        public PhaseController(IMixer mixer)
        {
            Mixer = mixer;
        }

        [HttpPost]
        public IHttpActionResult Play(int channelId, [FromBody] PhaseDto phaseDto )
        {
            Mixer.Channels[channelId - 1].Position += phaseDto.phaseValue;
            return Ok(Mixer.Channels[channelId - 1].Position);
        }
    }

    public class PhaseDto
    {
        public long phaseValue { get; set; }
    }
}