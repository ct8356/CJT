using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CJT.Models;

namespace CJT {
    public class DataTableVM : DataListVM, IDataTableVM {
        public DataTable DataTable { get; set; }
        public string CommandText { get; set; }
        public string FileName { get; set; }// = "ElectricalCupboardContents";
        public string FilePath { get; set; }
        public string TableName { get; set; }
        private DataRow selectedItem;
        public DataRow SelectedItem {
            get { return selectedItem; }
            set { selectedItem = value; NotifyPropertyChanged("SelectedItem"); }
        }

        public override IList<Entry> GetDataList(string searchString) {
            return null;
        }

        public override void UpdateDataList() {
            DataTable = DbContext.GetDataTable(CommandText, "", FilePath);
        }

        public override void UpdateDataList(string search) {
            DataTable = DbContext.GetDataTable(CommandText, search);
        }

    }
}
