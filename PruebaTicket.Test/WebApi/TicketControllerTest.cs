using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using PruebaTicket.Controllers;
using PruebaTicket.Core.Dto;
using PruebaTicket.Core.Interface;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PruebaTicket.Test
{
    public class Tests
    {
        private StatusCode code;
        private IDatosTicketUseCase useCase;
        private ResponseDto request;
        private List<Ticket> Listaticket;
        private Ticket ticket;
        private Field field;
        private List<Field> listaField;
        private SummaryState summary;
        private List<SummaryState> listaSumary;
        private List<Ticket> listaTicket;

        [SetUp]
        public void Setup()
        {
            code = new StatusCode()
            {
                code = System.Net.HttpStatusCode.OK
            };
            useCase = Substitute.For<IDatosTicketUseCase>();

            request = new ResponseDto()
            {
                Tickets = listaTicket
            };
            listaField = new List<Field>();
            listaSumary = new List<SummaryState>();
            listaTicket = new List<Ticket>();
            listaTicket.Add(ticket);
            field = new Field() { agentId = 1,
                agentName = "carlos panameno",
                company = "sony",
                completed = true,
                priority = "low",
                startDate = DateTime.Now,
                summaryStates = listaSumary
            };

            summary = new SummaryState()
            {
                id = 1,
                name = "Waiting",
                duration = 3000
            };

            listaSumary.Add(summary);
            listaField.Add(field);

            ticket = new Ticket()
            {
                id = 1,
                key = "T1",
                fields = listaField
            };
            
            request = new ResponseDto()
            {
             Tickets = listaTicket
            };
        }

        [TestCase(HttpStatusCode.OK)]
        [TestCase(HttpStatusCode.InternalServerError)]
        public async Task  CuandoGeneroArchivoMetodoGet(HttpStatusCode datos)
        {
            //ARRANGE
            useCase.LeerArchivo().ReturnsForAnyArgs(code);
            //ACT
            ConsultaTicketController controller = new ConsultaTicketController(useCase);
            ObjectResult responseController = (ObjectResult) await controller.CuandoGeneroArchivo();
            //ASSERT
            Assert.AreEqual(responseController.StatusCode.Value, (int)code.code);
        }

        [TestCase(HttpStatusCode.OK)]
        [TestCase(HttpStatusCode.InternalServerError)]
        public async Task CuandoGeneroArchivoMetodoPost(HttpStatusCode datos)
        {
            //ARRANGE
            useCase.DetalleTicket(request).ReturnsForAnyArgs(code);
            //ACT
            ConsultaTicketController controller = new ConsultaTicketController(useCase);
            ObjectResult responseController = (ObjectResult)await controller.CuandoEnvioTickets(request);
            //ASSERT
            Assert.AreEqual(responseController.StatusCode.Value, (int)code.code);
        }

    }
}