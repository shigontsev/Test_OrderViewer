namespace OrderViewer.Common.Entities
{
    public class OrderInfoFull
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string? UserName { get; set; }

        public IList<Product> Products { get; set; }
        public int Count
        {
            get => Products.Count();
        }

        public decimal Sum
        {
            get => Products.Sum(x => x.Price);
        }
    }
}
