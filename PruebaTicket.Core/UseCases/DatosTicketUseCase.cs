using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PruebaTicket.Core.Dto;
using PruebaTicket.Core.Dto.Common;
using PruebaTicket.Core.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTicket.Core.UseCases
{
    public class DatosTicketUseCase : IDatosTicketUseCase
    {
        private readonly IGenerarArchivoRepository _repsository;
        private readonly ConfigUrls _parametros;
        public DatosTicketUseCase(IGenerarArchivoRepository repsository, IOptions<ConfigUrls> parametros)
        {
            _parametros = parametros.Value;
            _repsository = repsository ?? throw new ArgumentNullException(nameof(repsository));
        }
        public async Task<StatusCode> LeerArchivo()
        {
            string estado = _parametros.estadoGet;
            ResponseDto response = new ResponseDto();
            var ruta = Path.Combine(_parametros.ruta);
            var json = await File.ReadAllTextAsync(ruta);
            var convertirJson = JsonConvert.DeserializeObject<ResponseDto>(json);
            var archivoGenerado =  await _repsository.GenerarArchivo(convertirJson, estado);
            return archivoGenerado;
        }
        
        public async Task<StatusCode> DetalleTicket(ResponseDto datos)
        {
            string  estado  = _parametros.estadoPost;
            var archivoGenerado = await _repsository.GenerarArchivo(datos, estado);
            return archivoGenerado;
        }
    }
}
