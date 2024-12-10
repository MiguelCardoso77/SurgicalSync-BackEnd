using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Specializations;

/**
 * The SpecializationDescription class represents the description of a specialization.
 */
public class SpecializationDescription: IValueObject<string>
{
    public string Value { get; private set; }
    
    private SpecializationDescription() { }

    /**
     * Constructor for SpecializationDescription
     * @param description
     */
    public SpecializationDescription(string description)
    {
        this.Value = description;
    }
    
    /**
     * Returns the string representation of the SpecializationDescription
     */
    public override string ToString()
    {
        return Value;
    }
    
    /**
     * Compares the SpecializationDescription with another object
     * @param obj
     * @return true if the objects are equal, false otherwise
     */
    public override bool Equals(object obj)
    {
        if (obj is SpecializationDescription other)
        {
            return Value == other.Value;
        }
        return false;
    }
    
    /**
     * Returns the hash code of the SpecializationDescription
     */
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

}