using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.RoomTypes;
using DDDNetCore.Domain.Shared;

namespace DDDNetCore.Application.Services;

public class RoomTypeService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoomTypeRepository _repo;
    private readonly RoomTypeMapper _mapper;
    
    public RoomTypeService(IUnitOfWork unitOfWork, IRoomTypeRepository repo, RoomTypeMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repo = repo;
        _mapper = mapper;
    }
    
    public async Task<List<RoomTypeDto>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return _mapper.ToListDto(list);
    }
    
    public async Task<RoomTypeDto> GetByCodeAsync(string code)
    {
        var roomType = await _repo.GetByIdAsync(new RoomTypeId(code));
        return _mapper.ToDto(roomType);
    }
    
    public async Task<RoomTypeDto> AddAsync(RoomTypeDto roomTypeDto)
    {
        var roomType = _mapper.ToDomain(roomTypeDto);
        await _repo.AddAsync(roomType);
        await _unitOfWork.CommitAsync();
        return _mapper.ToDto(roomType);
    }
}