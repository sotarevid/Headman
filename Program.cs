using System;

namespace Headman
{
    static class Program
    {
        static void Main()
        {
            var botController = new Controller("");
            
            botController.StartBot();
            
            Console.ReadKey();
            
            botController.StopBot();
        }
    }
}