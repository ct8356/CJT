using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace CJT {
    public class CustomBoundColumn : DataGridBoundColumn {
        //Probs won't need to use this class.
        //Problem is, it goes funny if you have a scroll bar on the table.
        public DataTable DataTable { get; set; }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem) {
            string path = (Binding as Binding).Path.Path;
            var binding = new Binding(path);
            binding.Source = dataItem;
            var textBlock = new TextBlock();
            textBlock.SetBinding(TextBlock.TextProperty, binding);
            return textBlock;
        } //PROBLEM MUST BE HERE???? must be to do with dataItem passed!
        //What is a DataRowView? Is it part of DataTable? Yes its the view of a row in a DataTable.
        //As expected really. So the textBlock should stay connected TO THAT ROW!. But it does not. WHY?
        //OK, so this GenerateElement method, is done corrrectly I think (similar to many others online).
        //BUT it just really cant handle scrolling! 
        //SO could spend hours figuring out why,
        //OR could just use NON-editable ComboBoxes, (or not comboboxes at all, probs best),
        //YES DUH! Should really just edit in EXCEL if need to!
        //BUT if gonna evolve to SQL, probs best to have facility to edit.
        //SO first of all, IN SQL, transactions should reference a PART CLASS! Quicker editing).
        //TBH, that is only instance would want to edit MULTIPLE rows all at once. YES!
        //SO rest of edits, could be done with ComboBoxes, and then right click to add new option!
        //I like it! BUT it needs to be EDITABLE to allow TYPING! yes TRUE!
        //FUD! BUT cause only want to edit ONE at a time, can RIGHT CLICK IT, to edit it!
        //YEAH WHY NOT! GET THIS BAD BOY DONE! LIKE RICH WOULD LIKE IT! DONE QUICK!

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem) {
            string path = (Binding as Binding).Path.Path;
            var binding = new Binding(path);
            binding.Source = dataItem;
            List<string> list = DataTable.AsEnumerable().Select(r => r[path].ToString())
                .Distinct().Where(c => c != "").OrderBy(c => c).ToList<string>();
            //I would actually prefer it if did the above only when I clicked on cell.
            var comboBox = new ComboBox();
            comboBox.IsEditable = true;
            //comboBox.templ = (DataTemplate)cell.FindResource(TemplateName); //even nec?
            comboBox.SetValue(ComboBox.ItemsSourceProperty, list); //UseComboBox rather than ItemsControl. More specific!
            comboBox.SetBinding(ComboBox.TextProperty, binding); //YES! works. do NOT want ContentControl.ContentProperty.
            return comboBox; //NOTE: may have to use template selector if want other than comboBox.
        }

    }
}
