namespace TEST_APP_
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public List<string> Items { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}