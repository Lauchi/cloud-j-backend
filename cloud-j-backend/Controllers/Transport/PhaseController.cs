using System.Web.Http;
using AudioEngine.Domain;

namespace cloud_j_backend.Controllers.Transport
{
    [Route("fow")]
         public class ForwardController : ApiController
         {
             public IMixer Mixer { get; }

             public ForwardController(IMixer mixer)
             {
                 Mixer = mixer;
             }

             [HttpPost]
             public IHttpActionResult Play(int channelId )
             {
                 Mixer.Channels[channelId - 1].Position += 2500;
                 return Ok();
             }
         }

    [Route("bac")]
    public class BackwardController : ApiController
    {
        public IMixer Mixer { get; }

        public BackwardController(IMixer mixer)
        {
            Mixer = mixer;
        }

        [HttpPost]
        public IHttpActionResult Play(int channelId)
        {
            Mixer.Channels[channelId - 1].Position -= 2500;
            return Ok();
        }
    }

    public class PhaseDto
    {
        public long phaseValue { get; set; }
    }
}