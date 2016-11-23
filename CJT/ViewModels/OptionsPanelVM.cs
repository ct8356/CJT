using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using System.Windows.Data;
using CJT.Models;

namespace CJT.ViewModels {
    public class OptionsPanelVM {
        DbContext context;
        public bool ShowAllEntries { get; set; }
        public bool ShowAllChildren { get; set; }

        public OptionsPanelVM(IMainVM parentVM) {
            this.context = parentVM.DbContext;
        }

    }
}