﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Windows.Data;

namespace CJT {
    public class DataGrid : System.Windows.Controls.DataGrid {
        //PROBS don't need to use this class. The CustomBoundColumn bit
        //cant handle scroll bars for some reason!
        public bool IsSourceTable { get; set; }

        public DataGrid() {
            MaxColumnWidth = 500;
            AutoGenerateColumns = false;
            //SUBSCRIBE
            Loaded += this_Loaded; //Loaded seems better than datacontext changed.
            //datacontextChanged seems to call it just BEFORE dataContext actually changed!
            //JUST make sure you ALWAYS set the DataContext for each table!
            SelectionChanged += this_SelectionChanged;
        }

        public void AddColumnsFromDataTable() {
            if (Columns.Count == 0) {
                string[] columnNames = (DataContext as DataTableVM).DataTable.Columns
                    .Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
                foreach (string columnName in columnNames) {
                    Binding binding = new Binding(columnName);
                    Columns.Add(new CustomBoundColumn() {
                        Header = columnName,
                        Binding = binding,
                        //TemplateName = "CustomDataTemplate",
                        DataTable = (DataContext as DataTableVM).DataTable
                    }); //MY GUESS is, that because adding columns manually, 
                    //the original binding is lost.
                    //BUT we are setting a binding here.
                    //BUT probs have to set it again.
                    //DATACONTEXT! how does column know its dataContext?!
                }
            }
        }

        public virtual void this_Loaded(object sender, EventArgs e) {
            //AddColumnsFromDataTable(); //Not the issue.
            //Don't need to do this now thanks to EditableComboBox class?
        }

        public void this_SelectionChanged(object sender, EventArgs e) {
            if (IsSourceTable && SelectedItem != null)
                (DataContext as DataTableVM).SelectedItem = (SelectedItem as DataRowView).Row;
        } //PROBLEM! SelectionChanged is called AFTER PropertyChanged!
        //BUT NO! nonsense, since this method must be called, in order for SelectedItem to be changed?

    }
}