using AOP.Models;
using CodeRewriting;

namespace AOP.Services
{
    public class GreetingService : IGreetingService
    {
        [LogDataAspect]
        public GreetingModel SayHello(string name)
        {
            return new GreetingModel
            {
                Name = name,
                Greeting = $"Hello {name}"
            };
        }
    }
}