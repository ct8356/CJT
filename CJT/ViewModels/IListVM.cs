using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CJT.ViewModels {
    public interface IListVM {

        void GoTo(object item);

        void Remove(object item);

    }
}
