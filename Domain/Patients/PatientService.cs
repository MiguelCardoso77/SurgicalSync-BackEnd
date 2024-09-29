using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Patients
{
    public class PatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPatientRepository _repo;
        
        public PatientService(IUnitOfWork unitOfWork, IPatientRepository repo)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
        }
        
        public async Task<List<PatientDto>> GetAllAsync()
        {
            var list = await this._repo.GetAllAsync();
            
            List<PatientDto> listDto = list.ConvertAll<PatientDto>(pat => new PatientDto{});
            
            return listDto;
        }
        
        public async Task<PatientDto> GetByIdAsync(PatientId id)
        {
            var pat = await this._repo.GetByIdAsync(id);
            
            return pat == null ? null : new PatientDto{};
        }
        
        public async Task<PatientDto> AddAsync(PatientDto dto)
        {
            //var patient = new Patient(dto.Id, dto.Name);
            
            //await this._repo.AddAsync(patient);
            
            await this._unitOfWork.CommitAsync();
            
            return new PatientDto {};
        }
        
        public async Task<PatientDto> UpdateAsync(PatientDto dto)
        {
            //var patient = await this._repo.GetByIdAsync(new PatientId(dto.Id)); 
            
            //if (patient == null)
                return null;   
            
            //patient.ChangeEmergencyContact(dto.EmergencyContact);
            
            await this._unitOfWork.CommitAsync();
            
            return new PatientDto {};
        }
        
        public async Task<PatientDto> DeleteAsync(PatientId id)
        {
            var patient = await this._repo.GetByIdAsync(id);
            
            if (patient == null)
                return null;
            
            this._repo.Remove(patient);
            await this._unitOfWork.CommitAsync();
            
            return new PatientDto {};
        }
    }
}