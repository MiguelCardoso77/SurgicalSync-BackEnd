using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.SurgeryRooms
{
    /**
     * Represents a value object for the capacity of a surgery room.
     * This class defines the maximum number of patients and staff that a surgery room can accommodate.
     */
    public class Capacity : IValueObject<int>
    {
        /**
         * The maximum number of patients that the surgery room can accommodate.
         * This property stores the string representation of the maximum number of patients.
         */
        public int MaxPatients { get; private set; }

        /**
         * The maximum number of staff that the surgery room can accommodate.
         * This property stores the string representation of the maximum number of staff.
         */
        public int MaxStaff { get; private set; }
        /**
        * The value representing the total capacity (sum of MaxPatients and MaxStaff).
        * This property is required by the IValueObject interface.
        */
        public int Value => MaxPatients + MaxStaff;
        /**
         * Private constructor for the Capacity class.
         * This constructor is used for internal purposes and is not exposed for object creation.
         */
        private Capacity()
        {
            
        }

        /**
         * Initializes a new instance of the Capacity class with a specified maximum values for patients and staff.
         *
         * @param maxPatients The maximum number of patients the surgery room can accommodate.
         * @param maxStaff The maximum number of staff the surgery room can accommodate.
         */
        public Capacity(int maxPatients, int maxStaff)
        {
            if (maxPatients < 0 || maxStaff < 0)
            {
                throw new ArgumentException("Capacity values must be non-negative.");
            }

            MaxPatients = maxPatients;
            MaxStaff = maxStaff;
        }

        /**
         * Returns a string representation of the capacity.
         * The string includes both the maximum number of patients and staff.
         *
         * @return A string representing the maximum capacity.
         */
        public override string ToString()
        {
            return $"Max Patients: {MaxPatients}, Max Staff: {MaxStaff}";
        }

        /**
         * Compares the current Capacity object to another object for equality.
         * Two Capacity objects are considered equal if their MaxPatients and MaxStaff properties are the same.
         *
         * @param obj The object to compare with.
         * @return True if the objects are equal, otherwise false.
         */
        public override bool Equals(object obj)
        {
            if (obj is Capacity other)
            {
                return MaxPatients == other.MaxPatients && MaxStaff == other.MaxStaff;
            }

            return false;
        }

        /**
         * Returns a hash code for the Capacity object based on its MaxPatients and MaxStaff values.
         *
         * @return The hash code of the capacity value.
         */
        public override int GetHashCode()
        {
            return HashCode.Combine(MaxPatients, MaxStaff);
        }
    }
}
