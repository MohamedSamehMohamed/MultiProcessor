using System;
using MultiProcessor.Data;
using MultiProcessor.DataStructures;

namespace MultiProcessor
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var FcfsProcessor = new Processor((p1, p2) => p1.ArrivalTime - p2.ArrivalTime);
            for (int i = 10; i >= 0; i--)
            {
                var p = new Process();
                p.ArrivalTime = i;
                FcfsProcessor.Add(p);
            }

            for (int i = 0; i < 10; i++)
            {
                var p = FcfsProcessor.GetNext();
                Console.WriteLine(p.ArrivalTime);
            }
        }
    }
}