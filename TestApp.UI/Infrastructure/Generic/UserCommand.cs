using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TestApp.UI.Infrastructure.Generic
{
    public class UserCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;
 
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        
        public UserCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }
        
        public bool CanExecute(object parameter)
        {
            if (parameter == null) return false;

            if (!(parameter is T tParameter))
            {
                throw new NotSupportedException("Not supported command parameter type.");
            }

            return this._canExecute == null || this._canExecute(tParameter);
        }
 
        public void Execute(object parameter)
        {

            if (!(parameter is T tParameter))
            {
                throw new NotSupportedException("Not supported command parameter type.");
            }

            this._execute(tParameter);
        }

        public void Execute(T parameter)
        {
            this._execute(parameter);
        }
    }
}
