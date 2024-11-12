using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.SurgeryRooms
{
    /**
     * Represents a unique identifier for a surgery room, inheriting from EntityId.
     * The RoomNumber class is used to store and manage the room number as a string value.
     */
    public class RoomNumber : EntityId
    {
        /**
         * Initializes a new instance of the RoomNumber class with a specified value.
         *
         * @param value The string value representing the room number.
         */
        public RoomNumber(string value) : base(value)
        {

        }

        /**
        * Converts a string representation of the room number into an object.
        * This method is used to create an instance of RoomNumber from a string.
        *
        * @param text The string value representing the room number.
        * @return The RoomNumber object created from the string value.
        */
        protected override object createFromString(string text)
        {
            return text;
        }

        /**
        * Returns the string representation of the room number.
        * This method overrides the AsString method from the base EntityId class to return the room number as a string.
        *
        * @return The string value of the room number.
        */
        public override string AsString()
        {
            return (string)Value;
        }
    }
}