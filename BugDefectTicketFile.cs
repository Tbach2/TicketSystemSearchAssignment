using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace TicketingSystem
{
    public class BugDefectTicketFile
    {
        public string filePath { get; set; }
        public List<BugDefect> BugDefectTickets { get; set; }
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        public BugDefectTicketFile(string BugDefectTicketFilePath)
        {
            filePath = BugDefectTicketFilePath;
            BugDefectTickets = new List<BugDefect>();

            try
            {
                StreamReader sr = new StreamReader(filePath);
                while (!sr.EndOfStream)
                {
                    
                    string line = sr.ReadLine();
                    string[] ticketDetails = line.Split(',');

                    if (ticketDetails.Length == 8)
                    {
                        BugDefect bugDefect = new BugDefect();

                        bugDefect.ticketId = UInt64.Parse(ticketDetails[0]);
                        bugDefect.summary = ticketDetails[1];
                        bugDefect.status = ticketDetails[2];
                        bugDefect.priority = ticketDetails[3];
                        bugDefect.submitter = ticketDetails[4];
                        bugDefect.assigned = ticketDetails[5];
                        bugDefect.watching = ticketDetails[6].Split('|').ToList();
                        bugDefect.severity = ticketDetails[7];
                        BugDefectTickets.Add(bugDefect);
                    }
                }
                sr.Close();
                logger.Info("Tickets in file: {Count}", BugDefectTickets.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AddBugDefect(BugDefect bugDefect)
        {
            try
            {
                bugDefect.ticketId = BugDefectTickets.Max(m => m.ticketId) + 1;
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{bugDefect.ticketId},{bugDefect.summary},{bugDefect.status},{bugDefect.priority},{bugDefect.submitter},{bugDefect.assigned},{string.Join("|", bugDefect.watching)},{bugDefect.severity}");
                sw.Close();
                BugDefectTickets.Add(bugDefect);
                logger.Info("Ticket ID {Id} added", bugDefect.ticketId);
            } 
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}