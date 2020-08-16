using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace TestApp.UI.Infrastructure
{
    public class UserCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<object, bool> _canExecute;
 
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
 
        public UserCommand(Action execute, Func<object, bool> canExecute = null)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }
        
        public bool CanExecute(object parameter)
        {
            return this._canExecute == null || this._canExecute(parameter);
        }
 
        public void Execute(object parameter)
        {
            this._execute();
        }

    }
}
