using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJT.Models;

namespace CJT.ViewModels {
    public class NodeVM : EntryVM {

        public NodeVM(Node entry, ITreeVM treeVM) {
            //NOTE: this constructor just WRAPS a student in a VM.
            initialize(entry, treeVM);
        }

        public NodeVM(String name, EFContext DbContext) {
            //NOTE: this one creates the Entry, and THEN wraps it!!
            Node newEntry = new Node(name);
            initialize(newEntry, DbContext);
            DbContext.Nodes.Add(newEntry);
        } //WAY FORWARD! Want to move away from TreeVM....

        public NodeVM(String name, ITreeVM treeVM, Entry type) {
            //NOTE: this one creates the Entry, and THEN wraps it!!
            //NOTE: It also ADDs it to the DbContext! ON CONSTRUCTION!
            //MAKE new NODE
            Node newEntry = new Node(name);
            initialize(newEntry, treeVM);
            DbContext.Nodes.Add(newEntry);
            //MAKE new TYPE RELATION!
            Relationship relation = new Relationship(type, newEntry, "Type");
            DbContext.Relationships.Add(relation);
        }

        protected override void initializePropertyList() {
            base.initializePropertyList();
            //revisit
        }

        public EntryVM newNode(string nodeName) {
            Node node = new Node(nodeName);
            Entry = node;
            DbContext.Nodes.Add(node);
            DbContext.SaveChanges();
            return this;
        } //MAYBE PUT types in here, JUST to ensure, it shows up somewhere in list!
          //AND because lets be honest, each entry NEEDS at least one type! or meaningless!

        public override void insertEntry(EntryVM selectedVM) {
            NodeVM entryVM = new NodeVM("blank", TreeVM, TreeVM.TypePanelVM.SelectedItem as Entry);
            insertEntry(entryVM, selectedVM);
            //NEED to give it a TYPE, that matches the selected Type!
            //i.e. the type being viewed, or the type to be applied! (type applicator).
            //I THINK for now at least, it just makes sense to MATCH those being viewed.
            //IF want to create something else (i.e. have an applicator),
            //CAN just open a separate panel... 
            //(makes sense, since probs want to see what have created sofar).
        }

        public override void insertSubEntry(EntryVM selectedVM) {
            NodeVM entryVM = new NodeVM((selectedVM.Entry as Node).Name + " child", TreeVM, TreeVM.TypePanelVM.SelectedItem as Entry);
            //this creates an entry too!
            insertSubEntry(entryVM, selectedVM);
        }

    }
}
