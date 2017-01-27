using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJT.Models;
using CJT;

namespace CJT.ViewModels {
    public interface IMainVM {

        DbContext DbContext { get; set; }

        EntryVM SelectedEntryVM { get; set; }

        void UpdateEntries();

        

    }
}
