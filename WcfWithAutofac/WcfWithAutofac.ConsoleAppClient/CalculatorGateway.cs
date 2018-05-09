using System;
using WcfWithAutofac.ConsoleAppClient.WcfWithAutofacServiceReference;

namespace WcfWithAutofac.ConsoleAppClient
{
    public class CalculatorGateway : ICalculatorGateway
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorGateway(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        public void CallServiceOperations()
        {
            // Call the Add service operation.  
            double value1 = 100.00D;
            double value2 = 15.99D;
            double result = _calculatorService.Add(value1, value2);
            Console.WriteLine("Add({0},{1}) = {2}", value1, value2, result);

            // Call the Subtract service operation.  
            value1 = 145.00D;
            value2 = 76.54D;
            result = _calculatorService.Subtract(value1, value2);
            Console.WriteLine("Subtract({0},{1}) = {2}", value1, value2, result);

            // Call the Multiply service operation.  
            value1 = 9.00D;
            value2 = 81.25D;
            result = _calculatorService.Multiply(value1, value2);
            Console.WriteLine("Multiply({0},{1}) = {2}", value1, value2, result);

            // Call the Divide service operation.  
            value1 = 22.00D;
            value2 = 7.00D;
            result = _calculatorService.Divide(value1, value2);
            Console.WriteLine("Divide({0},{1}) = {2}", value1, value2, result);
        }
    }
}
