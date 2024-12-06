using System.Collections.Generic;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.RoomTypes;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.Mappers;

[TestFixture]
public class RoomTypeMapperTests
{
    private RoomTypeMapper _mapper;
    private Mock<RoomTypeId> _mockRoomTypeId;
    private Mock<RoomTypeDesignation> _mockRoomTypeDesignation;
    private Mock<RoomTypeDescription> _mockRoomTypeDescription;
    
    [SetUp]
    public void Setup()
    {
        _mapper = new RoomTypeMapper();
        _mockRoomTypeId = new Mock<RoomTypeId>("123--321");
        _mockRoomTypeDesignation = new Mock<RoomTypeDesignation>("Operating Room");
        _mockRoomTypeDescription = new Mock<RoomTypeDescription>("A room for surgeries");
    }
    
    [Test]
    public void TestToDto()
    {
        var roomType = new RoomType(_mockRoomTypeId.Object, _mockRoomTypeDesignation.Object, _mockRoomTypeDescription.Object);
        var roomTypeDto = _mapper.ToDto(roomType);
        
        Assert.AreEqual(roomType.Id.AsString(), roomTypeDto.RoomTypeCode);
        Assert.AreEqual(roomType.Designation.Value, roomTypeDto.RoomTypeDesignation);
        Assert.AreEqual(roomType.Description.Value, roomTypeDto.RoomTypeDescription);
    }
    
    [Test]
    public void TestToDomain()
    {
        var roomTypeDto = new RoomTypeDto
        {
            RoomTypeCode = "123-321",
            RoomTypeDesignation = "Operating Room",
            RoomTypeDescription = "A room for surgeries"
        };
        var roomType = _mapper.ToDomain(roomTypeDto);
        
        Assert.AreEqual(roomTypeDto.RoomTypeCode, roomType.Id.AsString());
        Assert.AreEqual(roomTypeDto.RoomTypeDesignation, roomType.Designation.Value);
        Assert.AreEqual(roomTypeDto.RoomTypeDescription, roomType.Description.Value);
    }
    
    [Test]
    public void TestToListDto()
    {
        var roomTypes = new List<RoomType>
        {
            new (_mockRoomTypeId.Object, _mockRoomTypeDesignation.Object, _mockRoomTypeDescription.Object),
            new (_mockRoomTypeId.Object, _mockRoomTypeDesignation.Object, _mockRoomTypeDescription.Object)
        };
        var roomTypeDtos = _mapper.ToListDto(roomTypes);
        
        Assert.AreEqual(roomTypes.Count, roomTypeDtos.Count);
        for (var i = 0; i < roomTypes.Count; i++)
        {
            Assert.AreEqual(roomTypes[i].Id.AsString(), roomTypeDtos[i].RoomTypeCode);
            Assert.AreEqual(roomTypes[i].Designation.Value, roomTypeDtos[i].RoomTypeDesignation);
            Assert.AreEqual(roomTypes[i].Description.Value, roomTypeDtos[i].RoomTypeDescription);
        }
    }
}