using System.Web.Http;
using AudioEngine.Domain;
using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using CSCore.Streams;

namespace cloud_j_backend.Controllers.Volume
{
    [Route("volume")]
    public class VolumesController : ApiController
    {
        private readonly SimpleMixer _mixer;
        private VolumeSource _volumeSource1;

        public VolumesController()
        {
            var fileWaveSource = CodecFactory.Instance.GetCodec(@"C:\Users\Simon\source\repos\cloud-j-backend\AudioEngine\TestMp3s\Sam Paganini - Rave (Original Mix).mp3");

            const int mixerSampleRate = 44100; //44.1kHz

            _mixer = new SimpleMixer(2, mixerSampleRate) //output: stereo, 44,1kHz
            {
                FillWithZeros = true,
                DivideResult = true //you may play around with this
            };

            //Add any sound track.
            _mixer.AddSource(
                fileWaveSource
                    .ChangeSampleRate(mixerSampleRate)
                    .ToStereo()
                    .AppendSource(x => new VolumeSource(x.ToSampleSource()), out _volumeSource1));

            //Initialize the soundout with the mixer.
            var soundOut = new WasapiOut {Latency = 200}; //better use a quite high latency
            soundOut.Initialize(_mixer.ToWaveSource());
            soundOut.Play();
        }

        [HttpGet]
        public IHttpActionResult Init()
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult ChangeVolume(int channelId, [FromBody] VolumeDto volumeDto)
        {
            _volumeSource1.Volume = volumeDto.VolumeValue;
            return Ok(volumeDto);
        }
    }
}