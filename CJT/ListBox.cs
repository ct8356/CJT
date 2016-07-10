using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.ComponentModel;

namespace CJT {
    public class ListBox : System.Windows.Controls.ListBox {

        public ListBox() {
            ItemsPanel = (ItemsPanelTemplate)XamlReader.Parse(
                "<ItemsPanelTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">"+
                "<StackPanel Orientation=\"Horizontal\"/></ItemsPanelTemplate>");
            BorderBrush = new SolidColorBrush(Colors.LightGray);
            Background = new SolidColorBrush(Colors.LightGray);
            Items.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
            ContextMenu = new ContextMenu();
            MenuItem delete_MenuItem = new MenuItem();
            ContextMenu.Items.Add(delete_MenuItem); //PROBLEM! menu belongs to listBox, not ITEM!
            delete_MenuItem.Click += Delete_MenuItem_Click;
            delete_MenuItem.Header = "Remove item";
        }

        public void Delete_MenuItem_Click(Object sender, EventArgs e) {
            MenuItem menuItem = (MenuItem)sender;
            if (menuItem != null) {
                if (SelectedIndex == -1) return;
                //IF this listBox is bound to student.courses, will deleting an item, delete the course from student?
                //MAYBE, BUT still, it probably would NOT do a SAVECHANGES...
                //MAybe could add a LISTENER for this though...
                object selectedItem = Items[SelectedIndex] as object;
                if (selectedItem != null) {                
                    //maybe object is just TOO generic to work? Not sure why though.      
                    (DataContext as IRemovable).Remove(selectedItem);
                    //REVISIT! Should be ItemsSource? PERHAPS!
                    //BECAUSE KNOW the itemsSource will be a list! DataContext, 
                    //COULD BE ANYTHING for a listBox! Needs thought.
                    //Fire event? to results?
                }
            }
        }

    }
}
