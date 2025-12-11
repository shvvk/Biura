using System;
using System.Collections.Generic;

namespace Biura.Model
{
	public class Insurance
	{
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
		public string Provider { get; set; }
		public DateTime CoverageStartDate { get; set; }
		public DateTime CoverageEndDate { get; set; }
		public decimal ComissionRate { get; set; }
        // add benefits
        public Insurance()
		{

		}

		public Insurance(
			string policyNumber,
			string provider,
			DateTime coverageStart,
			DateTime coverageEnd,
			decimal comissionRate)
		{
			PolicyNumber = policyNumber;
			Provider = provider;
			CoverageStartDate = coverageStart;
			CoverageEndDate = coverageEnd;
			ComissionRate = comissionRate;
            //Validate();
        }

		// Returns true if the insurance is active for the specified date (or today if null)
		public bool IsActive(DateTime? asOf = null)
		{
			var date = asOf ?? DateTime.UtcNow.Date;
			return CoversDate(date);
		}

		// Number of days in coverage (inclusive)
		public int DurationDays
		{
			get
			{
				if (CoverageEndDate < CoverageStartDate) return 0;
				return (int)(CoverageEndDate.Date - CoverageStartDate.Date).TotalDays + 1;
			}
		}

		// Checks if a given date is covered
		public bool CoversDate(DateTime date)
		{
			var d = date.Date;
			return d >= CoverageStartDate.Date && d <= CoverageEndDate.Date;
		}

		// Checks if the insurance overlaps with given trip dates
		public bool OverlapsWith(DateTime tripStart, DateTime tripEnd)
		{
			if (tripEnd < tripStart) return false;
			return CoverageStartDate.Date <= tripEnd.Date && CoverageEndDate.Date >= tripStart.Date;
		}

		// Basic validation to ensure required fields make sense
		public void Validate()
		{
			if (CoverageEndDate < CoverageStartDate)
			{
				throw new ArgumentException("CoverageEndDate must be the same or after CoverageStartDate.");
			}

			if (string.IsNullOrWhiteSpace(PolicyNumber))
			{
				throw new ArgumentException("PolicyNumber is required.");
			}

			if (string.IsNullOrWhiteSpace(Provider))
			{
				throw new ArgumentException("Provider is required.");
			}
		}

		public override string ToString()
		{
			return $"{Provider} - {PolicyNumber} ({CoverageStartDate:yyyy-MM-dd} to {CoverageEndDate:yyyy-MM-dd})";
		}
	}
}