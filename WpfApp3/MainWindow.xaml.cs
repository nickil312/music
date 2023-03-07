using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static string[] files;
        public static int n = 0;
        public static int startORstop;
        public static bool a = true;
        public static bool b = true;
        public static string[] files2;
        //public static Thread thread;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //CommonOpenFileDialog dialog = new CommonOpenFileDialog { IsFolderPicker = true };
            //dialog.Filters.Add(new CommonFileDialogFilter("MP3 Files", "*.mp3"));

            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            //dialog.Filters.Add(new CommonFileDialogFilter("MP3 Files", "*.mp3"));
            //dialog.AutoUpgradeEnabled = false;
            var result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                //openFileDialog.Filter = "txt Files|.txt|cs Files|.cs";
                files = Directory.GetFiles(dialog.FileName, "*.mp3");
                //files2 = Directory.GetFiles(dialog.FileName, "*.mp3");
                foreach (string file in files)
                {
                    ListOfSongs.Items.Add(file);
                    /*string b2 = Path.GetExtension(file);
                    if (b2 == ".mp3")
                    {
                        ListOfSongs.Items.Add(file);
                        files2.Append(file);
                    }
                    */
                    /*if (file.EndsWith(".mp3"))
                    {*/
                    //ListOfSongs.Items.Add(file);
                    /*files.Append(file);
                     * /*string b2 = Path.GetExtension(file);
                    if (b2 == ".mp3")
                    {
                        ListOfSongs.Items.Add(file);
                        files2.Append(file);
                    }
                    */
                    /*if (file.EndsWith(".mp3"))
                    {*/
                    //ListOfSongs.Items.Add(file);
                    /*files.Append(file);
                }*/
                /*}
                */
                }
                media.Source = new Uri(files[n]);
                media.Volume = Volume.Value = 0.1;
                BigSlider.Value = 0;
                media.Play();

                Start.Visibility = Visibility.Hidden;
                Stop.Visibility = Visibility.Visible;
                /*Thread secondthread = new Thread(SecondThread);
                secondthread.Start();*/
                /*SecondThread();*/
                Thread thread = new Thread(SecondThread);
                thread.Start();
            }
            else
            {
                MessageBox.Show("Вы не выбрали папку", "Ошибка");
            }
        }

        private void StartANDStop(object sender, RoutedEventArgs e)
        {
            media.Play();
            //Thread thread = new Thread(SecondThread);
            a = false;
            //b = true;
            //thread.Start();
            Start.Visibility = Visibility.Hidden;
            Stop.Visibility = Visibility.Visible;

        }
        private void StopANDStart(object sender, RoutedEventArgs e)
        {
            media.Pause();
            //Thread thread = new Thread(SecondThread);
            a = false;
            //b = false;
            //thread.Abort();
            Stop.Visibility = Visibility.Hidden;
            Start.Visibility = Visibility.Visible;

        }
        private void Back(object sender, RoutedEventArgs e)
        {
            if (n == 0)
            {
                n = 3;
                BigSlider.Value = 0;
                media.Source = new Uri(files[n]);
                // media.Play();

            }
            else
            {
                n--;
                BigSlider.Value = 0;
                media.Source = new Uri(files[n]);
                // media.Play();

            }
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            //files.Length - 1 == n
            //Подсчет идет с 0 и по этому n == 3
            if (n == 3)
            {
                n = 0;
                BigSlider.Value = 0;
                media.Source = new Uri(files[n]);
                //media.Play();

            }
            else
            {
                BigSlider.Value = 0;
                media.Source = new Uri(files[n + 1]);
                n++;
                //media.Play();

            }


        }

        private void Repeat(object sender, RoutedEventArgs e)
        {

        }

        private void Random(object sender, RoutedEventArgs e)
        {

        }
        private void VolumeVALUE(object sender, RoutedEventArgs e)
        {
            media.Volume = Volume.Value;
        }
        private void MediaOpener(object sender, RoutedEventArgs e)
        {

            secondtime.Content = media.NaturalDuration.TimeSpan.ToString().Substring(0, 8);
            BigSlider.Maximum = media.NaturalDuration.TimeSpan.Ticks;
        }
        private void BigSliderNewVAlUE(object sender, RoutedEventArgs e)
        {
           
            media.Position = TimeSpan.FromSeconds(BigSlider.Value);
            
        }
        private void SecondThread()
        {

            /*Thread secondthread = new Thread(_ =>
            {*/
            while (a == true)
            {
                a = false;
                /*while (b == true)
                {*/
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {



                        fisrttime.Content = media.Position.ToString().Substring(0, 8);

                        BigSlider.Value = media.Position.TotalSeconds;
                        

                    }));

                    /* });*/
                /*}*/

            }
        }

            private void MediaEnd(object sender, RoutedEventArgs e)
            {
                if (n == 3)
                {
                    a = false;
                    BigSlider.Value = 0;
                    n = 0;
                    media.Source = new Uri(files[n]);
                    //media.Play();

                }
                else
                {
                    a = false;
                    BigSlider.Value = 0;
                    media.Source = new Uri(files[n + 1]);
                    n++;
                    //media.Play();

                }
            }
        }
    }
