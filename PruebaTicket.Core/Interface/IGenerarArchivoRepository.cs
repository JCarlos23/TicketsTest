using PruebaTicket.Core.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTicket.Core.Interface
{
    public interface IGenerarArchivoRepository
    {
        Task<StatusCode> GenerarArchivo(ResponseDto datos, string estado);
    }
}
