using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Staffs;
using DDDNetCore.Domain;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.Users;

namespace DDDNetCore.Application.Services
{
    /**
     * Service class responsible for managing staff members, including retrieving, adding,
     * updating, and deactivating staff information.
     */
    public class StaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _repo;
        private readonly StaffMapper _mapper;

        /**
         * Initializes a new instance of the StaffService class.
         *
         * @param unitOfWork The unit of work for database operations.
         * @param repo The repository for staff data access.
         * @param mapper The mapper for converting between domain entities and DTOs.
         */
        public StaffService(IUnitOfWork unitOfWork, IStaffRepository repo, StaffMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._mapper = mapper;
        }

        /**
         * Retrieves all staff members as a list of StaffDtoList.
         *
         * @return A task that represents the asynchronous operation,
         *         containing a list of StaffDtoList objects.
         */
        public async Task<List<StaffDtoList>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();


            return _mapper.ToListDto(list);
        }


        /**
         * Retrieves a staff member by their ID.
         *
         * @param id The unique identifier for the staff member.
         * @return A task that represents the asynchronous operation,
         *         containing the StaffDto for the specified ID, or null if not found.
         */
        public async Task<StaffDto> GetByIdAsync(StaffId id)
        {
            var staff = await this._repo.GetByIdAsync(id);

            if (staff == null)
            {
                return null;
            }

            //var staffId = _mapper.ToDto(staff);

            return _mapper.ToDto(staff);
        }


        /**
         * Retrieves all staff members whose names contain the specified string.
         *
         * @param staffName The name string to filter staff members by.
         * @return A task that represents the asynchronous operation,
         *         containing a list of StaffDtoList objects that match the specified name.
         */
        public async Task<List<StaffDtoList>> GetAllByName(string staffName)
        {
            var list = await this._repo.GetAllAsync();

            // Filter list by Name
            var filteredList = list.Where(staff =>
                staff.StaffName.ToString().Contains(staffName, StringComparison.OrdinalIgnoreCase)).ToList();

            //Console.WriteLine(filteredList);
            var lists = _mapper.ToListDto(filteredList);
            return lists;
        }

        /**
         * Retrieves all staff members whose email addresses contain the specified string.
         *
         * @param staffEmail The email string to filter staff members by.
         * @return A task that represents the asynchronous operation,
         *         containing a list of StaffDtoList objects that match the specified email.
         */
        public async Task<List<StaffDtoList>> GetAllByEmail(string userEmail)
        {
            var list = await this._repo.GetAllAsync();

            var filteredList = list.Where(staff =>
                staff.UserEmail.ToString().Contains(userEmail, StringComparison.OrdinalIgnoreCase)).ToList();
            var lists = _mapper.ToListDto(filteredList);

            return lists;
        }

        /**
         * Retrieves all staff members with the specified specialization.
         *
         * @param staffSpecialization The specialization string to filter staff members by.
         * @return A task that represents the asynchronous operation,
         *         containing a list of StaffDtoList objects that match the specified specialization.
         */
        public async Task<List<StaffDtoList>> GetAllBySpecialization(string staffSpecialization)
        {
            var list = await this._repo.GetAllAsync();

            // Filter list by Specialization
            var filteredList = list.Where(staff =>
                    staff.StaffSpecialization.ToString()
                        .Contains(staffSpecialization, StringComparison.OrdinalIgnoreCase))
                .ToList();
            var lists = _mapper.ToListDto(filteredList);

            return lists;
        }

        /**
         * Deactivates a staff member identified by their ID.
         *
         * @param id The unique identifier for the staff member to deactivate.
         * @return A task that represents the asynchronous operation,
         *         containing the StaffDto for the deactivated staff member, or null if not found.
         */
        public async Task<StaffDto> DeactivateAsync(StaffId id)
        {
            var staff = await this._repo.GetByIdAsync(id);

            if (staff == null)
                return null;

            staff.IsActive = false;

            await this._unitOfWork.CommitAsync();

            return _mapper.ToDto(staff);
        }


        /**
        * Updates the information of an existing staff member.
        *
        * @param dto The StaffDto containing updated staff information.
        * @return A task that represents the asynchronous operation,
        *         containing the updated StaffDto, or null if the staff member was not found.
        */
        public async Task<StaffDto> UpdateAsync(StaffDto dto)
        {
            var staff = await this._repo.GetByIdAsync(new StaffId(dto.Id));

            if (staff == null)
                return null;

            var list = await this._repo.GetAllAsync();


            bool exists = list.Any(s =>
                (s.StaffPhoneNumber.ToString() == dto.StaffPhoneNumber || s.UserEmail.ToString() == dto.UserEmail) &&
                s.Id.AsString() != dto.Id);


            if (exists)
            {
                throw new InvalidOperationException("Já existe um registro com o mesmo phone number ou email.");
            }

            staff.ChangeStaffPhoneNumber(new StaffPhoneNumber(dto.StaffPhoneNumber));

            staff.ChangeUserEmail(new UserEmail(dto.UserEmail));


            staff.ChangeStaffSpecialization(new StaffSpecialization(dto.StaffSpecialization));


            staff.ChangeStaffAvailabilitySlots(new StaffAvailabilitySlots(dto.StaffAvailabilitySlots));

            var smtpEmailService = new EmailService();

            Console.WriteLine("email:", staff.UserEmail);
            var emailContent = $"Hello {staff.StaffName}! \nYour staff information was updated:\n\n" +
                               $"Email: {staff.UserEmail}\n\n" +
                               $"Phone Number: {staff.StaffPhoneNumber}\n\n" +
                               $"Specialization: {staff.StaffSpecialization}\n\n" +
                               $"Availability Slots: {staff.StaffAvailabilitySlots}";

            if (staff.StaffType.ToString() != dto.StaffType || staff.StaffName.ToString() != dto.StaffName ||
                staff.UserEmail.ToString() != dto.UserEmail)
            {
                throw new ArgumentException(
                    "Invalid update! You can only change staffPhoneNumber, userEmail, staffAvaiabilitySlots or staffSpecialization");
            }

            var email = new Email(emailContent, staff.UserEmail.ToString(), "Changes to Your Staff Information");

            await smtpEmailService.SendEmailAsync(email);

            await this._unitOfWork.CommitAsync();

            return _mapper.ToDto(staff);
        }

        /**
         * Adds a new staff member.
         *
         * @param dto The StaffDto containing information for the new staff member.
         * @return A task that represents the asynchronous operation,
         *         containing the newly created StaffDto.
         */
        public async Task<StaffDto> AddAsync(StaffDto dto)
        {
            var staffTypeString = dto.StaffType;

            if (!Enum.TryParse<StaffType>(staffTypeString, true, out var staffType))
            {
                throw new ArgumentException("Invalid staff type. Must be 'Doctor', 'Nurse', or 'Other'.");
            }

            var list = await this._repo.GetAllAsync();

            var staffId = GenerateLN(dto, staffType, list);
            var staff = _mapper.ToDomain(dto, staffId, new StaffAvailabilitySlots(dto.StaffAvailabilitySlots));

            await this._repo.AddAsync(staff);
            await this._unitOfWork.CommitAsync();

            return _mapper.ToDto(staff);
        }

        /**
         * Generates a unique StaffId for a new staff member.
         *
         * @param staffDto The StaffDto containing the information for the new staff member.
         * @param staffType The type of the staff member (Doctor, Nurse, Other).
         * @param list The list of existing staff members to check for duplicate phone numbers, emails, and license numbers.
         * @return A unique StaffId for the new staff member.
         */
        public StaffId GenerateLN(StaffDto staffDto, StaffType staffType, List<Staff> list)
        {
            int lastGeneratedNumber;
            bool exists = list.Any(s =>
                s.StaffPhoneNumber.ToString() == staffDto.StaffPhoneNumber ||
                s.UserEmail.ToString() == staffDto.UserEmail ||
                s.StaffLicenseNumber.ToString() == staffDto.StaffLicenseNumber);

            if (exists)
            {
                throw new InvalidOperationException(
                    "Já existe um registro com o mesmo phone number, email ou licenseNumber.");
            }

            char prefix = staffType switch
            {
                StaffType.Doctor => 'D',
                StaffType.Nurse => 'N',
                _ => 'O'
            };

            if (list.Count > 0)
            {
                 lastGeneratedNumber = list
                    .Select(s => int.TryParse(s.StaffLicenseNumber.ToString().Substring(5), out int num) ? num : 0)
                    .DefaultIfEmpty(0)
                    .Max();
            }
            else
            {
                 lastGeneratedNumber = 1;
            }

            int nextGeneratedNumber = lastGeneratedNumber + 1;


            string year = DateTime.Now.Year.ToString();

            string staffId = $"{prefix}{year}{nextGeneratedNumber:D5}";

            return new StaffId(staffId);
        }
    }
}