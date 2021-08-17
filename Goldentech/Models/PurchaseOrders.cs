using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goldentech.Models
{
    public class PurchaseOrders
    {
        
  


            public int ID { get; set; }
        public string BPCode { get; set; }
        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
    }
    public class SaleOrders
    {




        public int ID { get; set; }
        public string BPCode { get; set; }
        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
    }
}
