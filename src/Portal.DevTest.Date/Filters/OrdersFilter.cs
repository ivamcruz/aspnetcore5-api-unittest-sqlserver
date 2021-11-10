namespace Portal.DevTest.Date.Filters
{
    public class OrdersFilter : BaseFilter
    {
        public int? MinTotal { get; set; }
        public int? MaxTotal { get; set; }
    }
}
