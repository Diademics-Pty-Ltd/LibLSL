using LSL;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StreamDiagnosisWPF
{
    internal sealed class ViewModel : Notifier
    {
        private IContinuousResolver _continuousResolver;
        private int _selectedStreamInfo;
        private List<IStreamInfo> _streamInfos;
        private bool _clockSync;
        private bool _dejitter;
        private bool _monotonize;
        private bool _threadSafe;

        public ICommand Refresh => new Command(async _ => await RefreshImplAsync());

        public int SelectedStramInfo
        {
            get => _selectedStreamInfo;
            set => Update(ref _selectedStreamInfo, value);
        }
        public List<IStreamInfo> StreamInfos
        {
            get => _streamInfos;
            set => Update(ref _streamInfos, value);
        }
        public bool ClockSync
        {
            get => _clockSync;
            set => Update(ref _clockSync, value);
        }
        public bool Dejitter
        {
            get => _dejitter;
            set => Update(ref _dejitter, value);
        }
        public bool Monotonize
        {
            get => _monotonize;
            set => Update(ref _monotonize, value);
        }
        public bool ThreadSafe
        {
            get => _threadSafe;
            set => Update(ref _threadSafe, value);
        }

        public ViewModel()
        {
            StreamInfos = new();
            _continuousResolver = ContinuousResolverFactory.Create();
            _continuousResolver.OnGotResult = OnGotResults;
        }

        private void OnGotResults(IEnumerable<IStreamInfo> streamInfos)
        {
            StreamInfos = new();
            foreach (IStreamInfo info in streamInfos)
                StreamInfos.Add(info);
        }

        private async Task RefreshImplAsync() => await _continuousResolver.ResultsAsync();
    }
}
