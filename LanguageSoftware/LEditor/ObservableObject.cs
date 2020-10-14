using System.ComponentModel;
using System.Runtime.CompilerServices;
using LEditor.Annotations;

namespace LEditor
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged<T>(ref T property, T value, 
            [CallerMemberName] string propertyName = null)
        {

            property = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}