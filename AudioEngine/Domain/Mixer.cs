using System;
using System.Collections.Generic;
using System.Linq;
using CSCore;

namespace AudioEngine.Domain
{
    public class Mixer : ISampleSource
    {
        private readonly WaveFormat _waveFormat;
        private readonly IList<ISampleSource> _sampleSources = new List<ISampleSource>();
        private readonly object _lockObj = new object();
        private float[] _mixerBuffer;


       // public IList<Channel> Channels { get; }
        public bool FillWithZeros { get; set; } = true;
        public bool DivideResult { get; set; } = true;

        public Mixer()
        {
            var channels = 2;
            var sampleRate = 44100;
            var bits = 32;
            _waveFormat = new WaveFormat(sampleRate, bits, channels, AudioEncoding.IeeeFloat);
            FillWithZeros = false;
        }

        public void AddSource(ISampleSource source)
        {
            if(source.WaveFormat.Channels != _waveFormat.Channels ||
               source.WaveFormat.SampleRate != _waveFormat.SampleRate)
                throw new ArgumentException("Invalid format.", "source");

            lock (_lockObj)
            {
                _sampleSources.Add(source);
            }
        }

        public void RemoveSource(ISampleSource source)
        {
            lock (_lockObj)
            {
                _sampleSources.Remove(source);
            }
        }

        public int Read(float[] buffer, int offset, int count)
        {
            int numberOfStoredSamples = 0;

            if (count > 0 && _sampleSources.Count > 0)
            {
                lock (_lockObj)
                {
                    _mixerBuffer = _mixerBuffer.CheckBuffer(count);
                    List<int> numberOfReadSamples = new List<int>();
                    for (int m = _sampleSources.Count -1; m >= 0; m--)
                    {
                        var sampleSource = _sampleSources[m];
                        int read = sampleSource.Read(_mixerBuffer, 0, count);
                        for (int i = offset, n = 0; n < read; i++, n++)
                        {
                            if (numberOfStoredSamples <= i)
                                buffer[i] = _mixerBuffer[n];
                            else
                                buffer[i] += _mixerBuffer[n];
                        }
                        if (read > numberOfStoredSamples)
                            numberOfStoredSamples = read;

                        if (read > 0)
                            numberOfReadSamples.Add(read);
                        else
                        {
                            //raise event here
                            RemoveSource(sampleSource); //remove the input to make sure that the event gets only raised once.
                        }
                    }

                    if (DivideResult)
                    {
                        numberOfReadSamples.Sort();
                        int currentOffset = offset;
                        int remainingSources = numberOfReadSamples.Count;

                        foreach (var readSamples in numberOfReadSamples)
                        {
                            if (remainingSources == 0)
                                break;

                            while (currentOffset < offset + readSamples)
                            {
                                buffer[currentOffset] /= remainingSources;
                                buffer[currentOffset] = Math.Max(-1, Math.Min(1, buffer[currentOffset]));
                                currentOffset++;
                            }
                            remainingSources--;
                        }
                    }
                }
            }

            if (FillWithZeros && numberOfStoredSamples != count)
            {
                Array.Clear(
                    buffer,
                    Math.Max(offset + numberOfStoredSamples - 1, 0),
                    count - numberOfStoredSamples);

                return count;
            }

            return numberOfStoredSamples;
        }

        public void Dispose()
        {
            lock (_lockObj)
            {
                foreach (var sampleSource in _sampleSources.ToArray())
                {
                    sampleSource.Dispose();
                    _sampleSources.Remove(sampleSource);
                }
            }
        }

        public bool CanSeek { get; }
        public WaveFormat WaveFormat { get; }
        public long Position { get; set; }
        public long Length { get; }
    }

    public class Channel
    {
        public ISampleSource Track { get; }

        public void SetVolume(Volume volume)
        {

        }

        public void Play()
        {

        }

        public void Pause()
        {

        }
    }

    public class Volume
    {
    }
}