
using BaseClass;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Views.WMovie
{
    /// <summary>
    /// Lógica de interacción para YoutubePlayer.xaml
    /// </summary>
    public partial class YoutubePlayer : Window
    {
        const string YoutubeLinkRegex = "(?:.+?)?(?:\\/v\\/|watch\\/|\\?v=|\\&v=|youtu\\.be\\/|\\/v=|^youtu\\.be\\/)([a-zA-Z0-9_-]{11})+";
        Regex regexExtractId = new Regex(YoutubeLinkRegex, RegexOptions.Compiled);
        string[] validAuthorities = { "youtube.com", "www.youtube.com", "youtu.be", "www.youtu.be" };

        public YoutubePlayer(Movie movie)
        {
            InitializeComponent();
            this.Title = movie.Title;
            if (movie.UrlYoutube != "")
            {
                string videoId = ExtractVideoIdFromUri(new Uri(movie.UrlYoutube));
                if (videoId != null)
                {
                    ShowYoutubeWebPlayer(videoId);
                }
                else
                {
                    ShowMessage("Enlace erroneo");
                }
            }
            else
            {
                ShowMessage("La pelicula no tiene un link a Youtube");
            }
        }

        private void ShowYoutubeWebPlayer(string videoId)
        {
            ChromiumWebBrowser browser = new ChromiumWebBrowser("https://www.youtube.com/embed/" + videoId);
            grdYoutube.Children.Add(browser);
        }

        private void ShowMessage(string msg)
        {
            TextBlock txt = new TextBlock
            {
                Text = msg,
                FontSize = 30,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.White),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            grdYoutube.Children.Add(txt);
        }

        private string ExtractVideoIdFromUri(Uri uri)
        {
            try
            {
                string authority = new UriBuilder(uri).Uri.Authority.ToLower();

                //comprueba si la URL es una URL de youtube
                if (validAuthorities.Contains(authority))
                {
                    //extrae el ID
                    var regRes = regexExtractId.Match(uri.ToString());
                    if (regRes.Success)
                    {
                        return regRes.Groups[1].Value;
                    }
                }
            }
            catch { }
            return null;
        }
    }
}
