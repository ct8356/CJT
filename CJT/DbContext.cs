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
        //NOTE: you SHOULD dispose of Context when no longer required (i.e. use using(){}).
        public event EventHandler TableChanged;
        public abstract DbSet<Entry> Entries { get; set; }
        public abstract DbSet<Note> Notes { get; set; }
        public abstract DbSet<Task> Tasks { get; set; }
        public abstract DbSet<Tag> Tags { get; set; }
        public abstract DbSet<Transaction> Transactions { get; set; }
        public abstract DbSet<PartClass> Parts { get; set; }
        public abstract DbSet<PartInstance> PartInstances { get; set; }
        public string FilePath { get; set; }

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

        protected void NotifyTableChanged() {
            TableChanged(this, new EventArgs());
        }

        public abstract void SaveSettings();

        public virtual void SaveChanges(object sender) {
            SaveChanges();
        }

    }
}
