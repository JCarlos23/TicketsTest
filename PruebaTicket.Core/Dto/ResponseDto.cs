using System;
using System.Collections.Generic;
using System.Text;

namespace PruebaTicket.Core.Dto
{
    public class ResponseDto
    {
        public List<Ticket> Tickets { get; set; }
    }

    public class Ticket
    {
        public int id { get; set; }
        public string key { get; set; }
        public List<Field> fields { get; set; }
    }

    public class Field
    {
        public int agentId { get; set; }
        public string agentName { get; set; }
        public DateTime startDate { get; set; }
        public string type { get; set; }
        public string priority { get; set; }
        public string company { get; set; }
        public bool completed { get; set; }
        public int totalDuration { get; set; }
        public List<SummaryState> summaryStates { get; set; }
    }

    public class SummaryState
    {
        public int id { get; set; }
        public string name { get; set; }
        public int duration { get; set; }
    }
}
