using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goldentech.Models
{
    public class PurchaseOrdersLines
    {
        public int LineID { get; set; }
        public int DocID { get; set; }
        public string ItemCode { get; set; }
        public decimal Quantity { get; set; }
    }
    public class SaleOrdersLines
    {
        public int LineID { get; set; }
        public int DocID { get; set; }
        public string ItemCode { get; set; }
        public decimal Quantity { get; set; }
    }
    public class SaleOrdersLinesComments
    {
        public int LineID { get; set; }
        public int DocID { get; set; }
        public int  CommentLineID { get; set; }
        public string Comment { get; set; }
    }
}
