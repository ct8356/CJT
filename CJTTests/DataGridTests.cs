using Microsoft.VisualStudio.TestTools.UnitTesting;
using CJT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJT.Tests {
    [TestClass()]
    public class DataGridTests {
        [TestMethod()]
        public void this_LoadedTest() {
            ExcelContext context = new ExcelContext();
            DataTableVM vm = new DataTableVM();
            vm.DataTable = context.GetDataTable(
                "SELECT [Part number], Manufacturer, Description, SUM(Quant) AS Quant, Condition, Shelf FROM [Sheet1$] " +
            "WHERE FALSE " +
            "GROUP BY [Part number], Manufacturer, Description, Condition, Shelf ");
        }
    }
}