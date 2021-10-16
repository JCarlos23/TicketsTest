using PruebaTicket.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTicket.Core.Interface
{
    public interface IDatosTicketUseCase
    {
        Task<StatusCode> LeerArchivo();
        Task<StatusCode> DetalleTicket(ResponseDto datos);
    }
}
