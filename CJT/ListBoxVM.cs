using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;

namespace CJT {
    public class ListBoxVM<T> : BaseClass, IRemovable where T : Entry {
        //NOTE, this class is not really necessary?

        private ObservableCollection<T> objects;
        public ObservableCollection<T> Objects {
            get { return objects; }
            set { objects = value; NotifyPropertyChanged("Objects"); }
        }

        public ListBoxVM() {
            Objects = new ObservableCollection<T>();
        }

        public void This_Add(object sender, ObjectEventArgs<T> e) {
            Objects.Add(e.Object);
        }

        public void Remove(object item) {
            Objects.Remove(item as T); //cast only allowed if T : Something.
        }

    }
}
