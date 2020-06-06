using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManager.MVC.ActionFilterFeature
{
    //Custom Authentication Filter to check whether the user is authenticated to access the functionality meant to be used by only authenticated users
    public class AuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //if the current session's user value is not null and has something it    
            int? isLoggedIn = context.HttpContext.Session.GetInt32("isLoggedIn");
            if (isLoggedIn == 1)
            {


                // !!! check whether the user key is valid or not and then check whether the user is valid or not !!!                

            }
            else
            {
                //set the result to Unauthorized
                context.Result = new RedirectResult("~/Home/Index");
            }
            //Check Session is Empty Then set as Result is HttpUnauthorizedResult 
            return;
        }
    }

        //Runs after the OnAuthentication method  
        //------------//
        //OnAuthenticationChallenge:- if Method gets called when Authentication or Authorization is 
        //failed and this method is called after
        //Execution of Action Method but before rendering of View
        //------------//
     
    }
