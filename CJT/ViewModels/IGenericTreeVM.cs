using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJT.Models;
using System.Collections.ObjectModel;

namespace CJT.ViewModels {
    public interface IGenericTreeVM<E, EVM, D> where E : BaseEntry where EVM : TreeNodeVM where D : DbContext {

        D DbContext { get; set; }

        ObservableCollection<EVM> FirstGenEntryVMs { get; set; }

        void UpdateEntries();

    }
}
