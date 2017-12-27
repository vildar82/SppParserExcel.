using System;
using System.Windows.Input;

namespace SppParserExcel.MVVM
{
    public class Commamd : ICommand
    {
        private readonly Action action;

        public Commamd(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }

        public event EventHandler CanExecuteChanged;
    }
}