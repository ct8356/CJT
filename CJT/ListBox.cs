using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows;

namespace CJT {
    public class ListBox : System.Windows.Controls.ListBox {

        public ListBox() {
            ItemsPanel = XamlReader.Parse(
                "<ItemsPanelTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\">"+
                "<StackPanel Orientation=\"Horizontal\" Margin=\"0\"/></ItemsPanelTemplate>") as ItemsPanelTemplate;
            ItemContainerStyle = XamlReader.Parse(
                "<Style xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" "+
                "TargetType=\"ListBoxItem\">"+
                "<Setter Property=\"Padding\" Value=\"0\"/>"+
                "<Setter Property=\"BorderThickness\" Value = \"1\"/>"+
                "<Setter Property=\"Margin\" Value=\"0\"/>"+
                "</Style>") as Style;
            //OR even easier than this...  ItemContainerStyle = Resources["ResName"] as Style;
            //BUT the above way is also nice since it keeps it local. Do whatever.
            BorderBrush = new SolidColorBrush(Colors.LightGray);
            Background = new SolidColorBrush(Colors.LightGray);
            Items.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
            ContextMenu = new ContextMenu();
            MenuItem goTo = new MenuItem();
            ContextMenu.Items.Add(goTo); //PROBLEM! menu belongs to listBox, not ITEM!
            goTo.Click += GoTo_Click;
            goTo.Header = "Go to entry";
            MenuItem delete = new MenuItem();
            ContextMenu.Items.Add(delete); //PROBLEM! menu belongs to listBox, not ITEM!
            delete.Click += Delete_Click;
            delete.Header = "Remove item";
            //PADDING
            Padding = new Thickness(0);
            Margin = new Thickness(0);
            BorderThickness = new Thickness(0);
        }

        public void Delete_Click(Object sender, EventArgs e) {
            MenuItem menuItem = (MenuItem)sender;
            if (menuItem != null) {
                if (SelectedIndex == -1) return;
                //IF this listBox is bound to student.courses, will deleting an item, delete the course from student?
                //MAYBE, BUT still, it probably would NOT do a SAVECHANGES...
                //MAybe could add a LISTENER for this though...
                object selectedItem = Items[SelectedIndex] as object;
                if (selectedItem != null) { 
                    //maybe object is just TOO generic to work? Not sure why though.      
                    (DataContext as IListVM).Remove(selectedItem);
                    //REVISIT! Should be ItemsSource? PERHAPS!
                    //BECAUSE KNOW the itemsSource will be a list! DataContext, 
                    //COULD BE ANYTHING for a listBox! Needs thought.
                    //Fire event? to results?
                }
            }
        }

        public void GoTo_Click(Object sender, EventArgs e) {
            MenuItem menuItem = (MenuItem)sender;
            if (menuItem != null) {
                if (SelectedIndex == -1)
                    return;
                object selectedItem = Items[SelectedIndex];
                if (selectedItem != null)
                    (DataContext as IListVM).GoTo(selectedItem);
            }
        }

    }
}
