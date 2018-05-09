using WcfWithAutofac.WcfServiceLibrary.DomainServices;

namespace WcfWithAutofac.WcfServiceLibrary
{
    public class CalculatorService : ICalculatorService
    {
        private readonly ICalculator _calculator;

        public CalculatorService(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public double Add(double n1, double n2)
        {
            return _calculator.Add(n1, n2);
        }

        public double Subtract(double n1, double n2)
        {
            return _calculator.Subtract(n1, n2);
        }

        public double Multiply(double n1, double n2)
        {
            return _calculator.Multiply(n1, n2);
        }

        public double Divide(double n1, double n2)
        {
            return _calculator.Divide(n1, n2);
        }
    }
}
