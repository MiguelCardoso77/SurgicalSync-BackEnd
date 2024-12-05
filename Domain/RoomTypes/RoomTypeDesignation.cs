using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.RoomTypes;

/**
 * The RoomTypeDesignation class represents the designation of a room type.
 */
public class RoomTypeDesignation : IValueObject<string>
{
    public string Value { get; private set; }
    
    // Private constructor for EF Core
    private RoomTypeDesignation() { }
    
    /**
     * Constructor for RoomTypeDesignation
     * @param designation
     * @throws FormatException if designation is null, empty or has more than 100 characters.
     */
    public RoomTypeDesignation(string designation)
    {
        if (string.IsNullOrWhiteSpace(designation) || designation.Length >= 100)
        {
            throw new FormatException("Room type designation must be a non-empty string with less than 100 characters.");
        }
        
        this.Value = designation;
    }
    
    /**
     * Returns the string representation of the RoomTypeDesignation
     */
    public override string ToString()
    {
        return Value;
    }

    /**
     * Compares the RoomTypeDesignation with another object
     * @param obj
     * @return true if the objects are equal, false otherwise
     */
    public override bool Equals(object obj)
    {
        if (obj is RoomTypeDesignation other)
        {
            return Value == other.Value;
        }
        return false;
    }

    /**
     * Returns the hash code of the RoomTypeDesignation
     */
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
    
}