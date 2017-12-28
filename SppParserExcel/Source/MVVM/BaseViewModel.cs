using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace SppParserExcel.MVVM
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged([CanBeNull] [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}