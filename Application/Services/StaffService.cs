using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Staffs;
using DDDSample1.Domain.Shared;
using DDDNetCore.Domain;
using FirebaseAdmin;
using NUnit.Framework;


namespace DDDNetCore.Application.Services
{
    public class StaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _repo;
        private readonly StaffMapper _mapper;

        
        public StaffService(IUnitOfWork unitOfWork, IStaffRepository repo, StaffMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._mapper = mapper;
        } 
        
        public async Task<List<StaffDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

           
            return _mapper.ToListDto(list);
        }
        
        
        public async Task<StaffDto> GetByIdAsync(LicenseNumber id)
        {
            var staff = await this._repo.GetByIdAsync(id);

            if (staff == null)
            {
                return null;
            }

            var staffId = _mapper.ToDto(staff);

            return _mapper.ToDto(staff);
        }
        
        
        public async Task<List<StaffDto>> GetAllByName(string staffName)
        {
            var list = await this._repo.GetAllAsync();
            
            // Filter list by Name
            var filteredList = list.Where(staff => staff.StaffName.ToString().Contains(staffName, StringComparison.OrdinalIgnoreCase)).ToList();
    
            //Console.WriteLine(filteredList);
            var lists = _mapper.ToListDto(filteredList);
            return lists;
        }
        
        public async Task<List<StaffDto>> GetAllByEmail(string staffEmail)
        {
            var list = await this._repo.GetAllAsync();
            
            // Filter list by Email
            var filteredList = list.Where(staff => staff.StaffEmail.ToString().Contains(staffEmail, StringComparison.OrdinalIgnoreCase)).ToList();
            var lists = _mapper.ToListDto(filteredList);

            return lists;
        }
        
        public async Task<List<StaffDto>> GetAllBySpecialization(string staffSpecialization)
        {
            var list = await this._repo.GetAllAsync();
            
            // Filter list by Specialization
            var filteredList = list.Where(staff => staff.StaffSpecialization.ToString().Contains(staffSpecialization, StringComparison.OrdinalIgnoreCase)).ToList();
            var lists = _mapper.ToListDto(filteredList);

            return lists;
        }

    public async Task<StaffDto> DeleteAsync(LicenseNumber id)
        {
            var staff = await this._repo.GetByIdAsync(id);

            if (staff == null)
                return null;

            this._repo.Remove(staff);
            await this._unitOfWork.CommitAsync();

            return _mapper.ToDto(staff);
        }
        
        public async Task<StaffDto> DeactivateAsync(LicenseNumber id)
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
    var staff = await this._repo.GetByIdAsync(new LicenseNumber(dto.Id));

    if (staff == null)
        return null;

    var phoneNumber = dto.StaffPhoneNumber.ToString();
    staff.ChangeStaffPhoneNumber(new StaffPhoneNumber(dto.StaffPhoneNumber));
    
    if (Enum.TryParse(dto.StaffSpecialization, out StaffSpecialization specialization))
    {
        staff.ChangeStaffSpecialization(specialization);
    }
    else
    {
        throw new ArgumentException("Invalid specialization value");
    }
    staff.ChangeStaffSpecialization(specialization);
    
    var avaiabilitySlots = dto.StaffAvaiabilitySlots.Select(st => new StaffAvaiabilitySlots(st)).ToList();
    List<StaffAvaiabilitySlots> staffAvaiabilitySlots = new List<StaffAvaiabilitySlots>();
    for (int i = 0; i < dto.StaffAvaiabilitySlots.Count; i++)
    { 
        staffAvaiabilitySlots.Add(new StaffAvaiabilitySlots(dto.StaffAvaiabilitySlots[i]));
    }
    staff.ChangeStaffAvaiabilitySlots(staffAvaiabilitySlots);
    
    var smtpEmailService = new EmailService();
    
    var avaiabilitySlotsText = string.Join(", ", staff.StaffAvaiabilitySlots.Select(slot => slot.StaffAvaiabilitySlotsValue));

    var emailContent = $"Hello {staff.StaffName}! \nYour staff information was updated:\n\n" +
                       $"Phone Number: {staff.StaffPhoneNumber.ToString()}\n\n" +
                       $"Specialization: {staff.StaffSpecialization.ToString()}\n\n" +
                       $"Availability Slots: {avaiabilitySlotsText}";

    if (staff.StaffType.ToString() != dto.StaffType || staff.StaffName.ToString() != dto.StaffName || staff.StaffEmail.ToString() != dto.StaffEmail)
    {
        throw new ArgumentException("Invalid update! You can only change staffPhoneNumber, staffAvaiabilitySlots or staffSpecialization");    
    }

    var email = new Email(emailContent, staff.StaffEmail.ToString(), "Changes to Your Staff Information");

    await smtpEmailService.SendEmailAsync(email);
    Console.WriteLine($"Successfully sent the email to {email.Destination}");

    await this._unitOfWork.CommitAsync();

    return _mapper.ToDto(staff);
}

        
        public async Task<StaffDto> AddAsync(StaffDto dto)
        {
            var avaiabilitySlotsList = new List<StaffAvaiabilitySlots>();
            
            String staffTypeString = dto.StaffType;

            if (!Enum.TryParse<StaffType>(staffTypeString, true, out var staffType))
            {
                throw new ArgumentException("Invalid staff type. Must be 'Doctor', 'Nurse', or 'Other'.");
            }
            
            var list = await this._repo.GetAllAsync();

            LicenseNumber licenseNumber = LicenseNumberService.GenerateLN( dto,staffType, list);
            
            var staff = StaffMapper.ToDomain(dto, licenseNumber , avaiabilitySlotsList);

            await this._repo.AddAsync(staff);
            await this._unitOfWork.CommitAsync();
            

            return _mapper.ToDto(staff);
        }

        
        
    }
}