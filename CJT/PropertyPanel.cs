using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using TextBlock = CJT.TextBlock;

namespace CJT {
    public class PropertyPanel : StackPanel {

        public TextBlock TextBlock { get; set; }

        public PropertyPanel() {
            Orientation = Orientation.Horizontal;
        }

        public PropertyPanel(string name) : this() {
            TextBlock = new TextBlock();
            TextBlock.Text = name + ":";
            TextBlock.Width = 100;
            Children.Add(TextBlock);
        }

        public PropertyPanel(string name, Control control) : this(name) {
            Children.Add(control);
        }

        protected void initialize(string name, Control control) {
            Orientation = Orientation.Horizontal;
            TextBlock = new TextBlock();
            TextBlock.Text = name + ":";
            TextBlock.Width = 100;
            Children.Add(TextBlock);
            Children.Add(control);
        }//THIS is a prime example of where initialize is better than constructors.

    }
}
