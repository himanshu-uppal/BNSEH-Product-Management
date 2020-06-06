using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using ProductManager.Common;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.MVC.ActionFilterFeature
{
    public class AccessTokenActionFilterAttribute : ActionFilterAttribute
    {
        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    var principal = context.HttpContext.User as ClaimsPrincipal;

        //    var accessTokenClaim = principal?.Claims
        //      .FirstOrDefault(c => c.Type == "access_token");

        //    if (accessTokenClaim is null || string.IsNullOrEmpty(accessTokenClaim.Value))
        //    {
        //        context.HttpContext.Response.Redirect("/login", permanent: true);

        //        return;
        //    }

        //    var sharedKey = Encoding.ASCII.GetBytes(Constants.SECRET);

        //    var validationParameters = new TokenValidationParameters
        //    {

        //         ValidateIssuerSigningKey = true,
        //        ValidateIssuer = false,
        //        IssuerSigningKey = new SymmetricSecurityKey(sharedKey),
        //        ValidateAudience = false
        //    };

        //    var accessToken = accessTokenClaim.Value;

        //    var handler = new JwtSecurityTokenHandler();

        //    var user = (ClaimsPrincipal)null;

        //    try
        //    {
        //        user = handler.ValidateToken(accessToken, validationParameters, out SecurityToken validatedToken);
        //    }
        //    catch (SecurityTokenValidationException exception)
        //    {
        //        throw new Exception($"Token failed validation: {exception.Message}");
        //    }

        //    base.OnActionExecuting(context);
        //}
    }
}
