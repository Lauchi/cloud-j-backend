using System.Collections.Generic;
using System.Web.Http;

namespace cloud_j_backend.Controllers.Volume
{
    public class VolumesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public void ChangeVolume(int channelId, [FromBody]VolumePost volumePost)
        {

        }
    }
}
