

using Backend.Modules.BFF.Application.Interfaces;

namespace Backend.Modules.BFF.Infrastructure.Middlewares
{
    public class SesionTokenMiddleware
    {


        private readonly RequestDelegate _next;

        public SesionTokenMiddleware(RequestDelegate next)
        {
            _next = next;

        }


        public async Task InvokeAsync(HttpContext context, ITokenProxyService proxyService)
        {
            var sessionId = context.Request.Cookies["session_id"];
            if (!string.IsNullOrEmpty(sessionId))
            {
                var tokenDto = await proxyService.GetValidTokenAsync(sessionId);
                if (tokenDto != null)
                {
                    context.Request.Headers["Authorization"] = $"Bearer {tokenDto.AccessToken}";
                }
                else
                {
                    context.Response.Cookies.Delete("session_id");
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;

                }
            }
            await _next(context);
        }
    }
}