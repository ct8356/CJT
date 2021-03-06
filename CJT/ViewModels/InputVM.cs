﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using CJT.Models;

namespace CJT.ViewModels {
    public class InputVM<T> : BaseClass, INotifyInputConfirmed where T : BaseEntry {
        public delegate void ObjectEventHandler(object sender, ObjectEventArgs<T> e);
        public event ObjectEventHandler InputConfirmed;

        private object input; //even needed?
        public object Input {
            get { return input; }
            set { input = value; NotifyPropertyChanged("Input"); }
        }

        private ObservableCollection<T> objects;
        public ObservableCollection<T> Objects {
            get { return objects; }
            set { objects = value; NotifyPropertyChanged("Objects"); }
        }

        public InputVM() {
            Objects = new ObservableCollection<T>();
            //SUBSCRIBE
            InputConfirmed += This_InputConfirmed;
        }

        public void NotifyInputConfirmed(string input) {
            //NOTE: I think it IS correct to take a string at this point...
            //BECAUSE want to be able to do comparisons BEFORE creating a new object!
            //BUT REVISIT! Since this will take some work.
            InputConfirmed(this, new ObjectEventArgs<T>(input as T));
        }//YES! JUST NotifyInputConfirmed in this method.
        //THEN if want to do other stuff after, do it in RESPONSE to this!

        public void This_InputConfirmed(object sender, ObjectEventArgs<T> e ) {
            Objects.Add(e.Object as T);
            //THEN remember that LISTBOXVM has to RESPOND to this. REVISIT
        }


    }
}
