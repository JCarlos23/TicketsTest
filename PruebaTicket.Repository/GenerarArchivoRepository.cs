using PruebaTicket.Core.Dto;
using PruebaTicket.Core.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTicket.Repository
{
    public class GenerarArchivoRepository : IGenerarArchivoRepository
    {
        public string convertirArchivo(MemoryStream memory)
        {

                string archivoTicket = Encoding.ASCII.GetString(memory.ToArray());
                byte[] archivoTransformado = Encoding.UTF8.GetBytes(archivoTicket);
                var encodedArchivo = Convert.ToBase64String(archivoTransformado);
                string archivoBase64 = Convert.ToString(encodedArchivo);
                return archivoBase64;
        }

        public async Task<StatusCode> GenerarArchivo(ResponseDto datos, string estado)
        {
            StatusCode response;
           
            try
            {
                string nombre = string.Empty;
                if (estado == "Get")
                     nombre = "outPutGet.txt";
                else
                    nombre = "outPutPost.txt";
                using (StreamWriter writer = new StreamWriter(nombre))
                {
                   await writer.WriteAsync("ticketID, key, agentName, startDate, type, priority, company, completed, totalDuration, open, inProgress, waiting, internalValidation");

                    string ticketID = string.Empty, key = string.Empty, agentName = string.Empty, startDate = string.Empty, type = string.Empty,
                                      priority = string.Empty, company = string.Empty, completed = string.Empty, totalDuration = string.Empty, open = string.Empty,
                                      inProgress = string.Empty, waiting = string.Empty, internalValidation = string.Empty;
                    foreach (var recorrido in datos.Tickets)
                    {
                        ticketID = Convert.ToString(recorrido.id);
                        key = Convert.ToString(recorrido.key);
                     await   writer.WriteAsync("\r\n");
                        foreach (var field in recorrido.fields)
                        {
                            agentName = field.agentName;
                            startDate = Convert.ToString(field.startDate);
                            type = field.type;
                            priority = field.priority;
                            company = field.company;
                            completed = Convert.ToString(field.completed);
                            totalDuration = Convert.ToString(field.totalDuration);
                            foreach (var summary in field.summaryStates)
                            {
                                if (summary.name == "Open")
                                    open = Convert.ToString(summary.duration);
                                else if (summary.name == "In Progress")
                                    inProgress = Convert.ToString(summary.duration);
                                else if (summary.name == "Waiting")
                                    waiting = Convert.ToString(summary.duration);
                                else
                                    internalValidation = Convert.ToString(summary.duration);

                            }
                        }
                      await  writer.WriteAsync($"{ticketID}, {key}, {agentName}, {startDate}, {type}, {priority}, " +
                            $"{company}, {completed}, {totalDuration}, {open}, {inProgress}, " +
                            $"{waiting}, {internalValidation}");
                     await   writer.FlushAsync();
                    }

                }

                response = new StatusCode()
                {
                    code = HttpStatusCode.OK, 
                    description = "Generado con exito, ruta donde se encuentra los archivos generados es la siguiente: PruebaTicket\\PruebaTicket"
                };
            }catch(Exception ex)
            {
                response = new StatusCode
                {
                    code = HttpStatusCode.InternalServerError, description = $"Error {ex.Message}"
                };
            }
            return response;
        }


    }
           
}
