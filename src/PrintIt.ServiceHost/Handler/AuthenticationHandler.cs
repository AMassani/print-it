//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Net;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace PrintIt.ServiceHost.Handler
//{
//    public class AuthenticationHandler
//    {
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public AuthenticationHandler(IHttpContextAccessor httpContextAccessor)
//        {
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public Task<IActionResult> AuthenticateAsync()
//        {
//            // 1. Look for credentials in the request.
//            var request = _httpContextAccessor.HttpContext.Request;
//            var authorization = request.Headers["Authorization"];

//            // 2. If there are no credentials, do nothing.
//            if (authorization == null)
//            {
//                var result = new AuthenticationFailureResult("Authorization header is 'null''", request);
//                return result;
//            }

//            // 3. If there are credentials but the filter does not recognize the 
//            //    authentication scheme, do nothing.
//            if (!authorization.Scheme.Equals("Bearer"))
//            {
//                context.ErrorResult = new AuthenticationFailureResult("Authentication type must be 'Bearer'", request);
//                return;
//            }

//            // 4. If there are credentials that the filter understands, try to validate them.
//            // 5. If the credentials are bad, set the error result.
//            if (string.IsNullOrEmpty(authorization.Parameter))
//            {
//                context.ErrorResult = new AuthenticationFailureResult("Bearer token is null or empty", request);
//                return;
//            }

//            if (!authorization.Parameter.Equals(_bearerToken))
//            {
//                context.ErrorResult = new AuthenticationFailureResult("Bearer token invalid", request);
//            }
//        }

//    }

//    public class AuthenticationFailureResult : IActionResult
//    {
//        public AuthenticationFailureResult(string reasonPhrase, HttpRequest request)
//        {
//            ReasonPhrase = reasonPhrase;
//            Request = request;
//        }

//        private string ReasonPhrase { get; }

//        private HttpRequest Request { get; }

//        public Task ExecuteResultAsync(ActionContext context)
//        {
//            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
//            context.HttpContext.Response.WriteAsync("Authentication failure - Bearer token is either missing or invalid");

//            return Task.CompletedTask;
//        }
//    }
//}
