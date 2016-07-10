using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CJT;
using System.Windows.Controls;
using System.Windows;

namespace CJT {
    public class DataTableVM : BaseClass {
        protected ExcelContext ExcelContext { get; set; }
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);
        public event MessageEventHandler ShowMessageEvent;
        public string CommandText;

        private DataTable dataTable;
        public DataTable DataTable {
            get { return dataTable; }
            set { dataTable = value;
                //NotifyPropertyChanged("DataTable");
            }
        }

        public string FileName = "ElectricalCupboardContents";
        public string FilePath { get; set; }

        private int currentSelection;
        public int CurrentSelection {
            get { return currentSelection; }
            set { currentSelection = value; NotifyPropertyChanged("CurrentSelection"); }
        }

        private DataRow selectedItem;
        public DataRow SelectedItem {
            get { return selectedItem; }
            set { selectedItem = value; NotifyPropertyChanged("SelectedItem"); }
        }

        public void Search(string searchBar) {
            UpdateDataTable(searchBar);
        }

        public void ShowMessage(string message) {
            ShowMessageEvent(this, new MessageEventArgs(message));
        }

        public void UpdateDataTable(string searchBar) {
            DataTable = ExcelContext.GetDataTable(CommandText, searchBar);
        }

    }
}
