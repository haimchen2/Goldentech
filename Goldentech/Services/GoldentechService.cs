using AutoMapper;
using Goldentech.Contracts;
using Goldentech.Data;
using Goldentech.Models;
using Goldentech.Models.Request;
using Goldentech.Models.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Goldentech.Services
{
   
    public class GoldentechService: IGoldentechService
    {
        private readonly ApplicationSqlDbContext _ApplicationSqlDbContext;
        private readonly IMapper _mapper;
        public GoldentechService(ApplicationSqlDbContext _ApplicationSqlDbContext, IMapper mapper)
        {
            this._ApplicationSqlDbContext = _ApplicationSqlDbContext;
            _mapper = mapper;
        }
       
         
        
        public async Task<GetItemsRes<Items>> GetItems(GetItemsReq getItemsReq)
        {
            try
            {
              
                    var  allData= this._ApplicationSqlDbContext.Set<Items>();
                    var data = getItemsReq.filterBy == null ? allData : this._ApplicationSqlDbContext.Items.FromSqlRaw<Items>($"select * from items where  {getItemsReq.filterBy.key} = '{ getItemsReq.filterBy.value }'");
                
                    if (getItemsReq.paging == null)
                        return new GetItemsRes<Items> { items = data };

                    var pageSize = getItemsReq.paging.pageSize;
                    var pagingData = data.Skip(pageSize * (getItemsReq.paging.pageNum - 1)).Take(pageSize);
                    var pagesCount = data.Count() % getItemsReq.paging.pageSize == 0 ? data.Count() / getItemsReq.paging.pageSize : data.Count() / getItemsReq.paging.pageSize + 1;

                await Task.Run(() =>
                {
                    
                });

                return new GetItemsRes<Items> { items = pagingData, pages = pagesCount };
            }
            catch (Exception ex)
            {
                 throw ex;
            }
          //  return null;
        }

        public async Task<GetItemsRes<BusinessPartners>> GetBusinessPartners(GetItemsReq getItemsReq)
        {
            try
            {
                var allData = this._ApplicationSqlDbContext.Set<BusinessPartners>();
                var data = getItemsReq.filterBy == null ? allData : this._ApplicationSqlDbContext.BusinessPartners.FromSqlRaw<BusinessPartners>($"select * from BusinessPartners where  {getItemsReq.filterBy.key} = '{ getItemsReq.filterBy.value }'");
              
                if( data.Count()==0)
                return new GetItemsRes<BusinessPartners> { items = null };
             
                if (getItemsReq.paging == null)
                    return new GetItemsRes<BusinessPartners> { items = data };

                var pageSize = getItemsReq.paging.pageSize;
                var pagingData = data.Skip(pageSize * (getItemsReq.paging.pageNum - 1)).Take(pageSize);
                var pagesCount = data.Count() % getItemsReq.paging.pageSize == 0 ? data.Count() / getItemsReq.paging.pageSize : data.Count() / getItemsReq.paging.pageSize + 1;
                return new GetItemsRes<BusinessPartners> { items = pagingData, pages = pagesCount };

            }
            catch (Exception ex)
            {
                return new GetItemsRes<BusinessPartners> { items = null };
                throw ex;
            }

        }

        public async Task<BusinessPartners> GetBPEntity(DocumentReq model)
        {
            try
            {

                return await this._ApplicationSqlDbContext.BusinessPartners.Where(x => x.BPCode == model.BPCode).FirstOrDefaultAsync();
            }
            catch {
                return null;
            }
          
        }

        public async Task<bool> ItemsValidtion (DocumentReq model)
        {
            try
            {
                Items res = null;
                foreach (var i in model.ordersLines)
                {
                     res = await this._ApplicationSqlDbContext.Items.Where(x => x.ItemCode == i.ItemCode).FirstOrDefaultAsync();

                    if(!res.Active)
                          return false;
                }
                return true;
            }
            catch
            {
                return false;
            }

        }
       
        public async Task<DocumentRes> AddDocument(DocumentReq model)
        {
            try
            {
                if (model.DocumentType == "Sales")
                {
                    var saleOrder = new SaleOrders()
                    {
                        BPCode = model.BPCode,
                        CreatedBy = model.CreatedBy,
                       // LastUpdatedBy = model.CreatedBy
                    };

                    this._ApplicationSqlDbContext.SaleOrders.Add(saleOrder);
                    this._ApplicationSqlDbContext.SaveChanges();

                    foreach (var i in model.ordersLines)
                    {
                        var saleOrdersLines = new SaleOrdersLines()
                        {
                            DocID = saleOrder.ID,
                            ItemCode = i.ItemCode,
                            Quantity = i.Quantity
                        };
                        
                        this._ApplicationSqlDbContext.SaleOrdersLines.Add(saleOrdersLines);
                        this._ApplicationSqlDbContext.SaveChanges();
                        if (i.Comments != null && i.Comments.Count > 0)
                            foreach (var j in i.Comments)
                        {
                            var saleOrdersLinesComments = new SaleOrdersLinesComments()
                            {
                                DocID = saleOrder.ID,
                                LineID= saleOrdersLines.LineID,
                                Comment=j.Comment
                                                            };
                            this._ApplicationSqlDbContext.SaleOrdersLinesComments.Add(saleOrdersLinesComments);
                            this._ApplicationSqlDbContext.SaveChanges();
                        }
                    }
                }


                if (model.DocumentType== "Purchase")
                {
                    var purchaseOrders = new PurchaseOrders() {
                        BPCode = model.BPCode,
                        CreatedBy=model.CreatedBy,
                     
                    };

                    this._ApplicationSqlDbContext.PurchaseOrders.Add(purchaseOrders);
                    this._ApplicationSqlDbContext.SaveChanges();

                    foreach (var i in model.ordersLines)
                    {
                        var purchaseOrdersLines = new PurchaseOrdersLines()
                        {
                           DocID= purchaseOrders.ID,
                           ItemCode=i.ItemCode,
                           Quantity=i.Quantity
                        };

                        this._ApplicationSqlDbContext.PurchaseOrdersLines.Add(purchaseOrdersLines);
                        this._ApplicationSqlDbContext.SaveChanges();
                    }
                }
                var res = _mapper.Map<DocumentRes>(model);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<DocumentRes> UpdateDocument(DocumentReq model)
        {
            try
            {
                if (model.DocumentType == "Sales")
                {
                    var saleOrder = this._ApplicationSqlDbContext.SaleOrders.Where(x => x.ID == model.DocumentId).FirstOrDefault();
                    if (saleOrder == null)
                        return null;

                    if (!string.IsNullOrEmpty(model.BPCode)) saleOrder.BPCode = model.BPCode;
                    if (model.CreatedBy != null) saleOrder.CreatedBy = model.CreatedBy;
                     saleOrder.LastUpdatedBy = model.CreatedBy;

                    this._ApplicationSqlDbContext.SaleOrders.Update(saleOrder);
                    this._ApplicationSqlDbContext.SaveChanges();

                    foreach (var i in model.ordersLines)
                    {
                        var ordersLine = this._ApplicationSqlDbContext.SaleOrdersLines.Where(x => x.DocID == saleOrder.ID && x.ItemCode == i.ItemCode).FirstOrDefault();
                        ordersLine.Quantity = i.Quantity;

                        this._ApplicationSqlDbContext.SaleOrdersLines.Update(ordersLine);
                        this._ApplicationSqlDbContext.SaveChanges();
                        if (i.Comments!=null && i.Comments.Count>0)
                        foreach (var j in i.Comments)
                        {
                                var comment = this._ApplicationSqlDbContext.SaleOrdersLinesComments.Where(x => x.DocID == model.DocumentId && x.LineID == saleOrder.ID).FirstOrDefault();
                                if (comment == null) continue;
                                comment.Comment = j.Comment;
                         
                            this._ApplicationSqlDbContext.SaleOrdersLinesComments.Update(comment);
                            this._ApplicationSqlDbContext.SaveChanges();
                        }

                    }

                }
                if (model.DocumentType == "Purchase")
                {
                  var purchaseOrder=  this._ApplicationSqlDbContext.PurchaseOrders.Where(x => x.ID == model.DocumentId).FirstOrDefault();
                    if (purchaseOrder == null)
                        return null;

              if (!string.IsNullOrEmpty (model.BPCode))      purchaseOrder.BPCode = model.BPCode;
                    if (model.CreatedBy != null) purchaseOrder.CreatedBy = model.CreatedBy;
                   purchaseOrder.LastUpdatedBy = model.CreatedBy;
                  
                    this._ApplicationSqlDbContext.PurchaseOrders.Update(purchaseOrder);
                    this._ApplicationSqlDbContext.SaveChanges();

                    //delete items that removed
                    foreach (var i in this._ApplicationSqlDbContext.PurchaseOrdersLines.Where(x => x.DocID== model.DocumentId).ToList())
                    {
                    var  isItemExist=   model.ordersLines.Where(x => x.ItemCode == i.ItemCode).FirstOrDefault();
                        if (isItemExist==null)
                        {

                            this._ApplicationSqlDbContext.Attach(i);
                            this._ApplicationSqlDbContext.PurchaseOrdersLines.Remove(i);
                            this._ApplicationSqlDbContext.SaveChanges();

                        }

                    }
                    foreach (var i in model.ordersLines)
                    {
                        var ordersLine = this._ApplicationSqlDbContext.PurchaseOrdersLines.Where(x => x.DocID == purchaseOrder.ID && x.ItemCode==i.ItemCode).FirstOrDefault();
                        //add new items
                        if (ordersLine==null)
                        {
                            var purchaseOrdersLines = new PurchaseOrdersLines()
                            {
                                DocID = model.DocumentId,
                                ItemCode = i.ItemCode,
                                Quantity = i.Quantity
                            };

                            this._ApplicationSqlDbContext.PurchaseOrdersLines.Add(purchaseOrdersLines);
                            this._ApplicationSqlDbContext.SaveChanges();
                            continue;
                        }
                       

                        ordersLine.Quantity = i.Quantity;
                        this._ApplicationSqlDbContext.PurchaseOrdersLines.Update(ordersLine);
                        this._ApplicationSqlDbContext.SaveChanges();

                    }

                }
                var res = _mapper.Map<DocumentRes>(model);
                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

   

        public async Task<DocumentRes> DeleteDocument(DocumentReq model)
        {
            try
            {
                if (model.DocumentType == "Sales")
                {
                    
                    var ordersLines = this._ApplicationSqlDbContext.SaleOrdersLines.Where(x => x.DocID == model.DocumentId);

                    foreach (var j in ordersLines.ToList())
                    {
                        var ordersLinesComments = this._ApplicationSqlDbContext.SaleOrdersLinesComments.Where(x => x.DocID == model.DocumentId && x.LineID == j.LineID);
                        foreach (var c in ordersLinesComments.ToList())
                        {
                            this._ApplicationSqlDbContext.Attach(c);
                            this._ApplicationSqlDbContext.SaleOrdersLinesComments.Remove(c);
                            this._ApplicationSqlDbContext.SaveChanges();
                        }

                            this._ApplicationSqlDbContext.Attach(j);
                        this._ApplicationSqlDbContext.SaleOrdersLines.Remove(j);
                        this._ApplicationSqlDbContext.SaveChanges();
                    }

                    var saleOrder = this._ApplicationSqlDbContext.SaleOrders.Where(x => x.ID == model.DocumentId).FirstOrDefault();
                    if (saleOrder == null)
                        return null;

                    this._ApplicationSqlDbContext.SaleOrders.Remove(saleOrder);
                    this._ApplicationSqlDbContext.SaveChanges();

                }
                if (model.DocumentType == "Purchase")
                {
                        var ordersLines = this._ApplicationSqlDbContext.PurchaseOrdersLines.Where(x => x.DocID == model.DocumentId);
                        foreach (var j in ordersLines.ToList())
                        {
                            this._ApplicationSqlDbContext.Attach(j);
                            this._ApplicationSqlDbContext.PurchaseOrdersLines.Remove(j);
                            this._ApplicationSqlDbContext.SaveChanges();
                        }

                    var purchaseOrder = this._ApplicationSqlDbContext.PurchaseOrders.Where(x => x.ID == model.DocumentId).FirstOrDefault();
                    if (purchaseOrder == null)
                        return null;

                    this._ApplicationSqlDbContext.PurchaseOrders.Remove(purchaseOrder);
                    this._ApplicationSqlDbContext.SaveChanges();

                }
                var res = _mapper.Map<DocumentRes>(model);
                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DocumentRes> GetDocument(DocumentReq model)
        {
            try
            {
                var res = new DocumentRes();
                if (model.DocumentType == "Sales")
                {
                    var saleOrder = this._ApplicationSqlDbContext.SaleOrders.Where(x => x.ID == model.DocumentId).FirstOrDefault();
                    if (saleOrder == null)
                        return null;
                    res.BPName = this._ApplicationSqlDbContext.BusinessPartners.Where(x => x.BPCode == saleOrder.BPCode).FirstOrDefault().BPName;
                    res.Active = this._ApplicationSqlDbContext.BusinessPartners.Where(x => x.BPCode == saleOrder.BPCode).FirstOrDefault().Active;
                    res.CreatedByFullName = this._ApplicationSqlDbContext.Users.Where(x => x.ID == saleOrder.CreatedBy).FirstOrDefault().FullName;
                    res.LastUpdatedByFullName = (this._ApplicationSqlDbContext.Users.Where(x => x.ID == saleOrder.LastUpdatedBy).FirstOrDefault() != null) ? this._ApplicationSqlDbContext.Users.Where(x => x.ID == saleOrder.LastUpdatedBy).FirstOrDefault().FullName:"";
                    res.BPCode = saleOrder.BPCode;

                    var ordersLines = this._ApplicationSqlDbContext.SaleOrdersLines.Where(x => x.DocID == model.DocumentId);
                    res.ordersLines = new List<OrdersLine>();
                    var comments = new List<Comments>();
                    foreach (var j in ordersLines.ToList())
                    {
                        var ordersLinesComments = this._ApplicationSqlDbContext.SaleOrdersLinesComments.Where(x => x.DocID == model.DocumentId && x.LineID == j.LineID);
                     
                        foreach (var o in ordersLinesComments)
                        {
                            comments.Add(new Comments { Comment = o.Comment });
                        }
                        
                        res.ordersLines.Add(new OrdersLine
                        {
                            ItemName = this._ApplicationSqlDbContext.Items.Where(x => x.ItemCode == j.ItemCode).FirstOrDefault().ItemName,
                            Active = this._ApplicationSqlDbContext.Items.Where(x => x.ItemCode == j.ItemCode).FirstOrDefault().Active,
                            ItemCode = j.ItemCode,
                            Quantity = j.Quantity,
                            Comments= comments
                        }); ;
                    }

                }
                if (model.DocumentType == "Purchase")
                {
                    var purchaseOrder = this._ApplicationSqlDbContext.PurchaseOrders.Where(x => x.ID == model.DocumentId).FirstOrDefault();
                    if (purchaseOrder == null)
                        return null;
                    
                    res.BPName = this._ApplicationSqlDbContext.BusinessPartners.Where(x => x.BPCode == purchaseOrder.BPCode).FirstOrDefault().BPName;
                    res.Active = this._ApplicationSqlDbContext.BusinessPartners.Where(x => x.BPCode == purchaseOrder.BPCode).FirstOrDefault().Active;
                    res.CreatedByFullName = this._ApplicationSqlDbContext.Users.Where(x => x.ID == purchaseOrder.CreatedBy).FirstOrDefault().FullName;
                    res.LastUpdatedByFullName = (this._ApplicationSqlDbContext.Users.Where(x => x.ID == purchaseOrder.LastUpdatedBy).FirstOrDefault()!=null) ?this._ApplicationSqlDbContext.Users.Where(x => x.ID == purchaseOrder.LastUpdatedBy).FirstOrDefault().FullName:"";
                    res.BPCode = purchaseOrder.BPCode;
                    
                    var ordersLines = this._ApplicationSqlDbContext.PurchaseOrdersLines.Where(x => x.DocID == model.DocumentId);
                    res.ordersLines = new List<OrdersLine>();
                    foreach (var j in ordersLines.ToList())
                    {
                       
                        res.ordersLines.Add(new OrdersLine
                        {
                            ItemName = this._ApplicationSqlDbContext.Items.Where(x => x.ItemCode == j.ItemCode).FirstOrDefault().ItemName,
                            Active = this._ApplicationSqlDbContext.Items.Where(x => x.ItemCode == j.ItemCode).FirstOrDefault().Active,
                            ItemCode=j.ItemCode,
                            Quantity=j.Quantity
                            
                        }); ;

                    }

                }
          
                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DocumentReq, DocumentRes>(); //Map from Developer Object to DeveloperDTO Object
        }
    }
}
