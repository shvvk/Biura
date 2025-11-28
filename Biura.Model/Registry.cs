using System;

namespace Biura.Model
{
    public class Registry
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Date { get; set; }
        public List<Client> Clients { get; set; }
        public Excursion TripDetails { get; set; }
        public Payment InitialPayment { get; set; }
        public bool Surcharge {  get; set; }
        public Payment? Afterpayment { get; set; }
        public Insurance InsuranceDetails { get; set; }
        // automatyczne obliczanie daty doplaty i potwiedzenia lotu
        // usunąc siec tych biur 

        public Registry()
        {
            Clients = new List<Client>();
            Date = new DateTime();
            TripDetails = new Excursion();
            InitialPayment = new Payment();
            InsuranceDetails = new Insurance();
        }

        public Registry(List<Client> clients,DateTime date, Excursion tripDetails, decimal initialPayment, Insurance insuranceDetails, bool surr = false)
        {
            Clients = clients;
            Date = date;
            TripDetails = tripDetails;
            InitialPayment = new Payment (initialPayment,date);
            Surcharge = surr;
            // dodac jakiegos if a czy bedzie tworzyc afterpayment 
            // afterpayment i tak bedzie musial byc dodany po jakims czasie 
            //Afterpayment = new Payment(initialPayment, date+new TimeSpan(7, 0, 0, 0));
            InsuranceDetails = insuranceDetails;
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



    }


}