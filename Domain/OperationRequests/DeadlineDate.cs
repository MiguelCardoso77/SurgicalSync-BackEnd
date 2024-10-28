using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.OperationRequests
{
    /**
     * Represents a deadline date for an operation request in the domain.
     * This class encapsulates the logic and validation associated with a deadline date,
     * ensuring that it adheres to the rules of not being set in the past.
     */
    public class DeadlineDate : IValueObject<string>
    {
        /**
         * Stores the deadline date as a DateTime value.
         */
        public DateTime DateTime { get; private set; }
        public string Value  => DateTime.ToString("yyyy-MM-dd");

        // Private constructor for EF 
        private DeadlineDate()
        {
            
        }
        
        /**
         * Constructor that initializes the DeadlineDate with a specific date.
         */
        public DeadlineDate(DateTime date)
        {
            if (date < DateTime.Now.Date)
            {
                throw new ArgumentException("Deadline date cannot be in the past");
            }
            DateTime = date;
        }
        /**
         * Method to get the date as a formatted string.
         */
        public override string ToString()
        {
            return DateTime.ToString("yyyy-MM-dd");
        }
        /**
         * Method to compare two DeadlineDate instances.
         */
        public override bool Equals(object obj)
        {
            if (obj is DeadlineDate other)
            {
                return DateTime.Equals(other.DateTime);
            }
            return false;
        }
        /**
         * Method to get a hash code for the DeadlineDate.
         */
        public override int GetHashCode()
        {
            return DateTime.GetHashCode();
        }
    }
}