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
    public class EntriesTreeVM : BaseClass, ITreeVM {
        public IMainVM ParentVM { get; set; }
        public DbContext DbContext { get; set; }
        Type type;
        public EntryVM FilterEntryVM { get; set; }
        public ComboBoxVM NRPanelVM { get; set; } //Node Relation
        public ComboBoxVM TypePanelVM { get; set; }
        public ComboBoxVM GroupPanelVM { get; set; }
        public ComboBoxVM StructurePanelVM { get; set; }
        public IQueryable<Entry> FilteredEntries { get; set; } //for now, for reference only.
        public ObservableCollection<EntryVM> FirstGenEntryVMs { get; set; }
        //needed for treeview to bind to
        public ObservableCollection<EntryVM> AllEntryVMs { get; set; }
        //needed to keep track of EntryVMs shown in the treeView (without having to scan tree).
        int childLevel;
        public Entry Orphan { get; set; }
        public bool WaitingForParentSelection { get; set; }

        public EntriesTreeVM(IMainVM parentVM) {
            //NOTE: maybe best NOT to pass it parentVM.
            //ACCESS it some other way.
            //BECAUSE, means you need to SPECIFY the TYPE of parentVM...
            //NOT very flexible!
            ParentVM = parentVM;
            DbContext = parentVM.DbContext;
            //New stuff
            FilterEntryVM = new EntryVM(this);
            //FILTER PANELS
            NRPanelVM = new ComboBoxVM();
            NRPanelVM.Items = new ObservableCollection<object> {
                typeof(Node), typeof(Relationship) };
            NRPanelVM.SelectedItem = NRPanelVM.Items.First();
            NRPanelVM.PropertyChanged += FilterChanged;
            TypePanelVM = new ComboBoxVM();
            //Get entries where Relation "Type" == Type!
            IQueryable<Entry> types = DbContext.Entries.Where(
                e => e.ParentRelations.Where(r => r.Name == "Type")
                .Select(r => r.ParentEntry.Name).Contains("Type")
            ).Distinct();
            TypePanelVM.Items = new ObservableCollection<object>(types);
            TypePanelVM.SelectedItem = TypePanelVM.Items.First(); 
            TypePanelVM.PropertyChanged += FilterChanged;
            //GROUPING AND STRUCTURING PANELS
            GroupPanelVM = new ComboBoxVM();
            GroupPanelVM.PropertyChanged += StructureChanged;
            StructurePanelVM = new ComboBoxVM();
            StructurePanelVM.PropertyChanged += StructureChanged;
            //OLD STUFF
            FirstGenEntryVMs = new ObservableCollection<EntryVM>();
            AllEntryVMs = new ObservableCollection<EntryVM>();
            UpdateEntries();
            //filterToDos();
            //INITIALIZE
            //newEntry.LostFocus += new RoutedEventHandler(newEntry_LostFocus);
            //newEntry.KeyUp += new KeyEventHandler(newEntry_KeyUp);
        }

        protected IQueryable<Entry> getFilteredEntries() {
            IQueryable<Entry> value = null;
            if (NRPanelVM.SelectedItem != null) {
                Type selectedType = NRPanelVM.SelectedItem as Type; 
                if (selectedType == typeof(Entry)) {
                    int count = DbContext.Entries.Count();
                    value = filterEntries<Entry>(DbContext.Entries);
                }
                if (selectedType == typeof(Node))
                    value = filterEntries<Node>(DbContext.Nodes);
                if (selectedType == typeof(Relationship))//"is" does not work here. it says selected type is Type.
                    value = filterEntries<Relationship>(DbContext.Relationships);
            }
            return value;
        }

        public IQueryable<T> filterByType<T>(IQueryable<T> filteredEntries) where T : Entry {
            //string typeName = ((displayPanel.TypePanel.DataContext as TypePanelVM).SelectedObjects.First() as Type).FullName;
            //Type type = Type.GetType(typeName);
            //Type type = TypePanelVM.SelectedItem as Type;
            string type = TypePanelVM.SelectedItem.ToString();
            filteredEntries = filteredEntries.Where(
                e => e.ParentRelations.Where(r => r.Name == "Type")
                .Select(r => r.ParentEntry.Name).Contains(type)
            );
            //filteredEntries = filteredEntries.AsEnumerable<Entry>().Where(e => e.GetType() == type);
            //AHAH! Think the above is necessary, IF want to do GetType (reflection) in a query!
            return filteredEntries;
        }//LATER if decide it takes too long to do several queries,
         //can turn this method into a "construct SQL" query, maybe...

        protected IQueryable<T> filterByTag<T>(IQueryable<T> entries) where T : Entry {
            return entries
            //.Include(e => e.ParentRelations)
            //.Include(e => e.ParentRelations.Select(r => r.ParentEntry))
            ;//WOW! How strange, getting rid of these includes,
            //Seemed to speed it up loads! How weird!
            //SO maybe, doing lots of little queries, really is not a problem?
            //INFACT, better, than doing one big query?
            //(even if the query ISN'T that big!)
            //WELL ACTUALLY not THAT much faster. Hard to tell really.
            //STILL, getting relations, and including all ENDS of relations,
            //GOT to be faster, than getting entries, and getting all kids right?
            //WELL maybe, maybe not, as you discovered... (drawn on envelope).

            //IT STILL takes a long time to load, after compiling though.
            //WHY IS THAT? Quite a simple program really.
            //Maybe it loads lots of irrelevant stuff?
        }

        protected IQueryable<T> filterEntries<T>(IQueryable<T> entries) where T : Entry {
            //int count = entries.Count();
            IQueryable<T> filteredEntries = entries;
            filteredEntries = filterByType(filteredEntries);
            //int count2 = filteredEntries.Count();
            filteredEntries = filterByTag<T>(filteredEntries);
            //orderBy(); //does nothing yet
            return filteredEntries;           
        }

        public void FilterChanged(object sender, EventArgs e) {
            UpdateEntries();
        }

        protected void showLevels(IQueryable<Entry> filteredEntries) {
            string groupPropertyName = GroupPanelVM.SelectedItem as string;
            string structurePropertyName = StructurePanelVM.SelectedItem as string;
            if (GroupPanelVM.IsApplied == true) {
                IQueryable<Entry> groups = showGroups(filteredEntries, groupPropertyName);
                IQueryable<Entry> filteredParents = showMoreLevels(filteredEntries, groups, groupPropertyName);
            }
            else if (GroupPanelVM.IsApplied == false) {
                IQueryable<Entry> filteredParents = showFirstLevel(filteredEntries, structurePropertyName);
                //Expression<Func<T, bool>> expression = PropertyNotNull<T>(columnName); //Won't work with relations
                for (int gen = 2; gen <= 3; gen++) {
                    filteredParents = showMoreLevels(filteredEntries, filteredParents, structurePropertyName);
                }
            }
        }

        protected IQueryable<Entry> showGroups(IQueryable<Entry> entries, string propertyName) {
            IQueryable<Entry> groups = null;
            //groups = entries.Where(e => e.ParentRelations //NOTE
            //RATHER than getting filteredEntries, then trying to almagate a List of Lists of Genders,
            //(COZ not sure how to do that elegantly!)
            //just get ANY Entries, that match some criteria, e.g.
            groups = DbContext.Entries.Where(
                e => e.ChildRelations.Where(r => r.Name == propertyName).Select(r => r.ChildEntry)
                .Intersect(entries).Any()
            );
            foreach (Entry entry in groups) {
                EntryVM entryVM = EntryVM.WrapInCorrectVM(entry, this);
                FirstGenEntryVMs.Add(entryVM);
                AllEntryVMs.Add(entryVM);
            }
            return groups;
        }

        protected IQueryable<Entry> showFirstLevel(IQueryable<Entry> entries, string columnName) {
            //GET RELATIONS
            //basically, get any relations, where BOTH entries, have correct tags.
            //GET ENTRIES
            IQueryable<Entry> filteredParents = null;
            filteredParents = entries.Except(
                entries.Where(
                    e => e.ParentRelations.Where(r => r.Name == columnName).Select(r => r.ParentEntry)
                    .Intersect(entries).Any()
                )
            );//YES IT WORKS!
            //AHAH! Won't work for type, since its looking for entries WITHOUT type as parent.
            //BUT OF COURSE! ALL TYPES HAVE TYPE AS PARENT! 
            //SO ITS A SPECIAL CASE! NEEDS a special treatment if statement!!! CURRENT!
            int count0 = entries.Count();
            int count = filteredParents.Count();
            foreach (Entry entry in filteredParents) {
                EntryVM entryVM = EntryVM.WrapInCorrectVM(entry, this); //SHOULD make this into Constructor really.
                FirstGenEntryVMs.Add(entryVM);
                AllEntryVMs.Add(entryVM);
            }
            return filteredParents;
        }

        protected IQueryable<T> showFirstLevel1<T>(IQueryable<T> entries, string columnName) where T : Entry {
            //NOTE! Its quite possible that the TreeView might be able to do all this logic! REVISIT!
            IQueryable<T> filteredParents = null;
            Type selectedType = TypePanelVM.SelectedItem as Type;
            filteredParents = entries.Except(
                entries.Where(
                    e => e.ParentRelations.Where(r => r.Name == columnName).Select(r => r.ParentEntry)
                    .Intersect(entries).Any()
                )
            );//YES IT WORKS!
            //NOTE: IF I can figure out how to hardfix primitive properties to the entry,
            //THEN this RELATIONSHIPS way, should be just as good as neo4j, or the SQL hardcoded way, I think! 
            //SINCE, all relationships, would have to be done via JOINS anyway!
            int count0 = entries.Count();
            int count = filteredParents.Count();
            foreach (T entry in filteredParents) {
                EntryVM entryVM = EntryVM.WrapInCorrectVM(entry, this); //SHOULD make this into Constructor really.
                FirstGenEntryVMs.Add(entryVM);
                AllEntryVMs.Add(entryVM);
            }
            return filteredParents;
        }

        protected IQueryable<Entry> showMoreLevels(IQueryable<Entry> entries, IQueryable<Entry> filteredParents,
        string columnName) {
            IQueryable<Entry> filteredChildren = null;
            filteredChildren = entries.Where(
                e => e.ParentRelations.Where(r => r.Name == columnName).Select(r => r.ParentEntry)
                .Intersect(filteredParents).Any()
            );
            foreach (Entry child in filteredChildren) {
                int count = AllEntryVMs.Count();
                IEnumerable<EntryVM> parentVMs = AllEntryVMs.Where(
                        evm => child.ParentRelations.Where(r => r.Name == columnName).Select(r => r.ParentEntry)
                        .Contains(evm.Entry));
                EntryVM childVM = EntryVM.WrapInCorrectVM(child, this);
                foreach (EntryVM parentVM in parentVMs) {
                    parentVM.Children.Add(childVM);
                    childVM.Parents.Add(parentVM);
                }
                //OH YEAH! Queryables can take expressions, Enumerables must take delegates!
                AllEntryVMs.Add(childVM);
            }
            return filteredChildren;
        }

        protected IQueryable<T> showMoreLevels1<T>(IQueryable<T> entries, IQueryable<T> filteredParents,
        string columnName) where T : Entry {
            IQueryable<T> filteredChildren = null;
            IEnumerable<int> parentEntryIDs = filteredParents.Select(e => e.EntryID);
            //filteredChildren = entries.Where(e => parentEntryIDs.Contains(e.Sensor.EntryID));
            //filteredChildren = entries.Where(EnumContainsPropertyEntryID<T>(parentEntryIDs, columnName));
            //NOTE! innefficiency above, in that it does this for parents too...
            //perhaps in previous cycle, could REMOVE parents. once placed. BUT then they
            //only show once. MIGHT not want that.
            //NOTE! NOW, what we want to do,
            //IS find all FILTERED entries, that overlap with filteredParents CHILDREN!
            //OR find all FILTERED entries, whose PARENTS overlap with filtered parents!
            //OR similarly, find all filteredParents children, that fit the filter...
            filteredChildren = entries.Where(
                    e => e.ParentRelations.Where(r => r.Name == columnName).Select(r => r.ParentEntry)
                    .Intersect(filteredParents).Any()
                ).Include(e => e.ParentRelations).Include(e => e.ParentRelations.Select(r => r.ParentEntry)); 
            foreach (T child in filteredChildren) {
                //if (hasParent(child)) { //Won't work with Relations (since not a property)
                //BUT surely BOUND to have a parent!??? if filtered children???
                //try {
                //EntryVM parentVM = AllEntryVMs.Where(eVM => eVM.Entry == child.Sensor).Single();
                //EntryVM parentVM = AllEntryVMs.Where(EntryEqualsChildsProperty<T>(child, columnName).Compile()).First();
                //here, we want to find entryVM where Entry equals childs "Parent" parent.
                //OR EVEN, find all entryVMs where Entry overlaps with child's "Parent" parents.
                //OR where child's "Parent" parents CONTAIN the entryVM's entry.
                int count = AllEntryVMs.Count(); //CURRENT: CLEARLY a problem that some of these are null!
                IEnumerable<EntryVM> parentVMs = AllEntryVMs.Where(
                        evm => child.ParentRelations.Where(r => r.Name == columnName).Select(r => r.ParentEntry)
                        .Contains(evm.Entry));
                    EntryVM childVM = EntryVM.WrapInCorrectVM(child, this);
                    foreach (EntryVM parentVM in parentVMs) {
                        parentVM.Children.Add(childVM);
                        childVM.Parents.Add(parentVM);
                    }
                    //OH YEAH! Queryables can take expressions, Enumerables must take delegates!
                    AllEntryVMs.Add(childVM);
                //}
                //catch (Exception e) {
                    //Do nothing
                //}
                //}
            }
            return filteredChildren;
        }

        //public void showUntaggedEntries() {
        //    IQueryable<Task> untaggedStudents = filteredStudents.Where(s => !s.Tags.Any()); //Will this work?
        //    foreach (Task student in untaggedStudents) {
        //    }
        //}

        public void StructureChanged(object sender, EventArgs e) {
            RefreshView();
        }

        public void UpdateEntries() { //Update and refresh are synonyms i think.
            FilteredEntries = getFilteredEntries();
            //GROUPING SETTINGS
            IQueryable<string> properties = DbContext.Relationships.Where(
                r => FilteredEntries.Contains(r.ChildEntry)
            ).Select(r => r.Name).Distinct();
            GroupPanelVM.Items.Clear();
            foreach (string property in properties) {
                GroupPanelVM.Items.Add(property);
            }
            //GroupPanelVM.Items = new ObservableCollection<object>(properties); 
            //NOTE, does creating new, break the binding?
            //IT WOULD SEEM SO!
            GroupPanelVM.SelectedItem = GroupPanelVM.Items.First();
            //ADD some clever logic here, to say, if selectedItem still contained, keep it.
            //IF not, then set it to first again.
            //STRUCTURE BY
            IQueryable<string> properties2 = DbContext.Relationships
                .Where(r => FilteredEntries.Contains(r.ChildEntry))
                //.Where(r => r.ChildEntry.ParentRelations
                    //.Where(re => re.ParentEntry.Name == "Type").Intersect(TypePanelVM.Items).Any())
                .Select(r => r.Name).Distinct(); //ONLY wants to be, those relations,
            //that point to entries, of same type as the entry(s) in TYPE PANEL VM!
            StructurePanelVM.Items.Clear();
            foreach (string property in properties2) {
                StructurePanelVM.Items.Add(property);
            }
            StructurePanelVM.SelectedItem = StructurePanelVM.Items.First();
            RefreshView();
        }

        public void RefreshView() {
            FirstGenEntryVMs.Clear();
            AllEntryVMs.Clear();
            showLevels(FilteredEntries);
        }

        public void waitForParentSelection(Entry entry) {
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
