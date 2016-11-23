using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJT.Models;
using CJT.ViewModels;
using System.Collections.ObjectModel;

namespace CJT {
    public interface ITreeVM {

        DbContext DbContext { get; set; }

        ObservableCollection<Tag> AllTags { get; set; }

        ListBoxPanelVM<Tag> FilterPanelVM { get; set; }

        ObservableCollection<EntryVM> FirstGenEntryVMs { get; set; }

        IMainVM ParentVM { get; set; }

        void UpdateEntries();

    }
}
