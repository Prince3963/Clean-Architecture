using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.HelperServices
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string? Error { get; set; }
    }

    public class DeleteServiceResponse<T>
    {
        public bool Success { get; set; } = true;
        public string? Error { get; set; }
        public string? Message { get; set; }

    }
}
