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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Views.ViewModel;

namespace Views.Messages
{
    /// <summary>
    /// Lógica de interacción para W_MessagesBox.xaml
    /// </summary>
    public partial class W_MessagesBox : Window
    {
        public string ButtonString { get; set; }
        public W_MessagesBox(string Message, MessagesBox.Buttons buttons)
        {
            InitializeComponent();
            txtMessage.Text = Message;
            switch (buttons)
            {
                case MessagesBox.Buttons.Yes_No:
                    btnOK.Visibility = Visibility.Hidden;
                    break;
                case MessagesBox.Buttons.OK:
                    btnYes.Visibility = Visibility.Hidden;
                    btnNo.Visibility = Visibility.Hidden;
                    break;
            }

        }

        private void gBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch { }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            ButtonString = "-1";
            Close();
        }

        DoubleAnimation anim;
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Closing -= Window_Closing;
            e.Cancel = true;
            anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.3));
            anim.Completed += (s, _) => this.Close();
            this.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Height = (txtMessage.LineCount * 27) + gBar.Height + 60;
        }

        private void btnReturnValue_Click(object sender, RoutedEventArgs e)
        {
            ButtonString = ((Button)sender).Uid.ToString();
            Close();
        }

    }
}
