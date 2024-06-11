using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Create_Ticket.Assest
{
    public class Parameters
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public int AssignedBy { get; set; }
        public int AssignedTo { get; set; }

        public DateTime AssignedDate { get; set; }
        public DateTime StatusDate { get; set; }
        public int StatusName { get; set; }
        public int Priority { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

    }
}