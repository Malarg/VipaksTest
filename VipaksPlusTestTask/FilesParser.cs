using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace VipaksPlusTestTask
{
    public class FilesParser : INotifyPropertyChanged
    {
        public object syncObject = new object();

        //эти две переменные нужны для промежуточного подсчета, завершена ли обработка файлов
        private int totalFilesCount;
        private int filesParsingFinished;

        /// <summary>
        /// Показывает, завершена ли обработка файлов
        /// </summary>
        public bool IsParsingFinished { get; private set; } = false;

        /// <summary>
        /// Коллекция для хранения частоты слов. 
        /// </summary>
        public ObservableDictionary<string, WordInfoWrapper> WordsFrequency { get; set; } = new ObservableDictionary<string, WordInfoWrapper>();

        /// <summary>
        /// Считает, сколько раз встретилось слово
        /// </summary>
        /// <param name="folderPath"></param>
        public void CalculateFrequencies(string folderPath)
        {
            var filesPath = Directory.GetFiles(folderPath).Where(filePath => filePath.EndsWith(".txt")).ToArray();
            totalFilesCount = filesPath.Count();
            filesParsingFinished = 0;
            IsParsingFinished = false;
            foreach (var path in filesPath)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(ReadFile));
                thread.Start(path);
            }
        }

        /// <summary>
        /// Чтение файла
        /// </summary>
        /// <param name="objPath">Путь к файлу</param>
        private void ReadFile(object objPath)
        {
            var path = objPath as string;
            var words = File.ReadAllText(path, Encoding.Default).Split(' ', '\n', '!', '?', '.', ',', ':', ';', '"');
            lock (syncObject)
            {
                foreach (var word in words.Where(w => !w.Equals(string.Empty)))
                {
                    if (WordsFrequency.ContainsKey(word))
                    {
                        WordsFrequency[word].IncFrequency();
                    }
                    else
                    {
                        if (App.Current != null)                                            //Ветвление написано для тестов
                        {
                            App.Current.Dispatcher.Invoke(delegate
                            {
                                WordsFrequency.Add(word, new WordInfoWrapper(word));
                            });
                        }
                        else
                        {
                            WordsFrequency.Add(word, new WordInfoWrapper(word));
                        }
                    }
                }
                filesParsingFinished++;
                if (filesParsingFinished.Equals(totalFilesCount))
                {
                    IsParsingFinished = true;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
