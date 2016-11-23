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
    public class EntryVM : BaseClass {
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
        public ITreeVM TreeVM { get; set; }
        public Entry Entry { get; set; }
        public DbContext DbContext { get; set; }
        public static ObservableCollection<object> Properties { get; set; }
        public ObservableCollection<Property> ImportantProperties { get; set; }
        public ListBoxPanelVM<Tag> TagsVM { get; set; }
        public EntryVM Parent { get; set; }
        //NOTE! I think proper way to do this, is to just MODIFY the entry,
        //BUT because the the EntryVM is bound to it, it will update itself accordingly!
        //i.e. this entryVM just has a public Entry Parent.
        //NOW, when that Entry is deleted, the EntryVM deletes itself. SO this EntryVM,
        //does not even need to know its own ParentVM???? maybe... not sure yet.
        public ObservableCollection<EntryVM> Children { get; set; }
        //DON'T want others adding to children. If I make it private, will that stop binding?
        //No, it won't, provided you KEEP this property public!!!
        //BUT problem is, then you can still add to Children...issue...
        public ListBoxPanelVM<Tag> FilterTags { get; set; }
        public bool IsExpanded { get; set; }

        //SHOULD there be an option to emanciate??? (free from parent?)

        public EntryVM() {
            //do nothing.
            //NOTE: is this called? maybe. Even if it is, does not matter.
        }

        public EntryVM(EntriesTreeVM treeVM) {
            TreeVM = treeVM;
            DbContext = treeVM.DbContext;
        }

        protected void initialize(DbContext dbContext) {
            DbContext = dbContext;
            //DON'T NEED to instantiate Entry here,
            //BECAUSE it is done in the class inheriting from EntryVM!
            //SUBSCRIBE
            Entry.PropertyChanged += Entry_PropertyChanged;
            PropertyChanged += EntryVM_PropertyChanged;
            //TagsInputVM.InputConfirmed += TagsVM.This_Add;
            //OH MY GOSH! So you are saying, should register to all lists
            //from here? What a pain in the bum!
            //SURELY it is much easier to PASS reference of this EntryVM,
            //TO the inputVM (or just use the ListBoxPanelVM!).
            //AND call the EntryVM to add, from there!!!
            //WELL, yes I am...
            //BUT this way is more flexible!
            //BUT maybe also a bit stupid.
            //COZ only reason did it, was so did not have to PASS an EntryVM.
            //AND you could just make anything with lists attached,
            //an EntryVM. Or a ListAttachedObject...
        }

        protected void initialize(Entry entry, DbContext dbContext) {
            initialize(dbContext);
            Children = new ObservableCollection<EntryVM>();
            bindToEntry(entry);
            //PROPERTIES
            initializePropertyList();
        }

        protected void initialize(Entry entry, ITreeVM treeVM, DbContext dbContext) {
            TreeVM = treeVM;
            FilterTags = treeVM.FilterPanelVM;
            initialize(entry, dbContext);
            //NOTE: don't HAVE to pass EntryVM a treeVM,
            //AS LONG AS you don't use any methods REQUIRING one...
            //OR FilterTags...
            //potentially dangerous though? Ok sort it out later.
        }

        protected virtual void initializePropertyList() {
            ImportantProperties = new ObservableCollection<Property>();
            ImportantProperties.Add(new Property("EntryID", Entry.EntryID, InfoType.TextBlock, false, DbContext));
            ImportantProperties.Add(new Property("CreationDate", Entry.CreationDate, InfoType.TextBlock, false, DbContext));
            ImportantProperties.Add(new Property("Name", Entry.Name, InfoType.TextBox, false, DbContext));
            ImportantProperties.Add(new Property("Parent", Entry.Parent, InfoType.LinkedTextBlock, false, DbContext));
            ImportantProperties.Add(new Property("Children", Children, InfoType.ListBox, false, DbContext));
            ImportantProperties.Add(new Property("Tags", TagsVM, InfoType.ListBox, true, DbContext));
        }

        public void adoptChild(EntryVM childVM) {
            //NOTE: This method should only be used if want to ADD children to ENTRY!!
            Children.Add(childVM); //Does this do the trick? Yes it seems to...
            childVM.Parent = this;
            Entry.Children.Add(childVM.Entry);
        }

        public void adoptSibling(EntryVM entryVM) {
            Parent.Children.Add(entryVM);
            entryVM.Parent = Parent;
            Parent.Entry.Children.Add(entryVM.Entry);
        }

        public void adoptChildFromTreeVM(EntryVM orphan) {
            Entry.Children.Add(orphan.Entry);
        }

        public void bindToEntry(Entry entry) {
            Entry = entry;
            TagsVM = new ListBoxPanelVM<Tag>(this);
            TagsVM.SelectableItems = TreeVM.AllTags;
            TagsVM.SelectedItems = Entry.Tags; //YES! This actually seems to
            //create a BINDING between ThisVM and the Entry.Tags!!!
            //NOW bind to this, in the XAML!
        }

        public void deleteEntry() {
            if (Parent != null) {//IF it has a parent: delete self from children
                Parent.Children.Remove(this);
            }
            else { //else delete self from FirstLevelVMs
                TreeVM.FirstGenEntryVMs.Remove(this);
            }
            DbContext.SaveChanges(); //ALSO lazy. BUT more distributed!
            //Easier you just do these things, IN THE VIEWMODEL!
            //TreeVM.ParentVM.UpdateEntries();
            //REVISIT above it lazy, coz does whole tree, rather than just approp ones.
            //BUT  ok for now.
        }//REMEMBER in your extension Class, to remove it from DbContext.Entries!

        public void EntryVM_PropertyChanged(object sender, PropertyChangedEventArgs args) {
            DbContext.SaveChanges(sender); //THIS clearly does NOTHING for excel. Yes really.
            //INSTEAD, you will need PROBS to OVERRIDE it, in Excel context.
            //THIS will save the selected entry? (VIA submitting an update?)
            //BUT gets tricky if you update a massive selection!
            //TBH! keep it simple! OUGHT to just have a SAVE button...
            //BUT NAH! EVEN THAT complicated, if got to update MANY entries together.
            //WELL NOT THAT complex.Not EF easy though.
        }

        public void Entry_PropertyChanged(object sender, PropertyChangedEventArgs args) {
            //DbContext.SaveChanges();
            //THIS does not seem to get called, not sure why.
            //Never mind, just try with entryVM
            //AHAH! ITs obvious!
            //You have subscribed to the TRANSACTION Entry,
            //NOT the PartClass Entry!
            //BUT NOTE: REVISIT CURRENT
            //I DO think it is better to subscribe to ViewModel,
            //AND NOT the model,
            //BECAUSE model should be PRIVATE, to each viewModel wrapper, I think.
        }

        public virtual void insertEntry(EntryVM selectedVM) {
            //Do nothing
        }

        public void insertEntry(EntryVM entryVM, EntryVM selectedVM) {
            if (Parent != null) {//IF it has a parent: make new one a sibling.
                adoptSibling(entryVM);
            }
            else { //else put it in FirstLevelVMs
                TreeVM.FirstGenEntryVMs.Add(entryVM);
            }
            foreach (Tag tag in FilterTags.SelectedItems) {
                entryVM.Entry.Tags.Add(tag);
            }
            DbContext.SaveChanges();
            //ParentVM.updateEntries(); //not needed? shouldn't be. Not needed for inserting tags...
        }

        public virtual void insertSubEntry(EntryVM selectedVM) {
            //Do nothing
        }

        public void insertSubEntry(EntryVM entryVM, EntryVM selectedVM) {
            adoptChild(entryVM);
            foreach (Tag tag in FilterTags.SelectedItems) {
                entryVM.Entry.Tags.Add(tag);
            }
            DbContext.SaveChanges();
            //ParentVM.updateEntries(); //nec //is it though? well yes. Is there another way?
            //is entry.Children observable? Yes. BUT real issue is: is it adding a VM? No!
            //so make it do so!
            //DONE! CBTL, works, but could be more issues here...
        }

        public override string ToString() {
            return Entry.ToString();
        }

        public static EntryVM WrapInCorrectVM(Entry entry, ITreeVM treeVM) {
            EntryVM entryVM = null;
            if (entry is PartClass)
                entryVM = new PartClassVM(entry as PartClass, treeVM);
            if (entry is PartInstance)
                entryVM = new PartInstanceVM(entry as PartInstance, treeVM);
            if (entry is Task)
                entryVM = new TaskVM(entry as Task, treeVM);
            if (entry is Tag)
                entryVM = new TagVM(entry as Tag, treeVM);
            return entryVM;
        }

    }
}
