using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJT.Models;
using CJT.ViewModels;

namespace CJT {
    public interface IMainVM {

        EFContext DbContext { get; set; }

        EntryVM SelectedEntryVM { get; set; }

        void UpdateEntries();

        

    }
}
