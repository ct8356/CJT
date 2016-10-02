﻿using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Controls;
using System.ComponentModel; //this allows INotifyPropertyChanged
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CJT.Models {
    public class PartClass : Entry {
        //PARENTS for this, should be INHERITANCE parents.
        //PARENTS for partInstance, should be LOCATION parents.
        //general PARENTs for Entry, should be, location parents.
        //WELL actually, I don't think they should exist.
        //WELL, maybe, for NOTE taking purposes.
        //WELL YES, I think, SHOULD be no GENERAL parent child relations.
        //ALL should have a SPECIFIC purpose.
        //SO should be NONE in Entry, unless can think of specific reason for it.
        //PARTS, SHOULD have ToDo, or NOTE children. thats how to relate them. NOT general children.

        string manufacturer = "Undefined";
        public string Manufacturer {
            get { return manufacturer; }
            set { manufacturer = value; NotifyPropertyChanged("Manufacturer"); }
        }

        string orderNumber = "Undefined";
        public string OrderNumber {
            get { return orderNumber; }
            set { orderNumber = value; NotifyPropertyChanged("OrderNumber"); }
        }

        string description = "Undefined";
        public string Description {
            get { return description; }
            set { description = value; NotifyPropertyChanged("Description"); }
        }

        public virtual ObservableCollection<PartInstance> PartInstances { get; set; }

        public virtual PartClass ParentPartClass { get; set; }
        public virtual ObservableCollection<PartClass> ChildPartClasses { get; set; }
        //[InverseProperty("SensedParts")]
        //public virtual ObservableCollection<PartInstance> Sensors { get; set; }

        public PartClass() : base() {
            Type = "PowerNote.Models.PartClass";
            PartInstances = new ObservableCollection<PartInstance>();
        } //IF got it, have to assume people will use it. WELL, if it is public.

        public PartClass(string nickName) : this() {
            Name = nickName;
        }

        public PartClass(string nickName, PartClass parent)
            : this() {
            Name = nickName;
            ParentPartClass = parent;
        }

    }
}
