using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Http;
using AudioEngine.Config;
using AudioEngine.Domain;
using MediaToolkit;
using MediaToolkit.Model;
using YoutubeExtractor;

namespace cloud_j_backend.Controllers.Filters
{
    [Route("applyFilter")]
    public class ApplyFilterController : ApiController
    {
        public IMixer Mixer { get; }

        public ApplyFilterController(IMixer mixer)
        {
            Mixer = mixer;
        }

        [HttpGet]
        public IHttpActionResult Init()
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult ApplyFilter(int channelId, FreqDto freqParam)
        {
            // e^((log(2) + 3 * log(5)) * x  + log(20))
            int maxFreq = 5000;
            int minFreq = 20;
            double freq = Math.Pow(Math.E, ((Math.Log(2) + 3 * Math.Log(5)) * freqParam.FreqValue + Math.Log(20)));
            Mixer.ApplyLowPass(channelId, Convert.ToInt32(freq));
            return Ok($"Set low pass filter to {freq}");
        }

    }
}