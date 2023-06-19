namespace OrderViewer.Common.Entities
{
    public class OrderProduct
    {
        public int Id { get; set; }

        public int? OrderId { get; set; }

        public int? ProductId { get; set; }
    }
}
