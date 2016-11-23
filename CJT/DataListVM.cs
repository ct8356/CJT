using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Controls;
using System.Windows;
using CJT.Models;
using CJT.ViewModels;
using System.Collections.ObjectModel;

namespace CJT {
    public abstract class DataListVM : BaseClass, ITreeVM {
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);
        public event MessageEventHandler ShowMessageEvent;

        public ObservableCollection<Tag> AllTags { get; set; }

        private int currentSelection;
        public int CurrentSelection {
            get { return currentSelection; }
            set { currentSelection = value; NotifyPropertyChanged("CurrentSelection"); }
        }

        public DbContext DbContext { get; set; }

        public abstract IList<EntryVM> DataList { get; set; }

        public ListBoxPanelVM<Tag> FilterPanelVM { get; set; }

        public ObservableCollection<EntryVM> FirstGenEntryVMs { get; set; }

        public IMainVM ParentVM { get; set; }

        public abstract EntryVM SelectedItem { get; set; }


        public abstract IList<EntryVM> GetDataList(string searchString);

        public void Search(string searchBar) {
            UpdateDataList(searchBar);
        }

        public void ShowMessage(string message) {
            ShowMessageEvent(this, new MessageEventArgs(message));
        }

        public abstract void UpdateDataList();

        public abstract void UpdateDataList(string searchBar);

        public void UpdateEntries() {
            UpdateDataList();
        }

    }
}
