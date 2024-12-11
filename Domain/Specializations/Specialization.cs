using System;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Domain.Specializations;

public class Specialization : Entity<SpecializationId>, IAggregateRoot
{
    public SpecializationId Id { get; private set; }
    public SpecializationDesignation Designation { get; private set; }
    public SpecializationDescription Description { get; private set; }
    
    // Private constructor for EF
    private Specialization()
    {
        this.Id = null;
        this.Designation = null;
        this.Description = null;
    }
    

    /**
     * Constructor that initializes the specialization with a unique identifier, designation, and description.
     * @param id The unique identifier for the specialization.
     * @throws ArgumentNullException if any of the parameters are null.
     */
    public Specialization(SpecializationId code, SpecializationDesignation designation, SpecializationDescription description)
    {
        this.Id = code ?? throw new ArgumentNullException(nameof(code), "Specialization Code cannot be null.");
        this.Designation = designation ?? throw new ArgumentNullException(nameof(designation), "Specialization Designation cannot be null.");
        this.Description = description ?? new SpecializationDescription("No description available.");
    }

    /**
     * Method that changes the designation of the specialization.
     */
    public void ChangeDesignation(SpecializationDesignation designation)
    {
        this.Designation = designation;
    }

    /**
     * Method that changes the description of the specialization.
     */
    public void ChangeDescription(SpecializationDescription description)
    {
        this.Description = description;
    }
}