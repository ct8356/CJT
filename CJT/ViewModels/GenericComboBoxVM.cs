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

        private ObservableCollection<T> objects;
        public ObservableCollection<T> Objects {
            get { return objects; }
            set { objects = value; NotifyPropertyChanged("Objects"); }
        }
        private T selectedObject;

        public T SelectedObject{
            get { return selectedObject; }
            set { selectedObject = value; NotifyPropertyChanged("SelectedObject"); }
        }

    }
}
