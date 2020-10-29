using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace TicketingSystem
{
    public class EnhancementTicketFile
    {
        public string filePath { get; set; }
        public List<Enhancement> EnhancementTickets { get; set; }
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        public EnhancementTicketFile(string EnhancementTicketFilePath)
        {
            filePath = EnhancementTicketFilePath;
            EnhancementTickets = new List<Enhancement>();

            try
            {
                StreamReader sr = new StreamReader(filePath);
                while (!sr.EndOfStream)
                {
                    
                    string line = sr.ReadLine();
                    string[] ticketDetails = line.Split(',');

                    if (ticketDetails.Length == 11)
                    {
                        Enhancement enhancement = new Enhancement();

                        enhancement.ticketId = UInt64.Parse(ticketDetails[0]);
                        enhancement.summary = ticketDetails[1];
                        enhancement.status = ticketDetails[2];
                        enhancement.priority = ticketDetails[3];
                        enhancement.submitter = ticketDetails[4];
                        enhancement.assigned = ticketDetails[5];
                        enhancement.watching = ticketDetails[6].Split('|').ToList();
                        enhancement.software = ticketDetails[7];
                        enhancement.cost = ticketDetails[8];
                        enhancement.reason = ticketDetails[9];
                        enhancement.estimate = ticketDetails[10];
                        EnhancementTickets.Add(enhancement);
                    }
                }
                sr.Close();
                logger.Info("Tickets in file: {Count}", EnhancementTickets.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AddEnhancemant(Enhancement enhancement)
        {
            try
            {
                enhancement.ticketId = EnhancementTickets.Max(m => m.ticketId) + 1;
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{enhancement.ticketId},{enhancement.summary},{enhancement.status},{enhancement.priority},{enhancement.submitter},{enhancement.assigned},{string.Join("|", enhancement.watching)},{enhancement.software},{enhancement.cost},{enhancement.reason},{enhancement.estimate}");
                sw.Close();
                EnhancementTickets.Add(enhancement);
                logger.Info("Ticket ID {Id} added", enhancement.ticketId);
            } 
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}