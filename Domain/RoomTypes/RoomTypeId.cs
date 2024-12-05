using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.RoomTypes;

/**
 * Represents a unique identifier for an room type in the domain.
 * This class extends EntityId and is used to ensure that each operation type
 * has a distinct and properly formatted identifier.
 * It provides methods for creating an ID from a string and retrieving the
 * ID value as a string.
 */
public class RoomTypeId : EntityId
{
    /**
     * Constructor that initializes the RoomTypeId with a string value.
     */
    public RoomTypeId(string value) : base(value)
    {
    }

    /**
     * Protected method that creates the ID from a string.
     * In this case, it simply returns the passed string.
     */
    protected override object createFromString(string text)
    {
        return text;
    }

    /**
     * Method that returns the value of RoomTypeId as a string.
     */
    public override string AsString()
    {
        return Value;
    }
}