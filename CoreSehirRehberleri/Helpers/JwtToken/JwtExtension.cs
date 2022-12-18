using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSehirRehberleri.Helpers.JwtToken
{
    public static class JwtExtension
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("ApplicationError", message);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Acces-Control-Exponse-Header", "Application-Error");
        }
    }
}
