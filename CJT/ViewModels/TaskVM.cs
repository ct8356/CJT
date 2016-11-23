﻿using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using CJT.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CJT.ViewModels {

    public class TaskVM : EntryVM {

        public TaskVM(Task task, ITreeVM treeVM) {
            //NOTE: this constructor just WRAPS a student in a VM.
            initialize(task, treeVM);
        }

        public TaskVM(String name, ITreeVM treeVM) {
            //NOTE: this one creates the Entry, and THEN wraps it!!!
            Task newTask = new Task(name);
            initialize(newTask, treeVM);
            DbContext.Tasks.Add(newTask);
        }

        public void initialize(Task task, ITreeVM treeVM) {
            base.initialize(task, treeVM, DbContext);
        }

        protected override void initializePropertyList() {
            base.initializePropertyList();
            //revisit
        }

        public override void insertEntry(EntryVM selectedVM) {
            TaskVM entryVM = new TaskVM("blank", TreeVM);
            insertEntry(entryVM, selectedVM);
        }

        public override void insertSubEntry(EntryVM selectedVM) {
            TaskVM entryVM = new TaskVM((selectedVM.Entry as Task).Name + " child", TreeVM);
            //this creates an entry too!
            insertSubEntry(entryVM, selectedVM);
        }
    }
}