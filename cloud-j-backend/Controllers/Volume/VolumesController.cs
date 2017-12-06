using System.Collections.Generic;
using System.Web.Http;

namespace cloud_j_backend.Controllers.Volume
{
    public class VolumesController : ApiController
    {
        [HttpGet]
        [Route("channels/{channelId}/volume")]
        public IHttpActionResult Get()
        {
            return Ok(new VolumeDto(5.6));
        }

        [HttpPost]
        [Route("channels/{channelId}/volume")]
        public IHttpActionResult ChangeVolume(int channelId, [FromBody]VolumeDto volumeDto)
        {
            return Ok(volumeDto);
        }
    }
}
