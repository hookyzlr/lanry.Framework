using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Services;
using Contracts;
using System.ServiceModel;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (CalculatorServiceClient.CalculatorServiceClient s = new CalculatorServiceClient.CalculatorServiceClient())
            //{

            //    Console.WriteLine(s.Add(1.0, 2.0));

            //    Console.Read();
            //}

            ChannelFactory<ICalculator> c =
                new ChannelFactory<ICalculator>("calculatorservice");

            ICalculator proxy = c.CreateChannel();

            Console.WriteLine( proxy.Add(1,2));
            Console.Read();
        }
    }
}
