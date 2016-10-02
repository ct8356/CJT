using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CJT;
using System.Windows.Controls;
using System.Windows;
using CJT.Models;

namespace CJT {
    public abstract class DataListVM : BaseClass {
        protected DbContext DbContext { get; set; }
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);
        public event MessageEventHandler ShowMessageEvent;
        private int currentSelection;
        public int CurrentSelection {
            get { return currentSelection; }
            set { currentSelection = value; NotifyPropertyChanged("CurrentSelection"); }
        }
        public IList<Entry> DataList { get; set; }

        public void Search(string searchBar) {
            UpdateDataList(searchBar);
        }

        public void ShowMessage(string message) {
            ShowMessageEvent(this, new MessageEventArgs(message));
        }

        public abstract IList<Entry> GetDataList(string searchString);

        public abstract void UpdateDataList();

        public abstract void UpdateDataList(string searchBar);

    }
}
