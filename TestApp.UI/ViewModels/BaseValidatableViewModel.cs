using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TestApp.UI.ViewModels
{
    public class BaseValidatableViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors => _errors.Count > 0;

        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> Errors
        {
            get => _errors;
            set
            {
                _errors = value;
                OnPropertyChanged();
            }
        }

        protected void SetErrors(string propertyName, List<string> propertyErrors)
        {
            _errors.Remove(propertyName);
            _errors.Add(propertyName, propertyErrors);
            OnPropertyChanged(nameof(Errors));
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void SetRequiredFieldError(string propertyName)
        {
            SetErrors(propertyName, new List<string>()
            {
                "This field is required."
            });
        }

        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return _errors.Values;
            }

            if (_errors.ContainsKey(propertyName))
            {
                return _errors[propertyName];
            }

            return null;
        }

        protected void ClearErrors(string propertyName)
        {
            _errors.Remove(propertyName);
            OnPropertyChanged(nameof(Errors));
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
