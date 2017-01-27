using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using CJT.Models;
using CJT;

namespace CJT.ViewModels {
    public class ComboBoxVM : BaseClass {
        public ObservableCollection<object> Items { get; set; }
        private object selectedItem;
        public object SelectedItem {
            get { return selectedItem; }
            set { selectedItem = value; NotifyPropertyChanged("SelectedItem"); }
        }
        private bool isApplied;
        public bool IsApplied {
            get { return isApplied; }
            set { isApplied = value; NotifyPropertyChanged("IsApplied"); }
        }

        public ComboBoxVM() {
            Items = new ObservableCollection<object>();
        }

    }
}
