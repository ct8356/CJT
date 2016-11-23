using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJT.Models;

namespace CJT.ViewModels {
    public class NoteVM : EntryVM {

        public NoteVM(Note entry, ITreeVM treeVM) {
            //NOTE: this constructor just WRAPS a student in a VM.
            initialize(entry, treeVM);
        }

        public NoteVM(String name, ITreeVM treeVM) {
            //NOTE: this one creates the Entry, and THEN wraps it!!!
            Note newEntry = new Note(name);
            initialize(newEntry, treeVM);
            DbContext.Notes.Add(newEntry);
        }

        public void initialize(Note entry, ITreeVM treeVM) {
            base.initialize(entry, treeVM, DbContext);
        }

        protected override void initializePropertyList() {
            base.initializePropertyList();
            //revisit
        }

        public override void insertEntry(EntryVM selectedVM) {
            NoteVM entryVM = new NoteVM("blank", TreeVM);
            insertEntry(entryVM, selectedVM);
        }

        public override void insertSubEntry(EntryVM selectedVM) {
            NoteVM entryVM = new NoteVM((selectedVM.Entry as Note).Name + " child", TreeVM);
            //this creates an entry too!
            insertSubEntry(entryVM, selectedVM);
        }

    }
}
