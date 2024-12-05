using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Application.DTO;
using DDDNetCore.Domain.RoomTypes;

namespace DDDNetCore.Application.Mappers;

public class RoomTypeMapper
{
    public RoomTypeDto ToDto(RoomType roomType)
    {
        return new RoomTypeDto
        {
            RoomTypeCode = roomType.Id.Value,
            RoomTypeDesignation = roomType.Designation.Value,
            RoomTypeDescription = roomType.Description.Value,
        };
    }
    
    public RoomType ToDomain(RoomTypeDto roomTypeDto)
    {
        return new RoomType(
            new RoomTypeId(roomTypeDto.RoomTypeCode), 
            new RoomTypeDesignation(roomTypeDto.RoomTypeDesignation), 
            new RoomTypeDescription(roomTypeDto.RoomTypeDescription)
        );
    }
    
    public List<RoomTypeDto> ToListDto(List<RoomType> roomTypes)
    {
        return roomTypes.Select(ToDto).ToList();
    }
}