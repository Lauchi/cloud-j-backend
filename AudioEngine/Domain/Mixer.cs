using System.Collections.Generic;
using CSCore;
using CSCore.SoundOut;
using CSCore.Streams;
using CSCore.Codecs;

namespace AudioEngine.Domain
{
    public class Mixer : IMixer
    {
        private SimpleMixer _mixer;
        private WasapiOut soundOut;

        public IList<VolumeSource> Volumes { get; }

        public Mixer()
        {
            var fileWaveSource = CodecFactory.Instance.GetCodec(@"C:\Users\simon\IdeaProjects\cloud-j-backend\AudioEngine\TestMp3s\Sam Paganini - Rave (Original Mix).mp3");

            const int mixerSampleRate = 44100; //44.1kHz

            _mixer = new SimpleMixer(2, mixerSampleRate) //output: stereo, 44,1kHz
            {
                FillWithZeros = true,
                DivideResult = true //you may play around with this
            };

            VolumeSource volumeSource1;
            //Add any sound track.
            _mixer.AddSource(
                fileWaveSource
                    .ChangeSampleRate(mixerSampleRate)
                    .ToStereo()
                    .AppendSource(x => new VolumeSource(x.ToSampleSource()), out volumeSource1));

            //Initialize the soundout with the mixer.
            soundOut = new WasapiOut { Latency = 200 }; //better use a quite high latency
            soundOut.Initialize(_mixer.ToWaveSource());
            soundOut.Play();

            Volumes = new List<VolumeSource>();
            Volumes.Add(volumeSource1);
        }
    }
}