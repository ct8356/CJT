using System.Collections.Generic;
using System.ComponentModel;

namespace CJT.Models {
    public class Tag : Entry {

        //NOTE: It makes life a lot easier to make this an ENTRY,
        //because then you can cast objects into type T in generic classes.
        //[DatabaseGenerated(DatabaseGeneratedOption.None)] //?? CBTL? important?

        public virtual ICollection<Entry> Entries { get; set; }

        public Tag() : base() {
            //do nothing
        }

        public Tag(string name) : this() {
            Name = name;
        }

    }
}
