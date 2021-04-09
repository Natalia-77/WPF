using System;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        static void Main()
        {
            
            for (int i = 1; i < 6; i++)
            {
                Methods(i);
            }
            Console.ReadLine();
        }

        static async void Methods(int count)
        {
            
            int result = await Task.Run(() => GetSum(count))
                .ContinueWith(task => MultiplyNegative1(task));
            Console.WriteLine("Methods result: " + result);
        }

        static int GetSum(int count)
        {
            // викликається першим.
            int sum = 0;
            for (int z = 1; z < count; z++)
            {
                
                sum = z*2;
            }
            return sum;
        }

        static int MultiplyNegative1(Task<int> task)
        {
            // викликається другим і поаертає значення номножене на мінус 1.
            return task.Result * -1;
        }
    }

}


