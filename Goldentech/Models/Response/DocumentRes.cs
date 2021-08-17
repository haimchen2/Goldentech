using Goldentech.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goldentech.Models.Response
{
    public class DocumentRes
    {
        public string DocumentType { get; set; }
        public string BPCode { get; set; }
        public int CreatedBy { get; set; }
        public int LastUpdatedBy { get; set; }
        public List<OrdersLine> ordersLines { get; set; }

        public string 	BPName  { get; set; }
        public string CreatedByFullName { get; set; }
        public string LastUpdatedByFullName { get; set; }
        public bool Active { get; set; }

        
    }
}
