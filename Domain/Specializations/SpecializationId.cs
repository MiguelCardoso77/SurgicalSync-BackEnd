using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Specializations;

public class SpecializationId : EntityId
{
    
    /**
     * Constructor that initializes the SpecializationId with a string value.
     */
    public SpecializationId(string value) : base(value)
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
     * Method that returns the value of SpecializationId as a string.
     */
    public override string AsString()
    {
        return Value;
    }

}
