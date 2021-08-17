using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goldentech.Models.Request
{
    public class GetItemsReq
    {
        public FilterBy filterBy { get; set; }
        public Paging paging { get; set; }
    }
    public class GetItemsRes<T>
    {
        public IQueryable<T> items { get; set; }
        public int? pages { get; set; }

    }
    public class FilterBy
    {
        public string key { get; set; }
        public string value { get; set; }
    }
    public class Paging
    {
        public int pageNum { get; set; }
        public int pageSize { get; set; }
    }
}
