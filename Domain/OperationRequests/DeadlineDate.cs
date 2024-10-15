using System;

namespace DDDNetCore.Domain.OperationRequests
{
    // Represents a deadline date for an operation request in the domain.
    // This class encapsulates the logic and validation associated with a deadline date,
    // ensuring that it adheres to the rules of not being set in the past.
    public class DeadlineDate
    {
        // Stores the deadline date as a DateTime value.
        public DateTime Date { get; private set; }
        // Constructor that initializes the DeadlineDate with a specific date.
        public DeadlineDate(DateTime date)
        {
            if (date < DateTime.Now.Date)
            {
                throw new ArgumentException("Deadline date cannot be in the past");
            }
            Date = date;
        }
        // Method to get the date as a formatted string.
        public override string ToString()
        {
            return Date.ToString("yyyy-MM-dd");
        }
        // Method to compare two DeadlineDate instances.
        public override bool Equals(object obj)
        {
            if (obj is DeadlineDate other)
            {
                return Date.Equals(other.Date);
            }
            return false;
        }
        // Method to get a hash code for the DeadlineDate.
        public override int GetHashCode()
        {
            return Date.GetHashCode();
        }
    }
}