namespace ElasticSearch.Web.ViewModel
{
    public class ECommerceViewModel
    {
        public string Id { get; set; } = null!;

        public string CustomerFirstName { get; set; } = null!;

        public string CustomerLastName { get; set; } = null!;

        public string CustomerFullName { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public double TaxFulTotalPrice { get; set; }

        public string[] Category { get; set; } = null!;

        public string Categories { get { return string.Join(",", Category); } }
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
