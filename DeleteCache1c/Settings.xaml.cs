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
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Button_Delete_Cache_Click(object sender, RoutedEventArgs e)
        {
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
            Task.WaitAll(new Task[] { Task.Delay(1500) });

            MessageBox.Show("Кэш очищен", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void Button_Stop_Apache_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = "/c net stop Apache2.2 & net stop Apache2.4"
            }
    );
        }


        private void Button_Stop_IIS_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = "/c iisreset /stop"
            }
            );
        }

        private void Button_Start_IIS_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = "/c iisreset /start"
            }
);
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }

        private void Button_Start_Apache_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = "/c net start Apache2.2 & net start Apache2.4"
            }
    );
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_home.PlacementTarget = Home;
            popup_home.Placement = PlacementMode.Right;
            popup_home.IsOpen = true;
            Headers.PopupText.Text = "Домой";
        }

        private void Home_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_home.Visibility = Visibility.Collapsed;
            popup_home.IsOpen = false;
        }
    }
}
