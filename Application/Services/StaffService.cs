using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Staffs;
using DDDSample1.Domain.Shared;
using DDDNetCore.Domain;
using DDDNetCore.Domain.Users;
using FirebaseAdmin;
using NUnit.Framework;


namespace DDDNetCore.Application.Services
{
    public class StaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _repo;
        private readonly StaffMapper _mapper;
        private static int _lastGeneratedNumber = 00003;


        public StaffService(IUnitOfWork unitOfWork, IStaffRepository repo, StaffMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._mapper = mapper;
        }

        public async Task<List<StaffDto2>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();


            return _mapper.ToListDto(list);
        }


        public async Task<StaffDto> GetByIdAsync(StaffId id)
        {
            var staff = await this._repo.GetByIdAsync(id);

            if (staff == null)
            {
                return null;
            }

            var staffId = _mapper.ToDto(staff);

            return _mapper.ToDto(staff);
        }


        public async Task<List<StaffDto2>> GetAllByName(string staffName)
        {
            var list = await this._repo.GetAllAsync();

            // Filter list by Name
            var filteredList = list.Where(staff => staff.StaffName.ToString().Contains(staffName, StringComparison.OrdinalIgnoreCase)).ToList();

            //Console.WriteLine(filteredList);
            var lists = _mapper.ToListDto(filteredList);
            return lists;
        }

        public async Task<List<StaffDto2>> GetAllByEmail(string staffEmail)
        {
            var list = await this._repo.GetAllAsync();

            var filteredList = list.Where(staff => staff.UserEmail.ToString().Contains(staffEmail, StringComparison.OrdinalIgnoreCase)).ToList();
            var lists = _mapper.ToListDto(filteredList);

            return lists;
        }

        public async Task<List<StaffDto2>> GetAllBySpecialization(string staffSpecialization)
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

        
        public async Task<StaffDto> DeactivateAsync(StaffId id)
        {
            var staff = await this._repo.GetByIdAsync(id);

            if (staff == null)
                return null;

            staff.IsActive = false;

            await this._unitOfWork.CommitAsync();

            return _mapper.ToDto(staff);
        }


        public async Task<StaffDto> UpdateAsync(StaffDto dto)
        {
            var staff = await this._repo.GetByIdAsync(new StaffId(dto.Id));

            if (staff == null)
                return null;

            var list = await this._repo.GetAllAsync();

            
            bool exists = list.Any(s => (s.StaffPhoneNumber.ToString() == dto.StaffPhoneNumber || s.UserEmail.ToString() == dto.UserEmail) );

            bool exist2 = list.Any(s => s.Id.AsString() != dto.Id);

            if (exists && exist2)
            {
                throw new InvalidOperationException("Já existe um registro com o mesmo phone number ou email.");
            }

            staff.ChangeStaffPhoneNumber(new StaffPhoneNumber(dto.StaffPhoneNumber));

            staff.ChangeUserEmail(new UserEmail(dto.UserEmail));

            if (Enum.TryParse(dto.StaffSpecialization, out StaffSpecialization specialization))
            {
                staff.ChangeStaffSpecialization(specialization);
            }
            else
            {
                throw new ArgumentException("Invalid specialization value");
            }


            var avaiabilitySlots = dto.StaffAvaiabilitySlots.Select(st => new StaffAvaiabilitySlots(st)).ToList();
            List<StaffAvaiabilitySlots> staffAvaiabilitySlots = new List<StaffAvaiabilitySlots>();
            for (int i = 0; i < avaiabilitySlots.Count; i++)
            {
                staffAvaiabilitySlots.Add(new StaffAvaiabilitySlots(dto.StaffAvaiabilitySlots[i]));
            }

            staff.ChangeStaffAvaiabilitySlots(staffAvaiabilitySlots);

            var smtpEmailService = new EmailService();

            var avaiabilitySlotsText = string.Join(", ", avaiabilitySlots);

            var emailContent = $"Hello {staff.StaffName}! \nYour staff information was updated:\n\n" +
                               $"Email: {staff.UserEmail}\n\n" +
                               $"Phone Number: {staff.StaffPhoneNumber}\n\n" +
                               $"Specialization: {staff.StaffSpecialization.ToString()}\n\n" +
                               $"Availability Slots: {avaiabilitySlotsText}";

            if (staff.StaffType.ToString() != dto.StaffType || staff.StaffName.ToString() != dto.StaffName ||
                staff.UserEmail.ToString() != dto.UserEmail)
            {
                throw new ArgumentException(
                    "Invalid update! You can only change staffPhoneNumber, userEmail, staffAvaiabilitySlots or staffSpecialization");
            }

            var email = new Email(emailContent, staff.UserEmail.ToString(), "Changes to Your Staff Information");

            await smtpEmailService.SendEmailAsync(email);
            Console.WriteLine($"Successfully sent the email to {email.Destination}");

            await this._unitOfWork.CommitAsync();

            return _mapper.ToDto(staff);
        }


        public async Task<StaffDto> AddAsync(StaffDto dto)
        {
            var staffTypeString = dto.StaffType;

            if (!Enum.TryParse<StaffType>(staffTypeString, true, out var staffType))
            {
                throw new ArgumentException("Invalid staff type. Must be 'Doctor', 'Nurse', or 'Other'.");
            }

            var list = await this._repo.GetAllAsync();

            var staffId = GenerateLN(dto, staffType, list);
            var staff = _mapper.ToDomain(dto, staffId,dto.StaffAvaiabilitySlots.Select(st => new StaffAvaiabilitySlots(st)).ToList());

            await this._repo.AddAsync(staff);
            await this._unitOfWork.CommitAsync();

            return _mapper.ToDto(staff);
        }

        public static StaffId GenerateLN(StaffDto staffDto, StaffType staffType, List<Staff> list)
        {
            bool exists = list.Any(s =>
                s.StaffPhoneNumber.ToString() == staffDto.StaffPhoneNumber ||
                s.UserEmail.ToString() == staffDto.UserEmail || s.StaffLicenseNumber.ToString() == staffDto.StaffLicenseNumber);

            if (exists)
            {
                throw new InvalidOperationException("Já existe um registro com o mesmo phone number ou email ou licenseNumber.");
            }


            char prefix = staffType switch
            {
                StaffType.Doctor => 'D',
                StaffType.Nurse => 'N',
                _ => 'O'
            };

            string year = DateTime.Now.Year.ToString();

            _lastGeneratedNumber = (_lastGeneratedNumber + 1) % 10000;

            string staffId = $"{prefix}{year}{_lastGeneratedNumber:D5}";

            return new StaffId(staffId);
        }
    }
}