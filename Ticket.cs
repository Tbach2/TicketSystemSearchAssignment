using System;
using System.Collections.Generic;

namespace TicketingSystem
{
    public abstract class Ticket
    {
        public UInt64 ticketId { get; set; }
        public string summary { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public string submitter { get; set; }
        public string assigned { get; set; }
        public List<string> watching { get; set; }
    
        public Ticket()
        {
            watching = new List<string>();
        }

        public virtual string Display()
        {
            return $"Ticket ID: {ticketId}\nSummary: {summary}\nStatus: {status}\nPriority: {priority}\nSubmitter: {submitter}\nAssigned: {assigned}\nWatching: {string.Join(", ", watching)}\n";
        }
    }
    public class BugDefect : Ticket
    {
        public string severity { get; set; }
        public override string Display(){
            return $"Ticket ID: {ticketId}\nSummary: {summary}\nStatus: {status}\nPriority: {priority}\nSubmitter: {submitter}\nAssigned: {assigned}\nWatching: {string.Join(", ", watching)}\nSeverity: {severity}\n";
        }
    }
        public class Enhancement : Ticket
    {
        public string software { get; set; }
        public string cost { get; set; }
        public string reason { get; set; }
        public string estimate { get; set; }
        public override string Display(){
            return $"Ticket ID: {ticketId}\nSummary: {summary}\nStatus: {status}\nPriority: {priority}\nSubmitter: {submitter}\nAssigned: {assigned}\nWatching: {string.Join(", ", watching)}\nSoftware: {software}\nCost: {cost}\nReason: {reason}\nEstimate: {estimate}\n";
        }
    }
            public class Task : Ticket
    {
        public string projectName { get; set; }
        public string dueDate { get; set; }
        public override string Display(){
            return $"Ticket ID: {ticketId}\nSummary: {summary}\nStatus: {status}\nPriority: {priority}\nSubmitter: {submitter}\nAssigned: {assigned}\nWatching: {string.Join(", ", watching)}\nProject Name: {projectName}\nDue Date: {dueDate}\n";
        }
    }
}