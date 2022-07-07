using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MouseKeyboardWPF
{
    internal class ViewModel : INotifyPropertyChanged
    {

        private string _linkKeyboardEventsContent = "Link Keyboard";
        private string _linkMouseEventsContent = "Link Mouse";

        public ICommand LinkKeyboardEvents => new Command(_ => OnLinkKeyboardEvents());
        public ICommand LinkMouseEvents => new Command(_ => OnLinkMouseEvents());

        public string LinkKeyboardEventsContent
        {
            get => _linkKeyboardEventsContent;
            set => Update(ref _linkKeyboardEventsContent, value);
        }

        public string LinkMouseEventsContent
        {
            get => _linkMouseEventsContent;
            set => Update(ref _linkMouseEventsContent, value);
        }


        private void OnLinkKeyboardEvents()
        {

        }

        private void OnLinkMouseEvents()
        {

        }

        #region INotifyPropertyChaged overrides
        public event PropertyChangedEventHandler? PropertyChanged = null;

        protected void Update<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return;

            field = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
