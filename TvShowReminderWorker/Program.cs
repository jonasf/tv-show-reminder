using System;
using Microsoft.Azure.WebJobs;

namespace TvShowReminderWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I am the web job, I ran at {0}", DateTime.Now);

            var host = new JobHost();
            host.RunAndBlock();
        }
    }
}
