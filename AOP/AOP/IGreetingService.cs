using AOP.Models;
namespace AOP
{
    public interface IGreetingService
    {
        GreetingModel SayHello(string name);
    }
}
