namespace WebAppProducer.Domain
{
    public sealed class Order
    {
        public uint Id { get; set; }
        public string Client { get; set; }
        public IEnumerable<string> Items{ get; set; }
    }
}