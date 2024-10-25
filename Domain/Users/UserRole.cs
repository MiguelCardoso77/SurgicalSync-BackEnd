namespace DDDNetCore.Domain.Users
{
    /**
     * UserRole is an enum that represents all the possible roles that a User can have.
     */
    public enum UserRole
    {
        None,
        Admin,
        Doctor,
        Nurse,
        Technician,
        Patient
    }
}