﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CJT {
    public class DataGrid : System.Windows.Controls.DataGrid {

        public DataGrid() {
            IsReadOnly = true;
            MaxColumnWidth = 500;
        }

    }
}