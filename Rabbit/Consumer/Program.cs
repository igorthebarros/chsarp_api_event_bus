namespace Consumer
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Consumer UP!");

            var _worker = new Worker();

            _worker.InitializeWorker();
        }
    }
}