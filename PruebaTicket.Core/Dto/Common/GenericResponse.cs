using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace PruebaTicket.Core.Dto
{
    public class GenericResponse<T>
    {
        public StatusCode status { get; set; }
        public T item { get; set; }
    }

    public class StatusCode
    {
        public HttpStatusCode code { get; set; }
        public string description { get; set; }
    }
}
