using System;

namespace Biura.Model
{

	public class Payment
	{
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
		public DateTime PaymentDate { get; set; }
        public Payment()
		{
		}
		public Payment(decimal amount, DateTime paymentDate)
		{
			Amount = amount;
			PaymentDate = paymentDate;
        }
    }
}