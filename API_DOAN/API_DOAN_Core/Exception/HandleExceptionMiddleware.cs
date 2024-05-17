using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using System.Net;
using API_DOAN_Core.DTOs;

namespace API_DOAN_Core.Exception
{
    public class HandleExceptionMiddleware
    {
        private RequestDelegate _next;
        public HandleExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DOANValidateException ex)
            {
                var serviceResult = new DOANServiceResult();
                serviceResult.userMsg = API_DOAN_Core.Resource.Resource1.Exception_UserMsg; 
                serviceResult.Errors.Add(ex.Message);
                var res = JsonConvert.SerializeObject(serviceResult);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(res);
            }
            catch (System.Exception ex)
            {
                var serviceResult = new DOANServiceResult();
                serviceResult.userMsg = API_DOAN_Core.Resource.Resource1.Exception_UserMsg;
                serviceResult.devMsg = ex.Message;
                serviceResult.Errors.Add(ex.Message);
                var res = JsonConvert.SerializeObject(serviceResult);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(res);
            }

        }
    }
}
