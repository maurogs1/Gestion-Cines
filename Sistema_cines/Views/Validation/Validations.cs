using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Views.Validation
{
    public static class Validations
    {
        public static void NoLetters_KeyDown(KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
            else
                e.Handled = true;

        }

        public static void NoNumbers_KeyDown(KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.Z)
                e.Handled = false;
            else
                e.Handled = true;

        }
    }
}
