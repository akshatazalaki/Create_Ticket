using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Create_Ticket.Assest
{
    public class AddTicket
    {
        public int Id { get; set; }
        public int TaskName { get; set; }
        public int AssignedBy { get; set; }
        public int AssignedTo { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime StatusDate { get; set; }
        public int StatusName { get; set; }
        public int Priority { get; set; }
    }
}