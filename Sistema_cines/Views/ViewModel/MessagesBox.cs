using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Effects;
using Views.Messages;

namespace Views.ViewModel
{
    public class MessagesBox
    {
        public enum Buttons { Yes_No, OK }

        public static string ShowDialog(string Text, Buttons buttons)
        {
            ShowBlurEffectAllWindow();
            W_MessagesBox messageBox = new W_MessagesBox(Text, buttons);
            messageBox.ShowDialog();
            StopBlurEffectAllWindow();
            return messageBox.ButtonString;

        }


        static BlurEffect MyBlur = new BlurEffect();
        public static void ShowBlurEffectAllWindow()
        {
            MyBlur.Radius = 20;
            foreach (Window window in Application.Current.Windows)
                window.Effect = MyBlur;
        }

        public static void StopBlurEffectAllWindow()
        {
            MyBlur.Radius = 0;
        }
    }

   

}
