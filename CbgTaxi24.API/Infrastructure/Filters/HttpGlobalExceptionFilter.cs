﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using CbgTaxi24.API.Infrastructure.ActionResults;
using CbgTaxi24.API.Infrastructure.Exceptions;

namespace CbgTaxi24.API.Infrastructure.Filters
{
    public class HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger, IWebHostEnvironment env) : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);

            if (context.Exception is PlatformException exception)
            {
                var statusCode = exception.CustomStatusCode != null ? exception.CustomStatusCode : StatusCodes.Status400BadRequest;
                var problemDetails = new
                {
                    Title = exception.Message,
                    Instance = context.HttpContext.Request.Path,
                    Status = statusCode,
                    Detail = "Please refer to the errors property for additional details.",
                    Errors = exception.Errors.ToArray()
                };

                context.Result = new ObjectResult(problemDetails) { StatusCode = statusCode };
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                var json = new JsonErrorResponse
                {
                    Messages = ["An error occured.Try it again."]
                };

                if (env.IsDevelopment())
                {
                    json.DeveloperMessage = new
                    {
                        ClassName = context.Exception.GetType().FullName,
                        context.Exception.Message,
                        context.Exception.Data,
                        InnerException = context.Exception.InnerException?.Message,
                        context.Exception.StackTrace,
                        context.Exception.Source,
                        context.Exception.HResult
                    };
                }

                context.Result = new InternalServerErrorObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }

    public record JsonErrorResponse
    {
        public string[] Messages { get; set; } = default!;

        public object DeveloperMessage { get; set; } = new object();
    }
}
