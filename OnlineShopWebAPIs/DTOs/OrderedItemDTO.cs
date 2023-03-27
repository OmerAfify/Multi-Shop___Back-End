namespace DTOs
{
    public class OrderedItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal ProductSalesPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

    }
}