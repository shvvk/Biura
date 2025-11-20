using System;

namespace Biura.Model
{
    public class Registry
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<Client> Clients { get; set; }
        public Excursion TripDetails { get; set; }
        public Payment InitialPayment { get; set; }
        public Payment? Afterpayment { get; set; }
        public Insurance InsuranceDetails { get; set; }
        // dodac doplaty w payment dodac boole do doplat 
        // automatyczne obliczanie daty doplaty i potwiedzenia lotu
        // usunąc siec tych biur 
        public Registry()
        {
            Clients = new List<Client>();
            TripDetails = new Excursion();
            InitialPayment = new Payment();
            InsuranceDetails = new Insurance();
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