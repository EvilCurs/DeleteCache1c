using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Windows.Controls.Primitives;

namespace DeleteCache1c
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        // Кнопка "Старт", запустить очистку кеша с остановками служб //
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = "/c net stop Apache2.2 & net stop Apache2.4 & " +
                "@echo Остановка  IIS & " +
                "iisreset /stop & " +
                "@echo IIS остановлен & " +
                "echo - & " +
                "@echo Процесс очистки кэша & " +
                "timeout 6 & " +
                "net start Apache2.2 & net start Apache2.4 & " +
                "@echo Запуск IIS & " +
                "iisreset /start & " +
                "@echo IIS запущен & " +
                "timeout 5"
            }
                );

            Task.WaitAll(new Task[] { Task.Delay(8500) });


            var usersDir = new DirectoryInfo("C:\\Users");

            foreach (var userDir in usersDir.GetDirectories())
            {
                var dir1s = new DirectoryInfo(Path.Combine(userDir.FullName, "AppData/Roaming/1C/1Cv8"));

                if (dir1s.Exists)
                {
                    var subdirs = dir1s.GetDirectories("????????-????-????-????-*");

                    foreach (var subdir in subdirs)
                    {
                        Console.WriteLine(subdir);
                        subdir.Delete(true);
                    }
                }
            }

            var usersDirSecond = new DirectoryInfo("C:\\Users");

            foreach (var userDirSecond in usersDirSecond.GetDirectories())
            {
                var dir1sSecond = new DirectoryInfo(Path.Combine(userDirSecond.FullName, "AppData/Local/1C/1Cv8"));

                if (dir1sSecond.Exists)
                {
                    var subdirsSecond = dir1sSecond.GetDirectories("????????-????-????-????-*");

                    foreach (var subdirSecond in subdirsSecond)
                    {
                        Console.WriteLine(subdirSecond);
                        subdirSecond.Delete(true);
                    }
                }
            }

            Task.WaitAll(new Task[] { Task.Delay(11000) });

            MessageBox.Show("Кэш очищен", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Settings_Click(object sender, RoutedEventArgs e)
        {
            var Settings = new Settings();
            Settings.Show();
            this.Close();
        }


        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = Start;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Очистить кэш\nс остановкой служб";
        }

        private void Start_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void Button_Settings_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = Button_Settings;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Расширенные\nнастройки";
        }

        private void Button_Settings_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }
    }
}
