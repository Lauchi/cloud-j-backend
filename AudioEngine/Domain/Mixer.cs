using System;
using System.Collections.Generic;
using AudioEngine.Config;
using CSCore;
using CSCore.Codecs;
using CSCore.DSP;
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
            const int mixerSampleRate = 44100; //44.1kHz

            _mixer = new SimpleMixer(2, mixerSampleRate) //output: stereo, 44,1kHz
            {
                FillWithZeros = true,
                DivideResult = true //you may play around with this
            };

            Load(1, FileLocations.File1);
            Load(2, FileLocations.File2);



            //Initialize the soundout with the mixer.
            soundOut = new WasapiOut {Latency = 200}; //better use a quite high latency
            soundOut.Initialize(_mixer.ToWaveSource());
            soundOut.Play();

            Channels[0].Position = 200000;
        }

        public IList<VolumeSource> Volumes { get; } = new List<VolumeSource>();
        public IList<Channel> Channels { get; } = new List<Channel>();

        public IList<BiQuadFilterSource> BqfSources = new List<BiQuadFilterSource>();

        public void Unload(int channelId)
        {
            _mixer.RemoveSource(channelId);
        }

        public void Load(int channelId, string fileName)
        {
            var fileWaveSource = CodecFactory.Instance.GetCodec(fileName);

            const int mixerSampleRate = 44100; //44.1kHz

            VolumeSource volsource;
            BiQuadFilterSource bqfSource;
            _mixer.AddSource(
                fileWaveSource
                    .ChangeSampleRate(mixerSampleRate)
                    .ToStereo()
                    .AppendSource(x => new VolumeSource(x.ToSampleSource()), out volsource)
                    .AppendSource(x => new BiQuadFilterSource(x), out bqfSource));
            volsource.Volume = 0;

            if (Volumes.Count < channelId) Volumes.Add(volsource);
            if (BqfSources.Count < channelId) BqfSources.Add(bqfSource);
            if (Channels.Count < channelId) Channels.Add(new Channel(fileWaveSource));

            Volumes[channelId - 1] = volsource;
            Channels[channelId - 1] = new Channel(fileWaveSource);
            BqfSources[channelId - 1] = bqfSource;
        }

        public void ApplyLowPass(int channelId, int freq)
        {
            //BqfSources[channelId - 1].Filter = new HighpassFilter(44100, 400);
            BqfSources[channelId - 1].Filter = new LowpassFilter(44100, freq);
        }
    }
}