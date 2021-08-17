using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goldentech.Enums
{
   
        public enum Errors
    {
            //General Errors
        GeneralError = 0,
        DocumentNotValid = 1,
        NotValidDocumentType=2,
        BusinessPartnerIsNotActive = 3,
        TypeVIsNotValid4SalesDocumentType=4,
        TypeCIsNotValid4PurchaseDocumentType = 5,
        InvalidDocumentWithoutLines = 6,
        SomeItemsNotActive = 7,
        DocumentNotExist=8
    }
    public enum ApiServices
    {
        
        
    }
}
