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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Views.WReserve;
using Views.WMovie;

namespace Views.WProjection
{
    /// <summary>
    /// Lógica de interacción para ProjectionCards.xaml
    /// </summary>
    public partial class ProjectionCards : UserControl
    {
        public ProjectionCards()
        {
            InitializeComponent();
        }
        private void AddReserve(object sender, RoutedEventArgs e)
        {
            Projection proj = ((sender as FrameworkElement).DataContext as Projection);
            Reserve reserve = new Reserve();
            reserve.SetProjection(proj);
            reserve.Show();
            
        }

        private void BtnYoutube_Click(object sender, RoutedEventArgs e)
        {
            var baseobj = sender as FrameworkElement;
            Projection projection = baseobj.DataContext as Projection;
            YoutubePlayer youtubePlayer = new YoutubePlayer(projection.Movie);
            youtubePlayer.Show();
        }

        private void BtnVideo_Click(object sender, RoutedEventArgs e)
        {
            var baseobj = sender as FrameworkElement;
            Projection projection = baseobj.DataContext as Projection;
            VideoPlayer videoPlayer = new VideoPlayer(projection.Movie);
            videoPlayer.Show();
        }
    }
}
