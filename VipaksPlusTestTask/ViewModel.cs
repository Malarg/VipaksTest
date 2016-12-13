using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace VipaksPlusTestTask
{
    public class ViewModel : INotifyPropertyChanged
    {
        private object syncObject = new object();
        KeyValuePair<string, WordInfoWrapper> _randomWord = new KeyValuePair<string, WordInfoWrapper>();
        public KeyValuePair<string, WordInfoWrapper> RandomWord
        {
            get { return _randomWord; }
            set { _randomWord = value; OnPropertyChanged(nameof(RandomWord)); }
        }

        private ObservableDictionary<string, WordInfoWrapper> _wordsFrequency = new ObservableDictionary<string, WordInfoWrapper>();
        public ObservableDictionary<string, WordInfoWrapper> WordsFrequency
        {
            get { return _wordsFrequency; }
            set { _wordsFrequency = value; OnPropertyChanged(nameof(WordsFrequency));  }
        }

        public ViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
