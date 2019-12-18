using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace Vavatech.WebApi.Api.Contstraints
{
    public class PeselRouteConstraint : IHttpRouteConstraint
    {
        private IValidator peselValidator = new PeselValidator();

        public bool Match(
            HttpRequestMessage request, 
            IHttpRoute route, 
            string parameterName, 
            IDictionary<string, object> values, 
            HttpRouteDirection routeDirection)
        {
            if (values.TryGetValue(parameterName, out object routeValue))
            {
                string number = routeValue.ToString();

                return peselValidator.IsValid(number);
            }

            else
                return false;
            
        }
    }

    public class PeselValidator : ValidatorBase
    {
        protected override byte[] Weights => new byte[] { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
        protected override int CheckControl(int sumControl) => 10 - sumControl % 10;
    }

    public interface IValidator
    {
        bool IsValid(string number);
    }

    public abstract class ValidatorBase : IValidator
    {
        private int CalculateSumControl(byte[] numbers, byte[] weights) => numbers
            .Take(numbers.Length - 1)
            .Select((number, index) => new { number, index })
            .Sum(n => n.number * weights[n.index]);

        protected abstract byte[] Weights { get; }

        protected abstract int CheckControl(int sumControl);

        private byte[] ToByteArray(string input) => input
                                                    .ToCharArray()
                                                    .Select(c => byte.Parse(c.ToString()))
                                                    .ToArray();

        public bool IsValid(string number)
        {
            if (!number.All(Char.IsDigit))
            {
                throw new FormatException($"Number must have only digits");
            }

            if (number.Length != Weights.Length + 1)
            {
                return false;
                //throw new FormatException($"Number must have {Weights.Length} digits");
            }

            byte[] numbers = ToByteArray(number);

            int controlSum = CalculateSumControl(numbers, this.Weights);

            System.Diagnostics.Trace.WriteLine($"sum control {controlSum}");

            int controlNum = CheckControl(controlSum);

            System.Diagnostics.Trace.WriteLine($"divide modulo {controlNum}");


            if (controlNum == 10)
            {
                controlNum = 0;
            }

            return controlNum == numbers.Last();
        }
    }

}