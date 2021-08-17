using Goldentech.Models;
using Goldentech.Models.Request;
using Goldentech.Models.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Goldentech.Contracts
{
   public interface IGoldentechService
    {
        Task<GetItemsRes<Items>> GetItems(GetItemsReq getItemsReq);
        Task<GetItemsRes<BusinessPartners>> GetBusinessPartners(GetItemsReq getItemsReq);
        Task<DocumentRes> AddDocument(DocumentReq model);

        Task<DocumentRes> UpdateDocument(DocumentReq model);

        Task<DocumentRes> DeleteDocument(DocumentReq model);

        Task<DocumentRes> GetDocument(DocumentReq model);
        Task<BusinessPartners> GetBPEntity(DocumentReq model);
        Task<bool> ItemsValidtion(DocumentReq model);
    }
}
