namespace ECommerceFeedback.Models.Domain.Response
{
    public class OrderDetails : TextResponse
    {

        public string? OrderId { get; set; }

        public long TotalCost { get; set; }

    }
}
