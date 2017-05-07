using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CJT.ViewModels {
    public class GenericComboBoxVM<T> : BaseClass {
        //NOTE: Could rename this to something more generic.
        //So that AutoCompleteBox and others could also use it as DataContext.
        //INFACT, could make THIS just a GROUPING class.
        //BUT No, it NEEDS to have some methods that do stuff to data.
        //(EVEN if these methods are triggered by events, not called).

        private ObservableCollection<T> items;
        public ObservableCollection<T> Items {
            get { return items; }
            set { items = value; NotifyPropertyChanged("Items"); }
        }

        private T selectedItem;
        public T SelectedItem {
            get { return selectedItem; }
            set { selectedItem = value; NotifyPropertyChanged("SelectedItem"); }
        }

        private bool isApplied;
        public bool IsApplied {
            get { return isApplied; }
            set { isApplied = value; NotifyPropertyChanged("IsApplied"); }
        }

        public GenericComboBoxVM() {
            Items = new ObservableCollection<T>();
        }

    }
}
