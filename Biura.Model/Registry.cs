using System;

namespace Biura.Model
{
    public class Registry
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public List<Client> Clients { get; set; }
        public Excursion TripDetails { get; set; }
        public Payment InitialPayment { get; set; }
        public bool AdditionalPayment {  get; set; }
        public Payment? Afterpayment { get; set; }
        public DateTime? AfterpaymentDate { get; set; }
        public DateTime FlightConfirmationDate { get; set; }
        public Insurance InsuranceDetails { get; set; }

        public Registry()
        {
            Clients = new List<Client>();
            Date = new DateTime();
            TripDetails = new Excursion();
            InitialPayment = new Payment();
            InsuranceDetails = new Insurance();
        }

        public Registry(List<Client> clients, DateTime date, Excursion tripDetails, decimal initialPayment, Insurance insuranceDetails, bool surr = false)
        {
            Clients = clients;
            Date = date;
            TripDetails = tripDetails;
            InitialPayment = new Payment(initialPayment, date);
            AdditionalPayment = surr;
            // dodac jakiegos if a czy bedzie tworzyc afterpayment 
            // afterpayment i tak bedzie musial byc dodany po jakims czasie 
            //Afterpayment = new Payment(initialPayment, date+new TimeSpan(7, 0, 0, 0));
            InsuranceDetails = insuranceDetails;
            if (surr)
            {
                AfterpaymentDate = date + new TimeSpan(7, 0, 0, 0);
            }
            if (tripDetails is not null)
            {
                FlightConfirmationDate = tripDetails.Date - new TimeSpan(7, 0, 0, 0);
            }
        }

        public decimal GetTripComission()
        {
            return(TripDetails.Cost * TripDetails.TripOperator.CommisionRate);
        }
        public decimal GetInsuranceComission() {
            return(TripDetails.Cost * InsuranceDetails.ComissionRate);
        }
        public decimal GetTotalComission() {
            return(GetTripComission() + GetInsuranceComission());
        }

        public override string ToString()
        {
            string clientsNames = string.Join(", ", Clients.ConvertAll(c => c.FirstName));
            return $"Registry for trip to {TripDetails.Location} on {TripDetails.Date.ToShortDateString()} with clients: {clientsNames}. Initial payment: {InitialPayment.Amount} on {InitialPayment.PaymentDate.ToShortDateString()}. Insurance: {InsuranceDetails.PolicyNumber}. Total commission: {GetTotalComission()}";
        }

    }


}