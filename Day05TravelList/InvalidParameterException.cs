using System;

namespace Day05TravelList
{
    internal class InvalidParameterException : Exception
    {
        public InvalidParameterException(string message) : base(message)
        {
        }
    }
}