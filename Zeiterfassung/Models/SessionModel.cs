using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zeiterfassung.Models
{
    public class SessionModel
    {
        public string SessionID { get; set; }
        public DateTime Created { get; private set; }
        public string Name { get; set; }
        public SessionModel(string SessionID)
        {
            Created = DateTime.Now;
            this.SessionID = SessionID;
        }
    }
}