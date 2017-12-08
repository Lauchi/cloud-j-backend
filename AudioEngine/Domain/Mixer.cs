using System.Collections.Generic;
using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;
using CSCore.Streams;

namespace AudioEngine.Domain
{
    public class Mixer : IMixer
    {
        private readonly SimpleMixer _mixer;
        private readonly WasapiOut soundOut;

        public Mixer()
        {
            var fileWaveSource = CodecFactory.Instance.GetCodec(
                @"C:\Users\simon\IdeaProjects\cloud-j-backend\AudioEngine\TestMp3s\Sam Paganini - Rave (Original Mix).mp3");
            var fileWaveSource2 = CodecFactory.Instance.GetCodec(
                @"C:\Users\simon\Desktop\Solomun - Medea (Original Mix) [quality dance music].mp3");

            const int mixerSampleRate = 44100; //44.1kHz

            _mixer = new SimpleMixer(2, mixerSampleRate) //output: stereo, 44,1kHz
            {
                FillWithZeros = true,
                DivideResult = true //you may play around with this
            };

            VolumeSource volumeSource1, volumeSource2;
            //Add any sound track.
            _mixer.AddSource(
                fileWaveSource
                    .ChangeSampleRate(mixerSampleRate)
                    .ToStereo()
                    .AppendSource(x => new VolumeSource(x.ToSampleSource()), out volumeSource1));

            _mixer.AddSource(
                fileWaveSource2
                    .ChangeSampleRate(mixerSampleRate)
                    .ToStereo()
                    .AppendSource(x => new VolumeSource(x.ToSampleSource()), out volumeSource2));

            //Initialize the soundout with the mixer.
            soundOut = new WasapiOut {Latency = 200}; //better use a quite high latency
            soundOut.Initialize(_mixer.ToWaveSource());
            soundOut.Play();

            Volumes = new List<VolumeSource>();
            Volumes.Add(volumeSource1);
            Volumes.Add(volumeSource2);
        }

        public IList<VolumeSource> Volumes { get; }
        public IList<Channel> Channels { get; }
    }
}