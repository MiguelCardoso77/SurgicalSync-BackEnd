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


namespace DDDNetCore.Application.Services
{
    public class StaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStaffRepository _repo;
        
        public StaffService(IUnitOfWork unitOfWork, IStaffRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        } 
        
        public async Task<List<StaffDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();

            List<StaffDto> listDto = list.ConvertAll<StaffDto>(staff => new StaffDto
            {
                Id = staff.Id.AsString(),
                StaffName = staff.StaffName.ToString(),
                StaffEmail = staff.StaffEmail.ToString(),
                StaffPhoneNumber = staff.StaffPhoneNumber.ToString(),
                StaffSpecialization = staff.StaffSpecialization.ToString(),
                StaffAvaiabilitySlots = staff.StaffAvaiabilitySlots.Select(rs => rs.StaffAvaiabilitySlotsValue).ToList(),
                //StaffType = staff.StaffType.ToString()
            });

            return listDto;
        }
        
        public async Task<StaffDto> GetByIdAsync(LicenseNumber id)
        {
            var staff = await this._repo.GetByIdAsync(id);

            if (staff == null)
            {
                return null;
            }

            return new StaffDto
            {
                Id = staff.Id.ToString(), StaffName = staff.StaffName.ToString(), StaffEmail = staff.StaffEmail.ToString(),
                StaffPhoneNumber = staff.StaffPhoneNumber.ToString(), StaffSpecialization = staff.StaffSpecialization.ToString(), StaffAvaiabilitySlots = staff.StaffAvaiabilitySlots.Select(rs=> rs.StaffAvaiabilitySlotsValue).ToList()
            };
        }
        
        public async Task<StaffDto> DeleteAsync(LicenseNumber id)
        {
            var staff = await this._repo.GetByIdAsync(id);

            if (staff == null)
                return null;

            this._repo.Remove(staff);
            await this._unitOfWork.CommitAsync();

            return new StaffDto()
            {
                Id = staff.Id.AsString(),
                StaffName = staff.StaffName.ToString(),
                StaffEmail = staff.StaffEmail.ToString(),
                StaffPhoneNumber = staff.StaffPhoneNumber.ToString(),
                StaffSpecialization = staff.StaffSpecialization.ToString(),
                StaffAvaiabilitySlots = staff.StaffAvaiabilitySlots.Select(rs => rs.StaffAvaiabilitySlotsValue).ToList(),
                //StaffType = staff.StaffType.ToString()
            };
        }
        
        public async Task<StaffDto> DeactivateAsync(LicenseNumber id)
        {
            var staff = await this._repo.GetByIdAsync(id);

            if (staff == null)
                return null;

            // Marcando o registro como inativo
            staff.IsActive = false;

            // Como o objeto 'staff' é rastreado pelo contexto do Entity Framework, não é necessário chamar um método 'Update'
            await this._unitOfWork.CommitAsync();

            return new StaffDto()
            {
                Id = staff.Id.AsString(),
                StaffName = staff.StaffName.ToString(),
                StaffEmail = staff.StaffEmail.ToString(),
                StaffPhoneNumber = staff.StaffPhoneNumber.ToString(),
                StaffSpecialization = staff.StaffSpecialization.ToString(),
                StaffAvaiabilitySlots = staff.StaffAvaiabilitySlots.Select(rs => rs.StaffAvaiabilitySlotsValue).ToList(),
                //StaffType = staff.StaffType.ToString()
            };
        }

        
        
        public async Task<StaffDto> UpdateAsync(StaffDto dto)
        {
            var staff = await this._repo.GetByIdAsync(new LicenseNumber(dto.Id));

            if (staff == null)
                return null;

            var phoneNumber = staff.StaffPhoneNumber.ToString();
            var avaiabilitySlots = staff.StaffAvaiabilitySlots.ToString();
            var specialization = staff.StaffSpecialization.ToString();
            
            // change all fields
            staff.ChangeStaffSpecialization(new StaffSpecialization());
            staff.ChangeStaffPhoneNumber(new StaffPhoneNumber(dto.StaffPhoneNumber));

            List<StaffAvaiabilitySlots> staffAvaiabilitySlots = new List<StaffAvaiabilitySlots>();
            for (int i = 0; i < dto.StaffAvaiabilitySlots.Count; i++)
            { 
                staffAvaiabilitySlots.Add(new StaffAvaiabilitySlots(dto.StaffAvaiabilitySlots[i]));
            }
            
            staff.ChangeStaffAvaiabilitySlots(staffAvaiabilitySlots);
            
            // Send set-up email to user
            var smtpEmailService = new EmailService();
            var emailContent = $"Hello {staff.StaffName}! \n Your  staff information was changed. Now it is : \n phone number : {phoneNumber}, \nspecialization : {specialization} \n avaiability Slots email : {avaiabilitySlots.ToList()}";
            var email = new Email(emailContent, staff.StaffEmail.ToString() , "Changes On Your Contact Information");
            await smtpEmailService.SendEmailAsync(email);
            Console.WriteLine($"Successfully sent the email to {email.Destination}");

            await this._unitOfWork.CommitAsync();


            return new StaffDto()
            {
                Id = staff.Id.AsString(),
                StaffName = staff.StaffName.ToString(),
                StaffEmail = staff.StaffEmail.ToString(),
                StaffPhoneNumber = staff.StaffPhoneNumber.ToString(),
                StaffSpecialization = staff.StaffSpecialization.ToString(),
                StaffAvaiabilitySlots = staff.StaffAvaiabilitySlots.Select(rs => rs.StaffAvaiabilitySlotsValue).ToList(),
                //StaffType = staff.StaffType.ToString()
            };
            
            
            
        }

        
        public async Task<StaffDto> AddAsync(StaffDto dto)
        {
            var avaiabilitySlotsList = new List<StaffAvaiabilitySlots>();
            
            String staffTypeString = dto.StaffType;

            if (!Enum.TryParse<StaffType>(staffTypeString, true, out var staffType))
            {
                throw new ArgumentException("Invalid staff type. Must be 'Doctor', 'Nurse', or 'Other'.");
            }
            LicenseNumber licenseNumber = LicenseNumberService.GenerateLN(staffType);
            
            var staff = StaffMapper.ToDomain(dto, licenseNumber , avaiabilitySlotsList);

            await this._repo.AddAsync(staff);
            await this._unitOfWork.CommitAsync();
            

            return new StaffDto()
            {
                StaffName = staff.StaffName.ToString(),
                StaffEmail = staff.StaffEmail.ToString(),
                StaffPhoneNumber = staff.StaffPhoneNumber.ToString(),
                StaffSpecialization = staff.StaffSpecialization.ToString(),
                StaffAvaiabilitySlots = staff.StaffAvaiabilitySlots.Select(rs => rs.StaffAvaiabilitySlotsValue).ToList(),
                StaffType = staff.StaffType.ToString()
            };
        }

        
        
    }
}