using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJT.Models {
    public class Note : Entry {

        public virtual ICollection<Entry> Entries { get; set; }

        public Note() : base() {
            Type = "PowerNote.Models.Note";
        }

        public Note(string contents) : this() {
            Name = contents;
        }

        public Note(string contents, Tag tag) : this(contents) {
            Tags.Add(tag);
        }

    }
}
