using System;
using System.Linq;
using CandidateEvaluator.Server.Models;
using Microsoft.AspNetCore.Http;

namespace CandidateEvaluator.Server.Extensions
{
    public static class HttpContextExtensions
    {
        public static User GetUser(this HttpContext context)
        {
            var user = new User();
            var id = context?.User?.Claims?.FirstOrDefault(x =>
                x.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            if (Guid.TryParse(id, out var userId))
            {
                user.Oid = userId;
            }
            user.Name = context?.User?.Claims?.FirstOrDefault(x =>
                x.Type == "name")?.Value;
            user.Email = context?.User?.Claims?.FirstOrDefault(x =>
                x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

            return user;
        }
    }
}
