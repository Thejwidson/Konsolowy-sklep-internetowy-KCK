using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SklepInternetowy_WPF.Localization
{
    public class LocalizationManager : INotifyPropertyChanged
    {
        private static LocalizationManager _instance;
        private ResourceManager _resourceManager;
        private CultureInfo _currentCulture;

        public static LocalizationManager Instance => _instance ??= new LocalizationManager();

        private LocalizationManager()
        {
            _resourceManager = new ResourceManager("SklepInternetowy_WPF.Resources.Strings", typeof(LocalizationManager).Assembly);
            _currentCulture = CultureInfo.CurrentCulture;
        }

        public string this[string key]
        {
            get
            {
                var value = _resourceManager.GetString(key, _currentCulture);
                return value ?? $"#{key}#";
            }
        }

        public CultureInfo CurrentCulture
        {
            get => _currentCulture;
            set
            {
                if (_currentCulture != value)
                {
                    _currentCulture = value;
                    OnPropertyChanged(nameof(CurrentCulture));
                    OnPropertyChanged("Item[]");
                }
            }
        }

        public void SetCulture(string cultureName)
        {
            CurrentCulture = new CultureInfo(cultureName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
