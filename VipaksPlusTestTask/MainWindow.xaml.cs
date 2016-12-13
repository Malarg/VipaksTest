using System;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Threading;
using Timer = System.Threading.Timer;

namespace VipaksPlusTestTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //Переменная для сохранения потокобезопасности
        private object syncObject = new object();

        //ссылка на вью модель для более удобного написания кода
        private ViewModel _viewModel;

        //Класс с основной логикой
        FilesParser _parser = new FilesParser();

        //Таймер, который обновляет случайное слово
        private Timer RndWordShowingTimer;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = this.DataContext as ViewModel;
            BindingOperations.EnableCollectionSynchronization(_viewModel.WordsFrequency, DG.ItemsSource);
            Thread timerThread = new Thread(new ThreadStart(StartTimer));
            timerThread.Start();
        }

        private void StartTimer()
        {
            var callBack = new TimerCallback(ShowRndWordFrequency);
            RndWordShowingTimer = new Timer(callBack, null, 0, 250);
        }

        private void ShowRndWordFrequency(object obj)
        {
            if (_viewModel.WordsFrequency.Count != 0)
            {
                var rnd = new Random();
                var nextRnd = rnd.Next(_viewModel.WordsFrequency.Count - 1);
                lock(syncObject)
                {
                    _viewModel.RandomWord = _viewModel.WordsFrequency.ToDictionary(l => l.Key, l => l.Value).ElementAt(nextRnd);
                }
            }
        }

        private void Compete_MenuItemClick(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            syncObject = _parser.syncObject;
            _viewModel.WordsFrequency = _parser.WordsFrequency;
            if (dialog.SelectedPath != string.Empty)
            {
                _parser.CalculateFrequencies(dialog.SelectedPath);
            }
        }

        private void Clear_MenuItemClick(object sender, RoutedEventArgs e)
        {
            _viewModel.WordsFrequency.Clear();
            DG.ItemsSource = null;
            DG.ItemsSource = _viewModel.WordsFrequency;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_parser.IsParsingFinished)
            {
                System.Windows.MessageBox.Show("Пожалуйста, дождитесь окончания работы программы", "Уведомление");
                e.Cancel = true;
            }
        }
    }
}
