using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.RoomTypes;

/**
 * The RoomTypeDescription class represents the description of a room type.
 */
public class RoomTypeDescription : IValueObject<string>
{
    public string Value { get; private set; }
    
    // Private constructor for EF Core
    private RoomTypeDescription() { }
    
    /**
     * Constructor for RoomTypeDescription
     * @param description
     */
    public RoomTypeDescription(string description)
    {
        this.Value = description;
    }
    
    /**
     * Returns the string representation of the RoomTypeDescription
     */
    public override string ToString()
    {
        return Value;
    }

    /**
     * Compares the RoomTypeDescription with another object
     * @param obj
     * @return true if the objects are equal, false otherwise
     */
    public override bool Equals(object obj)
    {
        if (obj is RoomTypeDescription other)
        {
            return Value == other.Value;
        }
        return false;
    }

    /**
     * Returns the hash code of the RoomTypeDescription
     */
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    
}