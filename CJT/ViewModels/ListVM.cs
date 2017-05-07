using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;
using CJT.Models;

namespace CJT.ViewModels {
    public class ListVM<T> : BaseClass, IListVM where T : BaseEntry {
        //NOTE, this class is not really necessary?

        private IList<T> items;
        public IList<T> Items {
            get { return items; }
            set { items = value; NotifyPropertyChanged("Objects"); }
        }

        public ListVM() {
            Items = new ObservableCollection<T>();
        }

        public void GoTo(object item) {
            Items.Remove(item as T); //cast only allowed if T : Something.
        }

        public void This_Add(object sender, ObjectEventArgs<T> e) {
            Items.Add(e.Object);
        }

        public void Remove(object item) {
            Items.Remove(item as T); //cast only allowed if T : Something.
        }

    }
}
