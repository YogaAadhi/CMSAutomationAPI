using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace CMSAutomationAPI.Model.APIResponseBase
{
    public static class ResponseExtension
    {
        public static IActionResult ToResponse<TModel>(this IListModelResponse<TModel> response, HttpStatusCode status = HttpStatusCode.OK)
        {


            if (response.Model == null && status == HttpStatusCode.OK)
            {
                status = HttpStatusCode.NoContent;
            }

            return new ObjectResult(response) { StatusCode = (Int32)status };
        }

        public static IActionResult ToResponse<TModel>(this ISingleModelResponse<TModel> response,
            HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {


            return new ObjectResult(response) { StatusCode = (Int32)httpStatusCode };
        }      

    }
}
    