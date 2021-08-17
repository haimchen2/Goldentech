using Goldentech.Contracts;
using Goldentech.Enums;
using Goldentech.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goldentech.Controllers
{
    [Route("api")]
    [ApiController]
    public class GoldentechController : BaseApiController
    {

        private readonly IGoldentechService goldentechService;



        public GoldentechController(IGoldentechService goldentechService) 
        {
           
            this.goldentechService = goldentechService;
        }
       

        [Route("ReadItems"), HttpPost]
        public async Task<IActionResult> ReadItems([FromBody] GetItemsReq getItemsReq)
        {
            try
            {

                var  result= await goldentechService.GetItems( getItemsReq);
                return Ok(result);
            }
            catch (Exception exc)
            {
                  return ErrorResponse(exc);
            }
         
        }
        [Route("ReadBusinessPartners"), HttpPost]
        public async Task<IActionResult> ReadBusinessPartners([FromBody] GetItemsReq getItemsReq)
        {
            try
            {
                var result = await goldentechService.GetBusinessPartners(getItemsReq);
                return Ok(result);
            }
            catch (Exception exc)
            {
                  return ErrorResponse(exc);
            }
           
        }
        [Route("AddDocument"), HttpPost]
        public async Task<IActionResult> AddDocument([FromBody] DocumentReq model)
        {

            try
            {
               
                if (!(model.DocumentType == "Sales" || model.DocumentType == "Purchase"))
                    return ErrorResponse(Errors.NotValidDocumentType);
                if (!goldentechService.GetBPEntity(model).Result.Active)
                    return ErrorResponse(Errors.BusinessPartnerIsNotActive);
                if (model.DocumentType== "Sales" && goldentechService.GetBPEntity(model).Result.BPType=="V")
                    return ErrorResponse(Errors.TypeVIsNotValid4SalesDocumentType);
                if (model.DocumentType == "Purchase" && goldentechService.GetBPEntity(model).Result.BPType == "C")
                    return ErrorResponse(Errors.TypeCIsNotValid4PurchaseDocumentType);
                if (model.ordersLines!=null && model.ordersLines.Count==0)
                    return ErrorResponse(Errors.InvalidDocumentWithoutLines);
                if (!goldentechService.ItemsValidtion(model).Result)
                    return ErrorResponse(Errors.SomeItemsNotActive);
                var result = await goldentechService.AddDocument(model);
                return Ok(result);
            }
            catch (Exception exc)
            {
                  return ErrorResponse(exc);
            }
        }
        [Route("UpdateDocument"), HttpPost]
        public async Task<IActionResult> UpdateDocument([FromBody] DocumentReq model)
        {
            try
            {
                if (!(model.DocumentType == "Sales" || model.DocumentType == "Purchase"))
                    return ErrorResponse(Errors.NotValidDocumentType);
                if (!goldentechService.GetBPEntity(model).Result.Active)
                    return ErrorResponse(Errors.BusinessPartnerIsNotActive);
                if (model.DocumentType == "Sales" && goldentechService.GetBPEntity(model).Result.BPType == "V")
                    return ErrorResponse(Errors.TypeVIsNotValid4SalesDocumentType);
                if (model.DocumentType == "Purchase" && goldentechService.GetBPEntity(model).Result.BPType == "C")
                    return ErrorResponse(Errors.TypeCIsNotValid4PurchaseDocumentType);
                if (model.ordersLines != null && model.ordersLines.Count == 0)
                    return ErrorResponse(Errors.InvalidDocumentWithoutLines);
                if (!goldentechService.ItemsValidtion(model).Result)
                    return ErrorResponse(Errors.SomeItemsNotActive);
                var result = await goldentechService.UpdateDocument(model);
                if (result==null)
                    return ErrorResponse(Errors.DocumentNotExist);
                return Ok(result);
            }
            catch (Exception exc)
            {
                  return ErrorResponse(exc);
            }
           
        }
       
        [Route("DeleteDocument"), HttpPost]
        public async Task<IActionResult> DeleteDocument([FromBody] DocumentReq model)
        {
            try
            {
                if (!(model.DocumentType == "Sales"|| model.DocumentType == "Purchase"))
                    return ErrorResponse(Errors.NotValidDocumentType);
             
                var result = await goldentechService.DeleteDocument(model);
                if (result == null)
                    return ErrorResponse(Errors.DocumentNotExist);
                return Ok(result);
            }
            catch (Exception exc)
            {
                  return ErrorResponse(exc);
            }
          
        }
        [Route("GetDocument"), HttpPost]
        public async Task<IActionResult> GetDocument([FromBody] DocumentReq model)
        {
            try
            {

                if (!(model.DocumentType == "Sales" || model.DocumentType == "Purchase"))
                    return ErrorResponse(Errors.NotValidDocumentType);
                var result = await goldentechService.GetDocument(model);
                if (result == null)
                    return ErrorResponse(Errors.DocumentNotExist);
                return Ok(result);
            }
            catch (Exception exc)
            {
                  return ErrorResponse(exc);
            }
           
        }
    }
}
