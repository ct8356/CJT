using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Controls;
using System.ComponentModel; //this allows INotifyPropertyChanged
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CJT.Models {
    public class BaseEntry : BaseClass {
        [Key] //nec if class name doesn't match int name.
        public int EntryID { get; set; }
        private string name;
        public string Name {
            get { return name; }
            set { name = value; NotifyPropertyChanged("Name"); }
        }

        public string Type { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Template { get; set; }

        public BaseEntry() {
            CreationDate = DateTime.Now;
            //DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        public BaseEntry(string name) : this() {
            Name = name;
        } //NOTE YO! I really don't think should be allowed to do this!
        //ALMOST needs to be an ABSTRACT class!

        public override string ToString() {
            return Name;
        }

    }
}
