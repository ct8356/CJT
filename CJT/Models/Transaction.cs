using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJT.Models {
    public class Transaction : Entry {

        public virtual PartClass PartClass { get; set; }

        int quantity = 0;
        public int Quantity {
            get { return quantity; }
            set { quantity = value; NotifyPropertyChanged("Quantity"); }
        }

        string location = "Undefined";
        public string Location {
            get { return location; }
            set { location = value; NotifyPropertyChanged("Location"); }
        }

        string condition = "Undefined";
        public string Condition {
            get { return condition; }
            set { condition = value; NotifyPropertyChanged("Condition"); }
        }

    }
}
