using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goldentech.Models.Request
{
    public class DocumentReq
    {
        public int DocumentId { get; set; }
        public string DocumentType { get; set; }
        public string BPCode { get; set; }
        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public List<OrdersLine> ordersLines { get; set; }
     //   public string DocumentType { get; set; }
    }
    
    public class OrdersLine
    {
        public string ItemCode { get; set; }
        public decimal Quantity { get; set; }
        public string ItemName { get; set; }
        public bool Active  { get; set; }

        public List<Comments> Comments { get; set; }
    }
    public class Comments
    {
        public string Comment { get; set; }
    }
    }
