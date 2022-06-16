namespace Producer
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Producer UP!");

            var _publisher = new Publisher();

           _publisher.InitializePublisher(args);

        }
    }
}