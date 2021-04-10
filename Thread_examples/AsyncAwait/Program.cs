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
        //
        static int GetSum(int count)
        {
            // викликається першим.Повертає значення,що дорівнює номеру ітерації,помноженому на 2.
            int res = 0;
            for (int z = 1; z < count; z++)
            {
                
                res = z*2;
            }
            return res;
        }

        static int MultiplyNegative1(Task<int> task)
        {
            // викликається другим і повертає значення номножене на мінус 1.
            return task.Result * -1;
        }
    }

}


