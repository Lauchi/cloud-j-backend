using System.Web.Http;

namespace cloud_j_backend.Controllers.Volume
{
    [Route("volume")]
    public class VolumesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(new VolumeDto(5.6));
        }

        [HttpPost]
        public IHttpActionResult ChangeVolume(int channelId, [FromBody]VolumeDto volumeDto)
        {
            return Ok(volumeDto);
        }
    }
}
