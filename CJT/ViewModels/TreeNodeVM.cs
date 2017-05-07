using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Controls;
using System.ComponentModel; //this allows INotifyPropertyChanged
using System.Collections.ObjectModel;
using CJT.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows;
using CJT;
using AutoCompleteBox = CJT.AutoCompleteBox;

namespace CJT.ViewModels {
    public class TreeNodeVM : BaseClass {
        public event EventHandler EntryVMSelected;
        public event EventHandler RelationChanged;
        //NOTE: MIGHT be able to make a generic version of this class,
        //BUT it is hard! lots of changing necessary.
        //TOO MUCH CHANGING considering how short on time I am!
        //NOTE: DataGrid AutoGenerateColumns will make a column
        //for every public property in this class.
        //BUT YO! I think the easiest way for NOW,
        //Is just to define the columns in the DataTable! 
        //REALLY won't take long to update it there!
        //OR maybe this VM should have a list?
        //BUT apparently Columns iS NOT a part of visual tree!
        //SO binding like that is difficult.
        //SO FOR NOW, just specify it in View.
        //NEED TO GO FASTER NOW!!! OR WILL NEVER GET ANYWHERE! REALLY! 2 YEARS GONE!
        //public ITreeVM TreeVM { get; set; } //PRETTY SURE THIS SHOULD ONLY APPEAR IN CHILDCLASS! REVISIT!
        //public Entry Entry { get; set; } //LIKEWISE
        //public DbContext DbContext { get; set; } //LIKEWISE
        //BUT NOTE, you can't do any special constructs (with arguments) here then!
        //OK! SEEMS FINE!
        //AH BUT problem was, HAD to specify type of TreeVM to use!
        //AND, did not know what version of EntryVM (type) to specify!
        //OK but I guess we do that in the SUB PROJECT!
        public bool IsExpanded { get; set; }
        //SHOULD there be an option to emanciate??? (free from parent?)

        public TreeNodeVM() {
        }

        public void NotifyEntryVMSelected() {
            if (EntryVMSelected != null) {
                EntryVMSelected(this, new EventArgs());
            }
        }

        public void NotifyRelationChanged() {
            if (RelationChanged != null) {
                RelationChanged(this, new EventArgs());
            }
        }

        public virtual void insertEntry(TreeNodeVM selectedVM) {
            //Do nothing (only here to be overridden.)
        }

        public virtual void insertSubEntry(TreeNodeVM selectedVM) {
            //Do nothing. (just here to be overriden by subclass)
            //who creates a NodeVM, and passes it to method below.
        }

    }
}
