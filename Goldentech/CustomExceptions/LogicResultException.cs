using Goldentech.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goldentech.CustomExceptions
{
    



        public class LogicResultException : Exception
        {
            public ApiServices ApiName { get; set; }
            public int Code { get; set; }
            public string Description { get; set; }
            public string OriginalDescription { get; set; }



            public LogicResultException(ApiServices ApiName, int Code, string CustomDescription) : base(CustomDescription)
            {
                this.ApiName = ApiName;
                this.Code = Code;
                this.Description = CustomDescription;
            }



            public LogicResultException(ApiServices ApiName, int Code, string CustomDescription, string OriginalDescription) : base(CustomDescription)
            {
                this.ApiName = ApiName;
                this.Code = Code;
                this.Description = CustomDescription;
                this.OriginalDescription = OriginalDescription;
            }
        
    





}
}
