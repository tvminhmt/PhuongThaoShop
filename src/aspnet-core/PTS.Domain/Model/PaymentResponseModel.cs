namespace PTS.Domain.Model
{
    public class PaymentResponseModel
    {
        public string OrderDescription { get; set; }
      
        public string PaymentMethod { get; set; }
        public bool Success { get; set; }
        public string BillCode { get; set; }
        public string VnPayResponseCode { get; set; }
    }
}
