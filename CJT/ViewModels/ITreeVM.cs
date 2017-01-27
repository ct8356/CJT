using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJT.Models;
using CJT;
using System.Collections.ObjectModel;

namespace CJT.ViewModels {
    public interface ITreeVM {

        DbContext DbContext { get; set; }

        ComboBoxVM TypePanelVM { get; set; }

        ComboBoxVM StructurePanelVM { get; set; }

        ObservableCollection<EntryVM> FirstGenEntryVMs { get; set; }

        IMainVM ParentVM { get; set; }

        void UpdateEntries();

    }
}
