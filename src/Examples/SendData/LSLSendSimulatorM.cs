using System;
using LSL;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SendDataWPF
{
    internal class DataGenerator : IDisposable
    {
        private readonly int _channels;
        private readonly int _chunkSize;
        private readonly bool _isSinus;
        private readonly double _samplingRate;
        private readonly int[,] _intData;
        private readonly float[,] _floatData;
        private readonly double[,] _doubleData;
        private double _phase;
        private readonly double _phaseIncrement;
        private readonly double[] _frequencyMultipliers;
        private readonly Random _random;

        public DataGenerator(int channels, int chunkSize, bool isSinus, double samplingRate)
        {
            _channels = channels;
            _chunkSize = chunkSize;
            _isSinus = isSinus;
            _samplingRate = samplingRate;
            _phaseIncrement = 2.0 * Math.PI / samplingRate;
            _intData = new int[chunkSize, channels];
            _floatData = new float[chunkSize, channels];
            _doubleData = new double[chunkSize, channels];
            int frequencyBase = 1;
            _frequencyMultipliers = new double[channels];
            for (int i = 0; i < channels; i++)
            {
                _frequencyMultipliers[i] = frequencyBase * (double)(i + 1);
                while (_frequencyMultipliers[i] >= (samplingRate / 2.2))
                    _frequencyMultipliers[i] -= 1.8 * samplingRate;
            }
            _random = new();
        }

        public int[,] GenerateIntData()
        {
            int datum;
            for (int i = 0; i < _chunkSize; i++)
            {
                for (int j = 0; j < _channels; j++)
                {
                    if (_isSinus)
                        datum = (int)(1000.0 * (Math.Sin(_frequencyMultipliers[j] * _phase) + 1.0));
                    else
                        datum = _random.Next(0, 999);
                    _intData[i, j] = datum;
                }
                _phase += _phaseIncrement;
                if (_phase >= (2.0 * Math.PI))
                    _phase -= 2.0 * Math.PI;
            }

            return _intData;
        }

        public float[,] GenerateFloatData()
        {
            float datum;
            for (int i = 0; i < _chunkSize; i++)
            {
                for (int j = 0; j < _channels; j++)
                {
                    if (_isSinus)
                        datum = (float)(1000.0 * (Math.Sin(_frequencyMultipliers[j] * _phase) + 1.0));
                    else
                        datum = _random.Next(0, 999);
                    _floatData[i, j] = datum;
                }
                _phase += _phaseIncrement;
                if (_phase >= (2.0 * Math.PI))
                    _phase -= 2.0 * Math.PI;
            }
            return _floatData;
        }

        public double[,] GenerateDoubleData()
        {
            double datum;
            for (int i = 0; i < _chunkSize; i++)
            {
                for (int j = 0; j < _channels; j++)
                {
                    if (_isSinus)
                        datum = 1000.0 * (Math.Sin(_frequencyMultipliers[j] * _phase) + 1.0);
                    else
                        datum = _random.Next(0, 999);
                    _doubleData[i, j] = datum;
                }
                _phase += _phaseIncrement;
                if (_phase >= (2.0 * Math.PI))
                    _phase -= 2.0 * Math.PI;
            }
            return _doubleData;
        }

        public void Dispose()
        {

        }
    }
    internal class LSLSendSimulatorM
    {

        private CancellationTokenSource _cancellationTokenSource;

        public LSLSendSimulatorM()
        {

        }

        public async Task StartSenderAsync(string name, string type, int channels, double samplingRate, ChannelFormatType channelFormatType, string uniqueID, int chunkSize, bool sinusChecked)
        {
            _cancellationTokenSource = new();
            using IStreamInfo streamInfo = StreamInfoFactory.Create(name,
                 type, channels, samplingRate, channelFormatType, uniqueID);
            using IStreamOutlet streamOutlet = StreamOutletFactory.Create(streamInfo, chunkSize);
            await Task.Run(() => SendData(streamOutlet, channels, chunkSize, channelFormatType, sinusChecked, samplingRate));
        }

        public void StopSender()
        {
            _cancellationTokenSource.Cancel();
        }

        private void SendData(IStreamOutlet streamOutlet, int channels, int chunkSize, ChannelFormatType channelFormatType, bool sinusChecked, double samplingRate)
        {
            using DataGenerator dataGenerator = new(channels, chunkSize, sinusChecked, samplingRate);
            while (true)
            {
                if (_cancellationTokenSource.Token.IsCancellationRequested)
                    break;
                switch (channelFormatType)
                {
                    case ChannelFormatType.DoubleSixtyFour:
                        _ = streamOutlet.PushChunk(dataGenerator.GenerateDoubleData());
                        break;
                    case ChannelFormatType.FloatThirtyTwo:
                        _ = streamOutlet.PushChunk(dataGenerator.GenerateDoubleData());
                        break;
                    case ChannelFormatType.IntThirtyTwo:
                        _ = streamOutlet.PushChunk(dataGenerator.GenerateDoubleData());
                        break;
                    case ChannelFormatType.StringType:
                    case ChannelFormatType.IntSixteen:
                    case ChannelFormatType.IntEight:
                    case ChannelFormatType.IntSixtyFour:
                    case ChannelFormatType.UndefinedType:
                    default:
                        break;
                }
                Thread.Sleep((int)(1000.0 / samplingRate) * chunkSize);
            }
        }
    }
}
