namespace OdeToFood
{
    public interface IGreeter
    {
        string GetGreeting();
    }
    public class Greeter : IGreeter
    {
        public string GetGreeting()
        {
            return "Hi from the greeter methods";
        }
    }
}
