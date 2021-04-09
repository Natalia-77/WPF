using System;
using System.Threading;

namespace Thread_examples
{
    #region Printer helper class
    
    public class Printer
    {
        // Lock token.
        private object threadLock = new object();
        enum Week { Sunday, Monday, Thuesday, Wednesday, Friday };
       
        public void PrintDayweek()
        {
            
            lock (threadLock)
            {
                
                Console.WriteLine("-> {0} display days of week ",
                  Thread.CurrentThread.Name);

                // Print out days.
                Console.Write("Your days: ");
                for (int i = 0; i < 5; i++)
                {
                    Random r = new Random();                    
                    var values = Enum.GetValues(typeof(Week));
                    Week randomValue = (Week)r.Next(0, values.Length);
                    Console.Write("{0}, ", randomValue);
                }
                Console.WriteLine();
            }
        }
    }
#endregion

    class Program
    {
        static void Main(string[] args)
        {           
            Console.WriteLine("*****Synchronizing Threads *****\n");
            Printer p = new Printer();

          //виведе три рядки рандомно сформованих днів тижня.
            Thread[] threads = new Thread[3];
            for (int i = 0; i < 3; i++)
            {
                threads[i] = new Thread(new ThreadStart(p.PrintDayweek))
                {
                    Name = $"Worker thread --> {i}"
                };
            }            
            foreach (Thread t in threads)
                t.Start();
           // Console.ReadLine();
        }
    }
}
