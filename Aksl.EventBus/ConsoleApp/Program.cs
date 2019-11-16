using System;
using System.Threading.Tasks;

namespace EventBus.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await EventBuser.Instance.InitializeTask();

            await EventBuser.Instance.InitializeSubscribe();

            Console.WriteLine("Hello World!");

            Console.ReadLine();
        }
    }
}
