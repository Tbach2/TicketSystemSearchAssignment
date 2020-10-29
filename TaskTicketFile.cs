using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog.Web;

namespace TicketingSystem
{
    public class TaskTicketFile
    {
        public string filePath { get; set; }
        public List<Task> TaskTickets { get; set; }
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

        public TaskTicketFile(string TaskTicketFilePath)
        {
            filePath = TaskTicketFilePath;
            TaskTickets = new List<Task>();

            try
            {
                StreamReader sr = new StreamReader(filePath);
                while (!sr.EndOfStream)
                {
                    
                    string line = sr.ReadLine();
                    string[] ticketDetails = line.Split(',');

                    if (ticketDetails.Length == 9)
                    {
                        Task task = new Task();

                        task.ticketId = UInt64.Parse(ticketDetails[0]);
                        task.summary = ticketDetails[1];
                        task.status = ticketDetails[2];
                        task.priority = ticketDetails[3];
                        task.submitter = ticketDetails[4];
                        task.assigned = ticketDetails[5];
                        task.watching = ticketDetails[6].Split('|').ToList();
                        task.projectName = ticketDetails[7];
                        task.dueDate = ticketDetails[8];
                        TaskTickets.Add(task);
                    }
                }
                sr.Close();
                logger.Info("Tickets in file: {Count}", TaskTickets.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void AddTask(Task task)
        {
            try
            {
                task.ticketId = TaskTickets.Max(m => m.ticketId) + 1;
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{task.ticketId},{task.summary},{task.status},{task.priority},{task.submitter},{task.assigned},{string.Join("|", task.watching)},{task.projectName},{task.dueDate}");
                sw.Close();
                TaskTickets.Add(task);
                logger.Info("Ticket ID {Id} added", task.ticketId);
            } 
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}