using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using Konnectic.Models;
using Konnectic.Services;
using KonnecticTestCoreUtility.Models.Common;

namespace Konnectic.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();

        protected static bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set {
                SetProperty(ref isBusy, value);
                if(value) ErrorInfo = new ErrorInfo();
            }
        }

        protected string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private ErrorInfo _errorInfo;
        public ErrorInfo ErrorInfo
        {
            get
            {
                return _errorInfo;
            }
            set
            {
                SetProperty(ref _errorInfo, value);
            }
        }

        protected string _debugMsg;
        public string DebugMessage
        {
            get
            { return _debugMsg; }
            set
            { SetProperty(ref _debugMsg, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
