using System.Collections.Generic;
using System.ComponentModel;

namespace CJT.Models {
    public class Tag : Entry {

        //NOTE: It makes life a lot easier to make this an ENTRY,
        //because then you can cast objects into type T in generic classes.
        //[DatabaseGenerated(DatabaseGeneratedOption.None)] //?? CBTL? important?

        //If you find yourself using more than 3 or 4 tags for one entry,
        //you should think about making a new property for that entry.

        public virtual ICollection<Entry> Entries { get; set; }

        public Tag() : base() {
            //do nothing
        }

        public Tag(string name) : this() {
            Name = name;
        }

    }
}
