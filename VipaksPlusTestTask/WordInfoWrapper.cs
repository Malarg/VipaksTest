using System.ComponentModel;

namespace VipaksPlusTestTask
{
    /// <summary>
    /// Класс, храняший информацию о слове. Пока только частота, если потребуется расширение функционала (Подсчет длины слова, наличие в слове определенных букв и т.д.), то расширяться будет этот класс.
    /// </summary>
    public class WordInfoWrapper : INotifyPropertyChanged
    {
        private string _word = string.Empty;
        public string Word
        {
            get { return _word; }
            private set { _word = value; OnPropertyChanged(nameof(Word)); }
        }

        private int _frequency;
        public int Frequency
        {
            get { return _frequency; }
            private set { _frequency = value; OnPropertyChanged(nameof(Frequency)); }
        }

        public WordInfoWrapper(string word)
        {
            Word = word;
            Frequency = 1;
        }

        public int IncFrequency()
        {
            Frequency++;
            return Frequency;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
