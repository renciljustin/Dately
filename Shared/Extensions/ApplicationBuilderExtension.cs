using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Dately.Shared.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseExceptionHandler(builder => {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";

                        var features = context.Features.Get<IExceptionHandlerFeature>();

                        if (context != null)
                        {
                            context.Response.Headers.Add("Application-Error", features.Error.Message);
                            context.Response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
                            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                            await context.Response.WriteAsync(JsonConvert.SerializeObject(features.Error.Message));
                        }
                    });
                });
        }
    }
}