using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Views.ViewModel
{
    public class ItemMenu
    {
        public string Title { get; set; }
        public PackIconKind Icon { get; set; }
        public List<SubItem> SubItems { get; set; }

        public ItemMenu(string title, List<SubItem> subItems, PackIconKind icon)
        {
            Title = title;
            SubItems = subItems;
            Icon = icon;
        }
    }
}
