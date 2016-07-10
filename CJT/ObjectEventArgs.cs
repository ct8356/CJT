using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJT {
    public class ObjectEventArgs<T> : EventArgs {
        public T Object { get; private set; }

        public ObjectEventArgs(T thing) {
            Object = thing;
        }
    }
}
