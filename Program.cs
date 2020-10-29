using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace TicketingSystem
{
    class Program
    {
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            string bugDefectTicketFilePath = Directory.GetCurrentDirectory() + "\\tickets.csv";
            string enhancementTicketFilePath = Directory.GetCurrentDirectory() + "\\enhancements.csv";
            string taskTicketFilePath = Directory.GetCurrentDirectory() + "\\tasks.csv";

            logger.Info("Program started");

            BugDefectTicketFile bugDefectTicketFile = new BugDefectTicketFile(bugDefectTicketFilePath);
            EnhancementTicketFile enhancementTicketFile = new EnhancementTicketFile(enhancementTicketFilePath);
            TaskTicketFile taskTicketFile = new TaskTicketFile(taskTicketFilePath);


            string choice = "";
            do
            {
                Console.WriteLine("1) Add Ticket");
                Console.WriteLine("2) Display All Tickets");
                Console.WriteLine("3) Search Tickets");
                Console.WriteLine("Enter to quit");

                choice = Console.ReadLine();
                logger.Info("User choice: {Choice}", choice);

                if (choice == "1")
                {

                    Console.WriteLine("1) Bug/Defect");
                    Console.WriteLine("2) Enhancement");
                    Console.WriteLine("3) Task");
                    string ticketTypeChoice = Console.ReadLine();
                    logger.Info("User choice: {Choice}", ticketTypeChoice);

                    if (ticketTypeChoice == "1")
                    {
                        BugDefect bugDefect = new BugDefect();

                        Console.WriteLine("Enter Bug/Defect ticket summary");
                        bugDefect.summary = Console.ReadLine();

                        Console.WriteLine("Enter Bug/Defect ticket status");
                        bugDefect.status = Console.ReadLine();

                        Console.WriteLine("Enter Bug/Defect ticket priority");
                        bugDefect.priority = Console.ReadLine();

                        Console.WriteLine("Enter Bug/Defect ticket submitter");
                        bugDefect.submitter = Console.ReadLine();

                        Console.WriteLine("Enter Bug/Defect ticket asignee");
                        bugDefect.assigned = Console.ReadLine();

                        string input;
                        do
                        {
                            Console.WriteLine("Enter Bug/Defect ticket watcher (or done to quit)");
                            input = Console.ReadLine();
                            if (input != "done" && input.Length > 0)
                            { bugDefect.watching.Add(input); }
                        }
                        while (input != "done");

                        if (bugDefect.watching.Count == 0)
                        { bugDefect.watching.Add("(no watchers listed)"); }

                        Console.WriteLine("Enter Bug/Defect ticket severity");
                        bugDefect.severity = Console.ReadLine();

                        bugDefectTicketFile.AddBugDefect(bugDefect);
                    }

                    if (ticketTypeChoice == "2")
                    {
                        Enhancement enhancement = new Enhancement();

                        Console.WriteLine("Enter Enhancement ticket summary");
                        enhancement.summary = Console.ReadLine();

                        Console.WriteLine("Enter Enhancement ticket status");
                        enhancement.status = Console.ReadLine();

                        Console.WriteLine("Enter Enhancement ticket priority");
                        enhancement.priority = Console.ReadLine();

                        Console.WriteLine("Enter Enhancement ticket submitter");
                        enhancement.submitter = Console.ReadLine();

                        Console.WriteLine("Enter Enhancement ticket asignee");
                        enhancement.assigned = Console.ReadLine();

                        string input;
                        do
                        {
                            Console.WriteLine("Enter Enhancement ticket watcher (or done to quit)");
                            input = Console.ReadLine();
                            if (input != "done" && input.Length > 0)
                            { enhancement.watching.Add(input); }
                        }
                        while (input != "done");

                        if (enhancement.watching.Count == 0)
                        { enhancement.watching.Add("(no watchers listed)"); }

                        Console.WriteLine("Enter Enhancement ticket software");
                        enhancement.software = Console.ReadLine();

                        Console.WriteLine("Enter Enhancement ticket cost");
                        enhancement.cost = Console.ReadLine();

                        Console.WriteLine("Enter Enhancement ticket reason");
                        enhancement.reason = Console.ReadLine();

                        Console.WriteLine("Enter Enhancement ticket estimate");
                        enhancement.estimate = Console.ReadLine();

                        enhancementTicketFile.AddEnhancemant(enhancement);
                    }
                    if (ticketTypeChoice == "3")
                    {
                        Task task = new Task();

                        Console.WriteLine("Enter Task ticket summary");
                        task.summary = Console.ReadLine();

                        Console.WriteLine("Enter Task ticket status");
                        task.status = Console.ReadLine();

                        Console.WriteLine("Enter Task ticket priority");
                        task.priority = Console.ReadLine();

                        Console.WriteLine("Enter Task ticket submitter");
                        task.submitter = Console.ReadLine();

                        Console.WriteLine("Enter Task ticket asignee");
                        task.assigned = Console.ReadLine();

                        string input;
                        do
                        {
                            Console.WriteLine("Enter Task ticket watcher (or done to quit)");
                            input = Console.ReadLine();
                            if (input != "done" && input.Length > 0)
                            { task.watching.Add(input); }
                        }
                        while (input != "done");

                        if (task.watching.Count == 0)
                        { task.watching.Add("(no watchers listed)"); }

                        Console.WriteLine("Enter Task ticket project name");
                        task.projectName = Console.ReadLine();

                        Console.WriteLine("Enter Task ticket due date");
                        task.dueDate = Console.ReadLine();

                        taskTicketFile.AddTask(task);
                    }
                } 

                else if (choice == "2")
                {
                    Console.WriteLine("1) Bug/Defect");
                    Console.WriteLine("2) Enhancement");
                    Console.WriteLine("3) Task");
                    string ticketTypeChoice = Console.ReadLine();
                    logger.Info("User choice: {Choice}", ticketTypeChoice);

                    if (ticketTypeChoice == "1")
                    {
                        foreach(Ticket m in bugDefectTicketFile.BugDefectTickets)
                    { Console.WriteLine(m.Display()); }
                    }
                    else if (ticketTypeChoice == "2")
                    {
                        foreach(Ticket m in enhancementTicketFile.EnhancementTickets)
                    { Console.WriteLine(m.Display()); }
                    }
                    else if (ticketTypeChoice == "3")
                    {
                        foreach(Ticket m in taskTicketFile.TaskTickets)
                    { Console.WriteLine(m.Display()); }
                    }
                    
                }
                else if (choice == "3")
                {
                    Console.WriteLine("1) Search Status");
                    Console.WriteLine("2) Search Priority");
                    Console.WriteLine("3) Search Submitter");
                    string searchChoice = Console.ReadLine();

                    if (searchChoice == "1")
                    {
                        Console.WriteLine("Enter Ticket Status");
                        string statusInput = Console.ReadLine();

                        var statusBugDefectSearch = bugDefectTicketFile.BugDefectTickets.Where(m => m.status.ToLower().Contains($"{statusInput}"));
                        var statusEnhancementSearch = enhancementTicketFile.EnhancementTickets.Where(m => m.status.ToLower().Contains($"{statusInput}"));
                        var statusTaskSearch = taskTicketFile.TaskTickets.Where(m => m.status.ToLower().Contains($"{statusInput}"));

                        Console.WriteLine($"There are {statusBugDefectSearch.Count()+statusTaskSearch.Count()+statusEnhancementSearch.Count()} {statusInput} status tickets:\n");
                        
                        Console.WriteLine("Bug Defect Tickets:");
                        foreach (BugDefect m in statusBugDefectSearch){ Console.WriteLine($" {m.Display()}"); }

                        Console.WriteLine("Enhancement Tickets:");
                        foreach (Enhancement m in statusEnhancementSearch){ Console.WriteLine($" {m.Display()}"); }

                        Console.WriteLine("Task Tickets:");
                        foreach (Task m in statusTaskSearch){ Console.WriteLine($" {m.Display()}"); }
                    }
                    else if (searchChoice == "2")
                    {
                        Console.WriteLine("Enter Ticket Priority");
                        string priorityInput = Console.ReadLine();

                        var priorityBugDefectSearch = bugDefectTicketFile.BugDefectTickets.Where(m => m.priority.ToLower().Contains($"{priorityInput}"));
                        var priorityEnhancementSearch = enhancementTicketFile.EnhancementTickets.Where(m => m.priority.ToLower().Contains($"{priorityInput}"));
                        var priorityTaskSearch = taskTicketFile.TaskTickets.Where(m => m.priority.ToLower().Contains($"{priorityInput}"));

                        Console.WriteLine($"There are {priorityBugDefectSearch.Count()+priorityEnhancementSearch.Count()+priorityTaskSearch.Count()} {priorityInput} tickets:\n");
                        
                        Console.WriteLine("Bug Defect Tickets:");
                        foreach (BugDefect m in priorityBugDefectSearch){ Console.WriteLine($" {m.Display()}"); }

                        Console.WriteLine("Enhancement Tickets:");
                        foreach (Enhancement m in priorityEnhancementSearch){ Console.WriteLine($" {m.Display()}"); }
                        Console.WriteLine("Task Tickets:");
                        foreach (Task m in priorityTaskSearch){ Console.WriteLine($" {m.Display()}"); }
                    }
                    else if (searchChoice == "3")
                    {
                        Console.WriteLine("Enter Ticket Submitter");
                        string submitterInput = Console.ReadLine();

                        var submitterBugDefectSearch = bugDefectTicketFile.BugDefectTickets.Where(m => m.submitter.ToLower().Contains($"{submitterInput}"));
                        var submitterEnhancementSearch = enhancementTicketFile.EnhancementTickets.Where(m => m.submitter.ToLower().Contains($"{submitterInput}"));
                        var submitterTaskSearch = taskTicketFile.TaskTickets.Where(m => m.submitter.ToLower().Contains($"{submitterInput}"));

                        Console.WriteLine($"There are {submitterBugDefectSearch.Count()+submitterEnhancementSearch.Count()+submitterTaskSearch.Count()} tickets submitted by {submitterInput}:\n");
                        
                        Console.WriteLine("Bug Defect Tickets:");
                        foreach (BugDefect m in submitterBugDefectSearch){ Console.WriteLine($" {m.Display()}"); }

                        Console.WriteLine("Enhancement Tickets:");
                        foreach (Enhancement m in submitterEnhancementSearch){ Console.WriteLine($" {m.Display()}"); }
                        Console.WriteLine("Task Tickets:");
                        foreach (Task m in submitterTaskSearch){ Console.WriteLine($" {m.Display()}"); }
                    }
                }
            } while (choice == "1" || choice == "2" || choice == "3");
            logger.Info("Program ended");
        }
    }
}