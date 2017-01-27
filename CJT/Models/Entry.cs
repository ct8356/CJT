using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Controls;
using System.ComponentModel; //this allows INotifyPropertyChanged
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CJT.Models {
    public class Entry : BaseClass {
        public int EntryID { get; set; }
        private string name;
        public string Name {
            get { return name; }
            set { name = value; NotifyPropertyChanged("Name"); }
        }

        public string Type { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Template { get; set; }

        public virtual Entry Parent { get; set; }

        public virtual ObservableCollection<Entry> Children { get; set; }
        //NOTE: somehow, EntFramework inherently knows that Parent and Children are related.
        //BUT actually, it CAN'T here! how can it distinguish, since Parent and Children are BOTH of type Entry?
        //IT COULD WELL interpret it as Children means Siblings, and think its a manyToMany relation!

        [Index]
        [InverseProperty("ParentEntry")]
        public virtual ObservableCollection<Relationship> ChildRelations { get; set; }

        [Index]
        [InverseProperty("ChildEntry")]
        public virtual ObservableCollection<Relationship> ParentRelations { get; set; }

        public Entry() {
            CreationDate = DateTime.Now; 
            //DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Children = new ObservableCollection<Entry>();
            ChildRelations = new ObservableCollection<Relationship>();
            ParentRelations = new ObservableCollection<Relationship>();
        }

        public Entry(string name) : this() {
            Name = name;
        } //NOTE YO! I really don't think should be allowed to do this!
        //ALMOST needs to be an ABSTRACT class!

        public override string ToString() {
            return Name;
        }

    }
}
