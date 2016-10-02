using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CJT {
    public class TextBlock : System.Windows.Controls.TextBlock {
        //NOTE: Don't really need to do it like this,
        //Could just make a template TextBlock in XAML, resources,
        //Then set ControlTemplate to it. Probably more standard way.
        //BUT since TextBlock SO common, putting CJT infront is probs easier.

        public TextBlock() {
            Margin = new Thickness(0);
            Padding = new Thickness(4);
        }

    }
}
