using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Specializations;

public class SpecializationCode : EntityId
{
    
    /**
     * Constructor that initializes the SpecializationCode with a string value.
     */
    public SpecializationCode(string value) : base(value)
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
     * Method that returns the value of SpecializationCode as a string.
     */
    public override string AsString()
    {
        return Value;
    }

}
