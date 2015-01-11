using System;

namespace TvShowReminderWorker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I am the web job, I ran at {0}", DateTime.Now);

            JobHost host = new JobHost();
            host.RunAndBlock();
        }
    }
}
