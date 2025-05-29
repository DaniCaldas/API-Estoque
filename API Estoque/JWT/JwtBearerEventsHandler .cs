using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API_Estoque.JWT;
public class JwtBearerEventsHandler: JwtBearerEvents
{
    private readonly ILogger<JwtBearerEventsHandler> _logger;

    public JwtBearerEventsHandler(ILogger<JwtBearerEventsHandler> logger)
    {
        _logger = logger;
    }
    public override Task AuthenticationFailed(AuthenticationFailedContext context)
    {
        _logger.LogError(context.Exception, "Falha na autenticação JWT");
        return base.AuthenticationFailed(context);
    }

    public override Task TokenValidated(TokenValidatedContext context)
    {
        _logger.LogInformation("Token validado para: {User}",
            context.Principal?.Identity?.Name ?? "anonymous");
        return base.TokenValidated(context);
    }

    public override Task Forbidden(ForbiddenContext context)
    {
        _logger.LogWarning("Acesso negado para: {Path}",
            context.HttpContext.Request.Path);
        return base.Forbidden(context);
    }
}
