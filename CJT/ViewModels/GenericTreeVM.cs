using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Data.Entity;
using CJT.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq.Expressions;
using AutoCompleteBox = CJT.AutoCompleteBox;
using CJT;

namespace CJT.ViewModels {
    public abstract class GenericTreeVM<E,EVM,D> 
        : BaseClass, IGenericTreeVM<E,EVM,D>
        where E : BaseEntry where EVM : TreeNodeVM where D : DbContext {
        public event EventHandler EntryVMSelected;
        public IQueryable<E> FilteredEntries { get; set; } //for now, for reference only.
        public D DbContext { get; set; }
        public ObservableCollection<EVM> FirstGenEntryVMs { get; set; }
        public ObservableCollection<EVM> AllEntryVMs { get; set; }
        //needed to keep track of EntryVMs shown in the treeView (without having to scan tree).
        //public DbContext DbContext { get; set; } //NOTE, should only appear in sub class!
        public TreeNodeVM FilterEntryVM { get; set; }
        public BaseEntry Orphan { get; set; }
        public bool WaitingForParentSelection { get; set; }
        //public void showUntaggedEntries() {
        //    IQueryable<Task> untaggedStudents = filteredStudents.Where(s => !s.Tags.Any()); //Will this work?
        //    foreach (Task student in untaggedStudents) {
        //    }
        //}

        public GenericTreeVM(D dbContext) {
            DbContext = dbContext as D;
            //New stuff
            //FilterEntryVM = new EntryVM(this); //CURRENT MUST DO THIS IN SUB CLASS!
            //FILTER PANELS
            //OLD STUFF
            FirstGenEntryVMs = new ObservableCollection<EVM>();
            AllEntryVMs = new ObservableCollection<EVM>();
            //filterToDos();
            //INITIALIZE
            //newEntry.LostFocus += new RoutedEventHandler(newEntry_LostFocus);
            //newEntry.KeyUp += new KeyEventHandler(newEntry_KeyUp);
        }

        public void FilterChanged() {
            UpdateEntries();
        }

        protected void NotifyEntryVMSelected(object sender, EventArgs args) {
            if (EntryVMSelected != null)
                EntryVMSelected(sender, args);
        }

        public void StructureChanged() {
            RefreshView();
        }

        public virtual void RefreshView() {
        }

        public abstract void UpdateEntries();

        public void waitForParentSelection(E entry) {
            WaitingForParentSelection = true;
            Orphan = entry;
            UpdateEntries();
            //this will update the treeView, AND if this is true,
            //it will be redrawn with certain colour scheme, to make the waiting, obvious.
            //drag and drop won't be too hard, BUT start here. easier.
            //ALSO, WHEN TRUE, need to create a LISTENER,
            //THAT subscribes now to the itemClick event.
            //and WHEN fires,
            //it will call that PANELs dataContext, and its method adoptChild(orphan);
            //WILL JUST HAVE TO UNSUBSCRIBE! It is poss. -=...
        }

    }
}
