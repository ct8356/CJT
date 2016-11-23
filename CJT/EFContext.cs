using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using DataTable = System.Data.DataTable;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CJT.Models;

namespace CJT {
    public class EFContext : DbContext {
        public override DbSet<Note> Notes { get; set; }
        public override DbSet<Entry> Entries { get; set; }
        public override DbSet<Task> Tasks { get; set; }
        public override DbSet<Tag> Tags { get; set; }
        public override DbSet<Transaction> Transactions { get; set; }
        public override DbSet<PartClass> Parts { get; set; }
        public override DbSet<PartInstance> PartInstances { get; set; }

        public override string GetConnectionString() {
            return "";
        }

        public override DataTable GetDataTable(string commandText) {
                    DataTable table = new DataTable();
                    return table;
        }

        public override DataTable GetDataTable(string commandText, string searchBar) {
            return null;
        }

        public override DataTable GetDataTable(string commandText, string searchBar, string filePath) {
            return null;
        }

        public override DataTable GetDataTable(string commandText, string[] columnNames, string[] values) {
            return null;
        }

        public override int InsertEntry(string commandText, string[] columnNames, string[] values) {
                    return 0;
        }

        public override void SaveSettings() {
            //do nothing
        }

    }

}
