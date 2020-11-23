using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Views.ViewModel
{
    public class SubItem
    {
        public string Name { get; set; }
        public UserControl Screen { get; set; }
        public Window Window { get; set; }
        public PackIconKind Icon { get; set; }
        public List<SubItem> SubItems { get; set; }

        public SubItem(string name, PackIconKind icon)
        {
            Name = name;
            Icon = icon;
        }




        public SubItem(string name, List<SubItem> subItems)
        {
            Name = name;
            SubItems = subItems;
        }



        public SubItem(string name, UserControl screen)
        {
            Name = name;
            Screen = screen;
        }

        public SubItem(string name, UserControl screen, PackIconKind icon)
        {
            Name = name;
            Screen = screen;
            Icon = icon;
        }

        public SubItem(string name, Window window)
        {
            Name = name;
            Window = window;
        }
    }
}
