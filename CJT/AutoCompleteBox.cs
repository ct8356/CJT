using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.ComponentModel;

namespace CJT {
    public class AutoCompleteBox : System.Windows.Controls.AutoCompleteBox {

        public AutoCompleteBox() {
            BorderBrush = new SolidColorBrush(Colors.LightGray);
            Background = new SolidColorBrush(Colors.LightGray);
            IsTextCompletionEnabled = true;
            //SUBSCRIBE
            //KeyUp += This_KeyUp;
            //DO THIS where the autocompleteBox is used! 
            //COZ not all uses of autoCompleteBox want to do something as soon as filled!
            //NOTE: you should try FilterMode = Contains!
        }

        public void This_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Return) {
                This_SelectionChanged(sender, e);
            }
        }

        public void This_SelectionChanged(object sender, EventArgs e) {
            AutoCompleteBox autoCompleteBox = (AutoCompleteBox)sender;
            (DataContext as INotifyInputConfirmed).NotifyInputConfirmed(autoCompleteBox.Text);
            //NOTE: You make your life WAY easier, if you just specify the INTERFACE for the DataContext!
            //(ItemsSource as List<Entry>).Add(autoCompleteBox.SelectedItem as Entry); //do this in VM? the DataContext?
            //MAYBE, but maybe easier to do it straight to ItemSource, since KNOW it is a list?
            //BUT of course, don't know it is a list of objects, SO just as bad!
            //(CAN'T even cast to a List<Entry> if it is a List<PartClass>! etc.
            //It is just not possible!
            //SO do it in the VM! the DataContext!

            //So DataContext of AutoCompleteBox is a list of objects. (Note, objects can be funny, beware).
            //GOOD! because it should not be a ListBoxVM. that is for LABELS, AND autocompleteBox.
            //AutoCompleteBox should own a REFERENCE to the Labels. REALLY? WHY?
            //The labels should have a ListBoxVM. Perhaps MAKE a Control called ListBox.
            //ListBox contains the labels.
            //DataContext is a List of SelectedObjects.
            //AutoCompleteBox's DataContext is List of Objects.
            //ListBoxVM must know about List Objects, to know it can only select from these options.
            //THEN can have ListAndAutoCompleteBoxVM, WHICH just LETS AUTOCOMPLETEBOX KNOW about ListBox.
            //(IF its even necessary).
            //AutoComplete is the ACTUATOR, ListBox is the RESPONDER.
            autoCompleteBox.Text = null;
        }

        private void updateSelection() { //NOT USED
            // get the source of the ListBox control inside the template
            var enumerator = ((Selector)GetTemplateChild("Selector")).ItemsSource.GetEnumerator();
            // update Selecteditem with the first item in the list
            enumerator.Reset();
            if (enumerator.MoveNext()) {
                var item = enumerator.Current;
                SelectedItem = item;
                // close the popup, highlight the text
                IsDropDownOpen = false;
                //(TextBox)GetTemplateChild("Text").SelectAll();
            }
        }

    }
}
