using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJT.Models {
    public class Node : Entry {

        public virtual ICollection<Entry> Entries { get; set; }

        public Node() : base() {
            Type = "PowerNote.Models.Node";
        }

        public Node(string contents) : this() {
            Name = contents;
        }

    }
}
