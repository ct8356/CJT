using System.Collections.Generic;
using System.ComponentModel;

namespace CJT {
    public class Tag : Entry {

        //NOTE: It makes life a lot easier to make this an ENTRY,
        //because then you can cast objects into type T in generic classes.
        //[DatabaseGenerated(DatabaseGeneratedOption.None)] //?? CBTL? important?

        string title;
        public string Title {
            get { return title; }
            set { title = value; NotifyPropertyChanged("Title"); }
        }

        public virtual ICollection<Entry> Entries { get; set; }

        public Tag() {
            //do nothing
        }

        public Tag(string title) {
            this.title = title;
        }

        public override string ToString() {
            return Title;
        }
    }
}
