using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Appointments;
/**
 * Represents a unique identifier for an appointment entity.
 * Inherits from the base EntityId class, providing a strong-typed ID for appointments.
 */
public class AppointmentId : EntityId
{
    /**
     * Initializes a new instance of the AppointmentId class with the specified identifier value.
     *
     * @param value The unique identifier value for the appointment.
     */
    public AppointmentId(string value) : base(value)
    {
        
    }
    /**
     * Creates an AppointmentId object from the given string.
     *
     * @param text The string representation of the appointment ID.
     * @return The appointment ID created from the input string.
     */
    protected override object createFromString(string text)
    {
        return text;
    }
    /**
     * Returns the appointment ID as a string.
     *
     * @return The string representation of the appointment ID.
     */
    public override string AsString()
    {
        return (string)Value;
    }
}