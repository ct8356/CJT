using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CJT.Models {
    public class Relationship : Entry {
        //NOTE: got to be careful of circular relationships!
        //could mean an endless tree!
        private Entry entry1;
        [Index]
        public virtual Entry ParentEntry {
            get { return entry1; }
            set { entry1 = value; NotifyPropertyChanged("ParentEntry"); }
        }

        private Entry entry2;
        [Index]
        public virtual Entry ChildEntry {
            get { return entry2; }
            set { entry2 = value; NotifyPropertyChanged("ChildEntry"); }
        }

        public Relationship() : base() {
            //do nothing
        }

        public Relationship(string name) : this() {
            Name = name;
        }

        public Relationship(Entry entry1, Entry entry2, string name) : this(name) {
            ParentEntry = entry1;
            entry1.ChildRelations.Add(this);
            ChildEntry = entry2;
            entry2.ParentRelations.Add(this);
        }

    }
}
