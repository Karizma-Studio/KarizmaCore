using KarizmaPlatform.Core.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KarizmaPlatform.Core.Utils;

internal static class ControllerExtension
{
    public static async Task<IActionResult> ToActionResult<T>(this ControllerBase controllerBase,
        Func<Task<ServiceResult<T>>> action, ILogger logger)
    {
        try
        {
            var result = await action();
            return result.Status switch
            {
                ResultStatus.Success => controllerBase.Ok(result.Data),
                ResultStatus.NoContent => controllerBase.NoContent(),
                ResultStatus.Invalid => controllerBase.BadRequest(result.Message),
                ResultStatus.Forbidden => controllerBase.StatusCode(StatusCodes.Status403Forbidden, new { error = result.Message }),
                ResultStatus.Error => controllerBase.StatusCode(StatusCodes.Status500InternalServerError, new { error = result.Message }), 
                _ => HandleUnexpected(result, controllerBase, logger)
            };
        }
        catch (Exception e)
        {
            logger.LogError($"Unexpected Error -> {e}");
            return controllerBase.StatusCode(StatusCodes.Status500InternalServerError, new { error = e.Message ?? "Internal Server Error" });
        }
    }
    
    private static IActionResult HandleUnexpected<T>(
        ServiceResult<T> result,
        ControllerBase controllerBase,
        ILogger logger)
    {
        logger.LogError("Unexpected error: {Message}", result.Message);
        return controllerBase.StatusCode(StatusCodes.Status500InternalServerError,
            new { error = result.Message ?? "Internal Server Error" });
    } 
}