using Goldentech.CustomExceptions;
using Goldentech.Enums;
using Goldentech.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Goldentech.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  

        [Route("api/[controller]")]
        //  [ApiController]
        public class BaseApiController : ControllerBase
        {
        
            
            [NonAction]
            public override OkObjectResult Ok(object obj)
            {
                var response = new AdResponse()
                {



                };



                return base.Ok(obj);
            }



            [NonAction]
            public OkObjectResult Ok(object obj, string token)
            {
                var response = new AdResponse()
                {

                };
                return base.Ok(obj);
            }
            [NonAction]
            public OkObjectResult ErrorResponse(Errors error)
            {
                var response = new AdResponse()
                {

                    Data = null,
                    Error = MapToApiError(error)
                };
                return base.Ok(response);
            }
            [NonAction]
            public OkObjectResult ErrorResponse(Exception exception)
            {
                var response = new AdResponse()
                {

                    Data = null
                };
                if (exception is LogicResultException)
                {
                    var exc = exception as LogicResultException;
                    response.Error = new ApiError()
                    {
                        Code = exc.Code.ToString(),
                        Message = exc.Description ?? exc.OriginalDescription ?? "GeneralError"
                    };
                }
                else
                {
                    response.Error = new ApiError()
                    {
                        Code = "-9999",
                        Message = exception.Message
                    };
                }



                return base.Ok(response);
            }
            [NonAction]
            private ApiError MapToApiError(Errors error)
            {
                return new ApiError()
                {
                    Code = ((int)error).ToString(),
                    Message = error.ToString()
                };
            }
            [NonAction]
            [ApiExplorerSettings(IgnoreApi = true)]
            public virtual async Task<T> GetAsync<T>(string url)
            {



                try
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    var res = await client.SendAsync(request);
                    var data = await res.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(data, new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    });
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            [NonAction]
            [ApiExplorerSettings(IgnoreApi = true)]
            public virtual async Task<T> PostAsync<T>(string url, object body) where T : class
            {
                try
                {



                    var client = new HttpClient();
                    HttpResponseMessage response = await client.PostAsync(url, body == null ? null : new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json"));



                    if (!response.IsSuccessStatusCode)
                    {
                        return new AdResponse<string>() { Data = response.RequestMessage.RequestUri.ToString() } as T; //todo return
                    }



                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(jsonResponse, new JsonSerializerSettings
                    {
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    });
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        











    }
}
