using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJT.ViewModels {
    public abstract class MainVM : IMainVM {

        public EFContext DbContext {get;set;}

        public EntryVM SelectedEntryVM { get; set; }

        public abstract void UpdateEntries();

    }
}
