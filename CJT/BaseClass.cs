using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; //this allows INotifyPropertyChanged

namespace CJT {
    public class BaseClass : INotifyPropertyChanged { //NOTE: Don't forget INotify, or it wont work!
        //NOTE, you only need INotify, for events that are AUTO raised,
        //when a property is changed.
        //IF you are just raising events yourself, then you won't need INotify.
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName) {
            if (PropertyChanged != null) 
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
