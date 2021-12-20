using LSL;
using System;
using System.Threading;
using System.Threading.Tasks;

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
                    datum = _isSinus ? (float)(1000.0 * (Math.Sin(_frequencyMultipliers[j] * _phase) + 1.0)) : _random.Next(0, 999);
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
                    datum = _isSinus ? 1000.0 * (Math.Sin(_frequencyMultipliers[j] * _phase) + 1.0) : _random.Next(0, 999);
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
            using StreamInfo simulatorInfo = new(name,
                 type, channels, samplingRate, channelFormatType, uniqueID);
            using StreamInfo markerInfo = new(name + "Mrkrs",
                 "Markers", 1, 0, ChannelFormatType.StringType, uniqueID + "Mrkrs");
            using StreamOutlet simulatorOutlet = new(simulatorInfo, chunkSize);
            using StreamOutlet markerOutlet = new(markerInfo);
            await Task.Run(() => SendData(simulatorOutlet, markerOutlet, channels, chunkSize, channelFormatType, sinusChecked, samplingRate));
        }

        public void StopSender() => _cancellationTokenSource.Cancel();

        private void SendData(StreamOutlet eegOutlet, StreamOutlet markerOutlet, int channels, int chunkSize, ChannelFormatType channelFormatType, bool sinusChecked, double samplingRate)
        {
            //string[] randomMarkers = new string[] { "1", "23skidoo", "lala" };
            string[] oneZeroMarkers = new string[] { "0", "1" };
            int markerIndex = 0;
            string[] markerOut = new string[1];
            Random random = new();
            using DataGenerator dataGenerator = new(channels, chunkSize, sinusChecked, samplingRate);
            while (true)
            {
                if (_cancellationTokenSource.Token.IsCancellationRequested)
                    break;
                switch (channelFormatType)
                {
                    case ChannelFormatType.DoubleSixtyFour:
                        eegOutlet.PushChunk(dataGenerator.GenerateDoubleData());
                        break;
                    case ChannelFormatType.FloatThirtyTwo:
                        eegOutlet.PushChunk(dataGenerator.GenerateFloatData());
                        break;
                    case ChannelFormatType.IntThirtyTwo:
                        eegOutlet.PushChunk(dataGenerator.GenerateIntData());
                        break;
                    case ChannelFormatType.StringType:
                    case ChannelFormatType.IntSixteen:
                    case ChannelFormatType.IntEight:
                    case ChannelFormatType.IntSixtyFour:
                    case ChannelFormatType.UndefinedType:
                    default:
                        break;
                }
                if (random.Next(500) > 495)
                {
                    //markerOut[0] = randomMarkers[random.Next(randomMarkers.Length)];
                    markerOut[0] = oneZeroMarkers[markerIndex];
                    markerIndex = markerIndex == 0 ? 1 : 0;
                    markerOutlet.PushSample(markerOut);
                }
                Thread.Sleep((int)(1000.0 / samplingRate) * chunkSize);
            }
        }
    }
}
