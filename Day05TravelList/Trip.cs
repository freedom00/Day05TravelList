using System;
using System.Text.RegularExpressions;

namespace Day05TravelList
{
    internal class Trip
    {
        private string destination;
        private string travellerName;
        private string travellerPassportNo;
        private DateTime departureDate;
        private DateTime returnDate;

        public string Destination
        {
            get => destination;
            set
            {
                if (value.Length < 2 || value.Length > 30 || value.Contains(";"))
                {
                    throw new InvalidParameterException("Destination must be 2-30.");
                }
                destination = value;
            }
        }

        public string TravellerName
        {
            get => travellerName;
            set
            {
                if (value.Length < 2 || value.Length > 30 || value.Contains(";"))
                {
                    throw new InvalidParameterException("Traveller name must be 2-30.");
                }
                travellerName = value;
            }
        }

        public string TravellerPassportNo
        {
            get => travellerPassportNo;
            set
            {
                if (!Regex.IsMatch(value, @"[A-Z]{2}\d{6}"))
                {
                    throw new InvalidParameterException("Traveller password number must be two uppercase letters followed by 8 digits.");
                }
                travellerPassportNo = value;
            }
        }

        public DateTime DepartureDate
        {
            get => departureDate;
            set
            {
                if (value.Year < 1900 || value.Year > 2100)
                {
                    throw new InvalidParameterException("Departure date must be 1900-2100.");
                }
                departureDate = value;
            }
        }

        public DateTime ReturnDate
        {
            get => returnDate;
            set
            {
                if (value.Year < 1900 || value.Year > 2100)
                {
                    throw new InvalidParameterException("Return time must be 1900-2100.");
                }
                returnDate = value;
            }
        }

        public Trip(string travellerName, string travellerPassportNo, string destination, DateTime departureDate, DateTime returnDate)
        {
            Destination = destination;
            TravellerName = travellerName;
            TravellerPassportNo = travellerPassportNo;
            setDepartureAndReturnDates(departureDate, returnDate);
        }

        public void setDepartureAndReturnDates(DateTime depDate, DateTime retDate)
        {
            if (DateTime.Compare(depDate, retDate) > 0)
            {
                throw new ArgumentException("Departure date must be before return date.");
            }
            DepartureDate = depDate;
            ReturnDate = retDate;
        }

        public override string ToString()
        {
            return string.Format("{0}'s {1}, Destination is {2}, Departure date is {3}, Return date is {4}", TravellerName, TravellerPassportNo, Destination, DepartureDate, ReturnDate);
        }

        public virtual string ToFileString()
        {
            return string.Format("{0};{1};{2};{3};{4}", TravellerName, TravellerPassportNo, Destination, DepartureDate, ReturnDate);
        }
    }
}