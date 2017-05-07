using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel; //this allows INotifyPropertyChanged
using CJT.Models;
using CJT;

namespace CJT.ViewModels {
    public class ListBoxPanelVM<T> : BaseClass, INotifyInputConfirmed, IListVM where T : BaseEntry, new() {
        //NOTE: this class is not so flexible,
        //BUT it is used so often, that it is worth using!
        public delegate void ObjectEventHandler(object sender, ObjectEventArgs<T> e);
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);
        public event MessageEventHandler InputConfirmed;
        public TreeNodeVM EntryVM { get; set; }
        public DbContext DbContext { get; set; }
        public DbSet<T> DbSet { get; set; }
        public string RelationName { get; set; }
        public int RelationNumber { get; set; } //1 for parents, 2 for children
        private ObservableCollection<T> selectableItems;
        public ObservableCollection<T> SelectableItems {
            get { return selectableItems; }
            set { selectableItems = value; NotifyPropertyChanged("SelectableItems"); }
        }
        private ObservableCollection<T> selectedItems;
        public ObservableCollection<T> SelectedItems {
            get { return selectedItems; }
            set { selectedItems = value; NotifyPropertyChanged("SelectedItems"); }
        }

        public virtual void GoTo(object something) {
            //
        }

        public void NotifyInputConfirmed(string input) {
            if (InputConfirmed != null)
                InputConfirmed(this, new MessageEventArgs(input));
        }

        public void Remove(object item) {
            SelectedItems.Remove(item as T);
            DbContext.SaveChanges();
            //MainVM.UpdateEntries(); //DO IN SUBCLASS!
            //AGAIN perhaps dont want to do this,
            //SINCE it updates whole list? slow!
        }
        //NOTE if DELETE tag totally, GOT to remove it from AllTags!
        //REVISIT CURRENT!

    }
}

