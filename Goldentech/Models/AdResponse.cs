using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goldentech.Models
{
    public class AdResponse
    {
        public object Data { get; set; }

        public ApiError Error { get; set; }




    }



    public class AdResponse<T> : AdResponse
    {
        public new T Data { get; set; }
    }



    public class ApiError
    {
        public string Code { get; set; }



        public string Message { get; set; }
    }
}
