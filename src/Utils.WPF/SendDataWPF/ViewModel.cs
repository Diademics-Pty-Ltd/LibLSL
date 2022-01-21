using LibLSL;
using SendDataWPF.Resources;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace SendDataWPF
{
    internal class BoolRadioConverter : IValueConverter
    {
        public bool Inverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;

            return Inverse ? !boolValue : boolValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolValue = (bool)value;

            return !boolValue ? null : !this.Inverse;
        }
    }

    internal class ChannelFormatItem
    {
        public string Name { get; }
        public ChannelFormatType ChannelFormat { get; }

        public ChannelFormatItem(string name, ChannelFormatType channelFormat)
        {
            Name = name;
            ChannelFormat = channelFormat;
        }
    }
    internal sealed class ViewModel : Notifier
    {
        private string _name = "SomeDevice";
        private string _type = "EEG";
        private int _channels = 8;
        private double _samplingRate = 500.0;
        private ChannelFormatItem _channelFormatItem;
        private string _uniqueID = "QWERTY1234";
        private bool _sinusChecked = true;
        private bool _randomChecked;
        private int _chunkSize = 10;
        private string _sendText = "Send Data";
        private readonly LSLSendSimulatorM _lslSendSimulatorM = new();
        private bool _isIdle = true;
        private string _statusState = StatusMessages.Idle;
        private bool _runProgressBar;

        public ICommand SendPressed => new Command(async _ => await SendPressedImplAsync());
        public string Name
        {
            get => _name;
            set => Update(ref _name, value);
        }

        public string Type
        {
            get => _type;
            set => Update(ref _type, value);
        }

        public string Channels
        {
            get => _channels.ToString(CultureInfo.CurrentCulture);
            set
            {
                if (int.TryParse(value, out _))
                {
                    Update(ref _channels, int.Parse(value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, CultureInfo.CurrentCulture));
                }
                else
                {
                    Update(ref _channels, 8);
                }
            }
        }

        public string SamplingRate
        {
            get => _samplingRate.ToString(CultureInfo.CurrentCulture);
            set
            {
                if (double.TryParse(value, out _))
                {
                    Update(ref _samplingRate, double.Parse(value,
                       CultureInfo.CurrentCulture));
                }
                else
                {
                    Update(ref _samplingRate, 500.0);
                }
            }
        }

        public ChannelFormatItem ChannelFormatItem
        {
            get => _channelFormatItem;
            set => Update(ref _channelFormatItem, value);
        }

        public ObservableCollection<ChannelFormatItem> ChannelFormatItems { get; }

        public string UniqueID
        {
            get => _uniqueID;
            set => Update(ref _uniqueID, value);
        }

        public bool SinusChecked
        {
            get => _sinusChecked;
            set => Update(ref _sinusChecked, value);
        }

        public bool RandomChecked
        {
            get => _randomChecked;
            set => Update(ref _randomChecked, value);
        }

        public string ChunkSize
        {
            get => _chunkSize.ToString(CultureInfo.CurrentCulture);
            set
            {
                if (int.TryParse(value, out _))
                {
                    Update(ref _chunkSize, int.Parse(value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, CultureInfo.CurrentCulture));
                }
                else
                {
                    Update(ref _chunkSize, 10);
                }
            }
        }

        public string SendText
        {
            get => _sendText;
            set => Update(ref _sendText, value);
        }

        public bool IsIdle
        {
            get => _isIdle;
            set => Update(ref _isIdle, value);
        }

        public string StatusState
        {
            get => _statusState;
            set => Update(ref _statusState, value);
        }
        public bool RunProgressBar
        {
            get => _runProgressBar;
            set => Update(ref _runProgressBar, value);
        }

        public ViewModel()
        {
            ChannelFormatItems = new()
            {
                new(ChannelFormatTexts.Float32, ChannelFormatType.FloatThirtyTwo),
                new(ChannelFormatTexts.Double64, ChannelFormatType.DoubleSixtyFour),
                new(ChannelFormatTexts.Int32, ChannelFormatType.IntThirtyTwo)
            };
            _channelFormatItem = ChannelFormatItems[0];
        }

        private async Task SendPressedImplAsync()
        {
            if (string.Compare(SendText, ButtonTexts.SendData, StringComparison.Ordinal) == 0)
            {
                SendText = ButtonTexts.StopSending;
                IsIdle = false;
                StatusState = StatusMessages.Sending;
                RunProgressBar = true;
                await _lslSendSimulatorM.StartSenderAsync(_name, _type, _channels, _samplingRate, ChannelFormatItem.ChannelFormat, _uniqueID, _chunkSize, _sinusChecked);
            }
            else
            {
                StatusState = StatusMessages.Idle;
                RunProgressBar = false;
                _lslSendSimulatorM.StopSender();
                SendText = ButtonTexts.SendData;
                IsIdle = true;
            }
        }


    }
}
