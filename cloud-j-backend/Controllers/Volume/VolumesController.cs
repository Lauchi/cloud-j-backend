using System.Collections.Generic;
using System.Web.Http;

namespace cloud_j_backend.Controllers.Volume
{
    public class VolumesController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        [Route("channels/{channelId}/volume")]
        public void ChangeVolume(int channelId, [FromBody]VolumePost volumePost)
        {

        }
    }
}
