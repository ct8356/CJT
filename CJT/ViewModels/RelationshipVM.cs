using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJT.Models;
using System.ComponentModel; //this allows INotifyPropertyChanged
using System.Collections.ObjectModel;

namespace CJT.ViewModels {
    public class RelationshipVM : EntryVM {
        public ObservableCollection<Entry> SelectableEntries { get; set; }

        public RelationshipVM(ITreeVM treeVM) {
            Entry = new Relationship();
            initialize(treeVM);
            //Do nothing.
            //JUST here, for when create a new Relationship, in window,
            //BUT have not saved it yet.
        }

        public RelationshipVM(Relationship entry, ITreeVM treeVM) {
            //NOTE: this constructor just WRAPS a student in a VM.
            initialize(entry, treeVM);
        }

        public RelationshipVM(String name, ITreeVM treeVM) {
            //NOTE: this one creates the Entry, and THEN wraps it!!!
            Relationship newEntry = new Relationship(name);
            initialize(newEntry, treeVM);
            DbContext.Relationships.Add(newEntry);
        }

        protected override void initialize(ITreeVM treeVM) {
            base.initialize(treeVM);
            SelectableEntries = new ObservableCollection<Entry>(DbContext.Nodes);
        }

        protected override void initializePropertyList() {
            base.initializePropertyList();
            ImportantProperties.Add(new Property("ParentEntry", (Entry as Relationship).ParentEntry, InfoType.LinkedTextBlock, true, DbContext));
            ImportantProperties.Add(new Property("ChildEntry", (Entry as Relationship).ChildEntry, InfoType.LinkedTextBlock, true, DbContext));
        }

        public override void insertEntry(EntryVM selectedVM) {
            RelationshipVM entryVM = new RelationshipVM("blank", TreeVM);
            insertEntry(entryVM, selectedVM);
        }

        public override void insertSubEntry(EntryVM selectedVM) {
            RelationshipVM entryVM = new RelationshipVM((selectedVM.Entry as Relationship).Name + " child", TreeVM);
            //this creates an entry too!
            insertSubEntry(entryVM, selectedVM);
        }

        public void AddRelationToDatabase() {
            DbContext.Relationships.Add(Entry as Relationship);
            DbContext.SaveChanges();
        }
    }
}
