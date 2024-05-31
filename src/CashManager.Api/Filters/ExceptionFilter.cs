using CashManager.Application.Interfaces;
using CashManager.Communication.Responses;
using CashManager.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Resources;

namespace CashManager.Api.Filters;

public class ExceptionFilter(IResourceManagerProvider resourceManagerProvider) : IExceptionFilter
{
    private readonly IResourceManagerProvider _resourceManagerProvider = resourceManagerProvider;

    public void OnException(ExceptionContext context)
    {
        var resourceManager = _resourceManagerProvider.GetResourceManager();

        if (context.Exception is CashManagerException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnknownError(context, resourceManager);
        }
    }

    private static void HandleProjectException(ExceptionContext context)
    {
        if (context.Exception is ErrorOnValidationException ex)
        {
            var errorResponse = new ResponseError(ex.Errors);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorResponse);
        }
        else
        {
            var errorResponse = new ResponseError(context.Exception.Message);

            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Result = new BadRequestObjectResult(errorResponse);
        }
    }

    private static void ThrowUnknownError(ExceptionContext context, ResourceManager resourceManager)
    {
        var errorResponse = new ResponseError(resourceManager.GetString("InternalServerError") ?? "INTERNAL_SERVER_ERROR");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}
