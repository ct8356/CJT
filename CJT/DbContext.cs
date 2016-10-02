using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel; //this allows INotifyPropertyChanged
using System.Data.OleDb;
using System.Data;
using System.IO;
using DataTable = System.Data.DataTable;
using System.Collections.Generic;
using CJT.Models;

namespace CJT {
    public abstract class DbContext : System.Data.Entity.DbContext {
        public event EventHandler TransactionTableChanged;

        public abstract string GetConnectionString();

        public string GetConnectionString(string filePath) {
            return string.Format(
                //"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'",
                "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 12.0;",
                filePath);
        }

        public string GetOtherConnectionString(string filePath) {
            return string.Format(
                //"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=YES;IMEX=1;'",
                "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;'",
                filePath);
        }

        public abstract DataTable GetDataTable(string command);

        public abstract DataTable GetDataTable(string command, string search);

        public abstract DataTable GetDataTable(string command, string search, string filePath);

        public abstract DataTable GetDataTable(string command, string[] a, string[] b);

        //public IList<Entry> GetList(DataTable dataTable) {
            //return dataTable.AsDataView<Entry>();
            //could use this, BUT would have to convert with a loop probably.
        //}

        public abstract int InsertEntry(string command, string[] a, string[] b);

        public abstract void SaveSettings();

    }
}
