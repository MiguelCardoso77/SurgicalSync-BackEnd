using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Appointments
{
    /**
     * Represents a specific time of day in minutes.
     */
    public class Time : IComparable<Time>
    {
        /**
         * The time value in minutes since midnight (0 to 1439).
         */
        public int Value { get; private set; }
        
        // Private default constructor to support serialization or ORM frameworks.
        private Time() { }

        /**
         * Initializes a new Time instance with a specified number of minutes.
         *
         * @param minutes The number of minutes (0 to 1439).
         * @throws ArgumentException if the provided time is invalid.
         */
        public Time(int minutes)
        {
            if (minutes < 0 || minutes >= 1440)
            {
                throw new ArgumentException("Invalid time. Minutes must be between 0 and 1439.");
            }

            Value = minutes;
        }

        /**
         * Initializes a new Time instance with a specified hour and minute.
         *
         * @param hours The hour component of the time (0-23).
         * @param minutes The minute component of the time (0-59).
         * @throws ArgumentException if the provided time is invalid (e.g., hours not in 0-23 or minutes not in 0-59).
         */
        public Time(int hours, int minutes)
        {
            if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59)
            {
                throw new ArgumentException("Invalid time. Hours must be between 0-23 and minutes between 0-59.");
            }

            Value = (hours * 60) + minutes;
        }

        /**
         * Returns the time as a formatted string in "HH:mm" format.
         *
         * @return A string representation of the time in "HH:mm" format.
         */
        public override string ToString()
        {
            int hours = Value / 60;
            int minutes = Value % 60;
            return $"{hours:D2}:{minutes:D2}";
        }

        /**
         * Checks if this Time is equal to another object.
         *
         * @param obj The object to compare with this instance.
         * @return true if the object is a Time and has the same value; otherwise, false.
         */
        public override bool Equals(object obj)
        {
            return obj is Time other && Value == other.Value;
        }

        /**
         * Gets the hash code for this Time instance.
         *
         * @return An integer representing the hash code for this time value.
         */
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /**
         * Compares this Time instance to another Time instance.
         *
         * @param other The other Time instance to compare to.
         * @return A negative value if this time is earlier, zero if equal, and a positive value if later than the other time.
         */
        public int CompareTo(Time other)
        {
            return Value.CompareTo(other.Value);
        }

        /**
         * Determines if one Time instance is earlier than another.
         *
         * @param t1 The first Time instance.
         * @param t2 The second Time instance.
         * @return true if t1 is earlier than t2; otherwise, false.
         */
        public static bool operator <(Time t1, Time t2) => t1.CompareTo(t2) < 0;
        public static bool operator >(Time t1, Time t2) => t1.CompareTo(t2) > 0;
        public static bool operator <=(Time t1, Time t2) => t1.CompareTo(t2) <= 0;
        public static bool operator >=(Time t1, Time t2) => t1.CompareTo(t2) >= 0;
    }
}
