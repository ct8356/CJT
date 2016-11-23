﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using CJT.Models;
using CJT;

namespace CJT.ViewModels {
    public class ComboBoxVM {
        public ObservableCollection<object> Objects { get; set; }
        public object SelectedObject { get; set; }
        public IMainVM VM { get; set; }

        public ComboBoxVM(IMainVM parentVM) {
            VM = parentVM;
        }

        public void updateSelectedObject(object selectedObject) {
            SelectedObject = selectedObject;
            VM.UpdateEntries();
        }


    }
}
