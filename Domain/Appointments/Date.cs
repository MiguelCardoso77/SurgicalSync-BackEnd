using System;
using DDDNetCore.Domain.OperationRequests;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Appointments
{
    /**
     * Represents a date value object, enforcing a non-past constraint for date values.
     * This value object is intended for use with appointments, ensuring valid date representation.
     */
    public class Date : IValueObject<string>
    {
        /**
         * The DateTime value of this date.
         */
        public DateTime DateTime { get; private set; }
        /**
         * Returns the string representation of the date in the format "yyyyMMdd".
         */
        public string Value => DateTime.ToString("yyyyMMdd");
        /**
         * Private constructor for ORM or serialization purposes.
         */
        private Date()
        {
            
        }
        /**
         * Initializes a new instance of the Date class with the specified date.
         *
         * @param date The date value to set.
         * @throws ArgumentException if the date is in the past.
         */
        public Date(DateTime date)
        {
            if (date < DateTime.Now)
            {
                throw new ArgumentException("Date cannot be in the past");
            }
            DateTime = date;
        }
        /**
        * Returns the string representation of this date in "yyyyMMdd" format.
        *
        * @return A string representation of the date.
        */
        public override string ToString()
        {
            return DateTime.ToString("yyyyMMdd");
        }
        /**
         * Checks if this date is equal to another date object.
         *
         * @param obj The object to compare to.
         * @return True if the other object is a Date and has the same date value; otherwise, false.
         */
        public override bool Equals(object obj)
        {
            if (obj is Date other)
            {
                return DateTime.Equals(other.DateTime);
            }

            return false;
        }
        /**
         * Returns the hash code for this date.
         *
         * @return The hash code of the date.
         */
        public override int GetHashCode()
        {
            return DateTime.GetHashCode();
        }
    }
}