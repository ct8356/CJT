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
        public string TemplateName { get; set; }
        public DataTable DataTable { get; set; }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem) {
            string path = (Binding as Binding).Path.Path;
            var binding = new Binding(path);
            binding.Source = dataItem;
            var textBlock = new TextBlock();
            textBlock.SetBinding(TextBlock.TextProperty, binding);
            return textBlock;
        }

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
