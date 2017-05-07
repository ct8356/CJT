using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel; //this allows INotifyPropertyChanged
using System.Collections.ObjectModel;
using CJT.Models;

namespace CJT.ViewModels {
    public class GenericEntryVM<T,U> 
        : TreeNodeVM where T : BaseEntry, new() where U : DbContext {
        public T Entry { get; set; }
        public U DbContext { get; set; }
        public ObservableCollection<GenericEntryVM<T,U>> Parents { get; set; } //SHOULD BE HERE!
        //NOTE! I think proper way to do this, is to just MODIFY the entry,
        //BUT because the the EntryVM is bound to it, it will update itself accordingly!
        //i.e. this entryVM just has a public Entry Parent.
        //NOW, when that Entry is deleted, the EntryVM deletes itself. SO this EntryVM,
        //does not even need to know its own ParentVM???? maybe... not sure yet.
        public ObservableCollection<GenericEntryVM<T,U>> Children { get; set; }
        //DON'T want others adding to children. If I make it private, will that stop binding?
        //No, it won't, provided you KEEP this property public!!!
        //BUT problem is, then you can still add to Children...issue...


        protected virtual void initialize() {
            //
        }

        protected virtual void initialize(U dbContext) {
            Parents = new ObservableCollection<GenericEntryVM<T, U>>();
            Children = new ObservableCollection<GenericEntryVM<T, U>>();
            DbContext = dbContext;
            initialize();
        }

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
            DbContext.SaveChanges();
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

        public override string ToString() {
            return Entry.ToString();
        }

    }
}
