using BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace Views.WMovie
{
    /// <summary>
    /// Lógica de interacción para VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : Window
    {
        DispatcherTimer timer;
        bool isPaused;
        bool noVideo;
        Movie movie;

        public VideoPlayer(Movie mov)
        {
            InitializeComponent();
            movie = mov;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += new EventHandler(Timer_Tick);
            if (!(movie.UrlTrailer is null))
            {
                LoadVideo(movie.UrlTrailer);
                noVideo = false;
            }
            else
            {
                DisableControls();
                noVideo = true;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            sliderVideo.Value = mediaElementTrailer.Position.TotalSeconds;
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (isPaused)
            {
                mediaElementTrailer.Play();
                isPaused = false;
            }
            else
            {
                mediaElementTrailer.Pause();
                isPaused = true;
            }
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaElementTrailer.Stop();
            isPaused = true;
        }

        private void SliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElementTrailer.Volume = (double)sliderVolume.Value;
        }

        private void SliderVideo_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElementTrailer.Position = TimeSpan.FromSeconds(sliderVideo.Value);
        }

        private void Drop_Drop(object sender, DragEventArgs e)
        {
            string filename = (string)((DataObject)e.Data).GetFileDropList()[0];
            LoadVideo(filename);
        }

        private void LoadVideo(string filename)
        {
            mediaElementTrailer.Source = new Uri(filename);
            mediaElementTrailer.LoadedBehavior = MediaState.Manual;
            mediaElementTrailer.UnloadedBehavior = MediaState.Manual;
            mediaElementTrailer.Volume = (double)sliderVolume.Value;
            mediaElementTrailer.Play();
            isPaused = false;
        }

        private void MediaElementTrailer_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = mediaElementTrailer.NaturalDuration.TimeSpan;
            sliderVideo.Maximum = ts.TotalSeconds;
            timer.Start();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            if (noVideo == false)
            {
                mediaElementTrailer.Stop();
            }
            this.Close();
        }

        private void BtnYoutube_Click(object sender, RoutedEventArgs e)
        {
            if (noVideo == false)
            {
                mediaElementTrailer.Stop();
            }
            YoutubePlayer youtubePlayer = new YoutubePlayer(movie);
            youtubePlayer.Show();
            this.Close();
        }

        private void DisableControls()
        {
            btnPlay.IsEnabled = false;
            btnStop.IsEnabled = false;
            sliderVideo.IsEnabled = false;
            sliderVolume.IsEnabled = false;
        }
    }
}
