using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using UniversityProject.Services.Interfaces;
using UniversityProject.Services.QueryFilters;

namespace UniversityProject.Services.Services
{
    public class UriService : IUriService
    {

        private readonly IHttpContextAccessor _context;

        public UriService( IHttpContextAccessor context)
        {
            _context = context;
        }

        public Uri GetStudentPaginationUri(StudentQueryFilter filter, int numPag,string route)
        {
            HttpRequest request = _context.HttpContext.Request;
            string baseUrl = String.Concat(request.Scheme, "://",request.Host.ToUriComponent());
            Uri endPoint = new Uri(string.Concat(baseUrl,route));
            string modifiedUri = QueryHelpers.AddQueryString(endPoint.ToString(), "PageNumber", numPag.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "PageSize", filter.PageSize.ToString());
            return new Uri(modifiedUri);
        }
    }
}
