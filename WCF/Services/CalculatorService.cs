using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;

namespace Services
{
    public class CalculatorService : ICalculator
    {
        public double Add(double x, double y)
        {
            return x + y;        
        }
    }
}
