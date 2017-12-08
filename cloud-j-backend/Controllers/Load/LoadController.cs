using System.Web.Http;
using AudioEngine.Config;
using AudioEngine.Domain;

namespace cloud_j_backend.Controllers.Transport
{
    [Route("load")]
    public class LoadController : ApiController
    {
        public IMixer Mixer { get; }

        public LoadController(IMixer mixer)
        {
            Mixer = mixer;
        }

        [HttpPost]
        public IHttpActionResult Pause(int channelId, [FromBody] FileDto fileDto)
        {
            Mixer.Load(channelId, fileDto.FileName);
            return Ok($"loaded {fileDto.FileName}");
        }
    }

    public class FileDto
    {
        public string FileName { get; set; }
    }
}