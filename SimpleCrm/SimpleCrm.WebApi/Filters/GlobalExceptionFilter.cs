using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCrm.WebApi.Filters
{
    public class GlobalExceptionFilter: IExceptionFilter, IDisposable
          {
        // TODO: you may want to inject an ILogger and write messages in the OnException method below.
        //private readonly ILogger _logger;
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
            
            
        }

    public void Dispose() { }

    public void OnException(ExceptionContext context)
    {

        // Check for ApiException in context.Exception, 
        if (context.Exception is ApiException )
            {
                var apiException = (ApiException)context.Exception;

                context.Result = new ObjectResult(apiException.Model) { StatusCode = apiException.StatusCode };
            }
            
        else
            {
                context.Result = new ObjectResult("This is an Error") { StatusCode = 500 };
            }
        // - if present use its status code and model in the object result below
        // - if another type, pick a default status code and anything you prefer for the model

        // no return type, instead you set the context.Result as last line in this method
        
    }
}

    }

