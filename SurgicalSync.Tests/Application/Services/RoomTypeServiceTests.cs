using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Application.Services;
using DDDNetCore.Domain.RoomTypes;
using DDDNetCore.Domain.Shared;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Application.Services;

[TestFixture]
public class RoomTypeServiceTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IRoomTypeRepository> _repoMock;
    private RoomTypeMapper _mapper;
    private RoomTypeService _service;
    
    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _repoMock = new Mock<IRoomTypeRepository>();
        _mapper = new RoomTypeMapper();
        _service = new RoomTypeService(_unitOfWorkMock.Object, _repoMock.Object, _mapper);
    }
    
    [Test]
    public async Task TestGetAllAsync()
    {
        // Arrange
        var code1 = new RoomTypeId("123--321");
        var code2 = new RoomTypeId("456--654");
        
        var des1 = new RoomTypeDesignation("Test Designation 1");
        var des2 = new RoomTypeDesignation("Test Designation 2");
        
        var description1 = new RoomTypeDescription("Test Description 1");
        var description2 = new RoomTypeDescription("Test Description 2");
        
        var roomType1 = new RoomType(code1, des1, description1);
        var roomType2 = new RoomType(code2, des2, description2);
        
        var roomTypes = new List<RoomType> { roomType1, roomType2 };
        _repoMock.Setup(x => x.GetAllAsync()).ReturnsAsync(roomTypes);
        
        // Act
        var result = await _service.GetAllAsync();
        
        // Assert
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("123--321", result[0].RoomTypeCode);
        Assert.AreEqual("Test Designation 1", result[0].RoomTypeDesignation);
        Assert.AreEqual("Test Description 1", result[0].RoomTypeDescription);
        Assert.AreEqual("456--654", result[1].RoomTypeCode);
        Assert.AreEqual("Test Designation 2", result[1].RoomTypeDesignation);
        Assert.AreEqual("Test Description 2", result[1].RoomTypeDescription);
    }
    
    [Test]
    public async Task TestGetByCodeAsync()
    {
        // Arrange
        var code1 = new RoomTypeId("123--321");
        var des1 = new RoomTypeDesignation("Test Designation 1");
        var description1 = new RoomTypeDescription("Test Description 1");
        var roomType1 = new RoomType(code1, des1, description1);
        _repoMock.Setup(x => x.GetByIdAsync(new RoomTypeId("123--321"))).ReturnsAsync(roomType1);
        
        // Act
        var result = await _service.GetByCodeAsync("123--321");
        
        // Assert
        Assert.AreEqual("123--321", result.RoomTypeCode);
        Assert.AreEqual("Test Designation 1", result.RoomTypeDesignation);
        Assert.AreEqual("Test Description 1", result.RoomTypeDescription);
    }
    
    [Test]
    public async Task TestAddAsync()
    {
        // Arrange
        var roomTypeDto = new RoomTypeDto { RoomTypeCode = "222--333", RoomTypeDesignation = "Test Room Type", RoomTypeDescription = "Testing" };
        
        // Act
        var result = await _service.AddAsync(roomTypeDto);
        
        // Assert
        Assert.AreEqual("222--333", result.RoomTypeCode);
        Assert.AreEqual("Test Room Type", result.RoomTypeDesignation);
        Assert.AreEqual("Testing", result.RoomTypeDescription);
    }
    
    [Test]
    public void TestAddInvalidRoomTypeAsync()
    {
        // Arrange
        var roomTypeDto = new RoomTypeDto { RoomTypeCode = "2;3", RoomTypeDesignation = "Test Room Type", RoomTypeDescription = "Testing" };
        
        var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _service.AddAsync(roomTypeDto));
        Assert.AreEqual("RoomTypeCode must be 8 characters long and contain only letters, numbers, and dashes.", ex.Message);
    }
}