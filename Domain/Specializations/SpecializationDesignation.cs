using System;

namespace DDDNetCore.Domain.Specializations;

/**
 * The SpecializationDesignation class represents the designation of a specialization.
 */
public class SpecializationDesignation
{
    public string Value { get; private set; }
    
    // Private constructor for EF Core
    private SpecializationDesignation() { }


    /**
     * Constructor for SpecializationDesignation
     * @param designation
     * @throws FormatException if designation is null, empty or has more than 100 characters.
     */
    public SpecializationDesignation(string designation)
    {
        if (string.IsNullOrWhiteSpace(designation) || designation.Length >= 100)
        {
            throw new FormatException("Specialization designation must be a non-empty string with less than 100 characters.");
        }
        
        this.Value = designation;
    }
    
    /**
    * Returns the string representation of the SpecializationDesignation
    */
    public override string ToString()
    {
        return Value;
    }
    
    /**
     * Compares the SpecializationDesignation with another object
     * @param obj
     * @return true if the objects are equal, false otherwise
     */
    public override bool Equals(object obj)
    {
        if (obj is SpecializationDesignation other)
        {
            return Value == other.Value;
        }
        return false;
    }
    
    /**
     * Returns the hash code of the SpecializationDesignation
     */
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}