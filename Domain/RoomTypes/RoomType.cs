using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.RoomTypes;

public class RoomType : Entity<RoomTypeId>, IAggregateRoot
{
    public new RoomTypeId Id { get; private set; }
    public RoomTypeDesignation Designation { get; private set; }
    public RoomTypeDescription Description { get; private set; }
    
    // Private constructor for EF
    private RoomType()
    {
        this.Designation = null;
        this.Description = null;
    }

    /**
     * Constructor that initializes the RoomType with a unique identifier, designation, and description.
     * @param id The unique identifier for the room type.
     * @throws ArgumentNullException if any of the parameters are null.
     */
    public RoomType(RoomTypeId code, RoomTypeDesignation designation, RoomTypeDescription description)
    {
        this.Id = code ?? throw new ArgumentNullException(nameof(code), "Room Type Code cannot be null.");
        this.Designation = designation ?? throw new ArgumentNullException(nameof(designation), "Room Type Designation cannot be null.");
        this.Description = description ?? new RoomTypeDescription("No description available.");
    }

    /**
     * Method that changes the designation of the RoomType.
     */
    public void ChangeDesignation(RoomTypeDesignation designation)
    {
        this.Designation = designation;
    }

    /**
     * Method that changes the description of the RoomType.
     */
    public void ChangeDescription(RoomTypeDescription description)
    {
        this.Description = description;
    }
}