using System.Collections.Generic;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Application.Services;
using DDDNetCore.Controllers;
using DDDNetCore.Domain.RoomTypes;
using DDDNetCore.Domain.Shared;
using Moq;
using NUnit.Framework;

namespace DDDNetCore.SurgicalSync.Tests.Controllers;

[TestFixture]
public class RoomTypesControllerTests
{
    private RoomTypesController _controller;
    private RoomTypeService _service;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IRoomTypeRepository> _repoMock;
    private RoomTypeMapper _mapper;
    
    [SetUp]
    public void SetUp()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _repoMock = new Mock<IRoomTypeRepository>();
        _mapper = new RoomTypeMapper();
        _service = new RoomTypeService(_unitOfWorkMock.Object, _repoMock.Object, _mapper);
        
        _controller = new RoomTypesController(_service);
    }

    [Test]
    public void TestGetAll()
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
        
        var result = _controller.GetAll();
        
        // Assert
        Assert.IsNotNull(result);
    }
    
    [Test]
    public void TestGetByCode()
    {
        // Arrange
        var code = new RoomTypeId("123--321");
        var des = new RoomTypeDesignation("Test Designation");
        var description = new RoomTypeDescription("Test Description");
        
        var roomType = new RoomType(code, des, description);
        _repoMock.Setup(x => x.GetByIdAsync(code)).ReturnsAsync(roomType);
        
        // Act
        var result = _controller.GetByCode("123--321");
        
        // Assert
        Assert.IsNotNull(result);
    }
    
    [Test]
    public void TestCreate()
    {
        // Arrange
        var code = new RoomTypeId("123--321");
        var des = new RoomTypeDesignation("Test Designation");
        var description = new RoomTypeDescription("Test Description");
        
        var roomType = new RoomType(code, des, description);
        _repoMock.Setup(x => x.AddAsync(roomType)).ReturnsAsync(roomType);
        
        // Act
        var result = _controller.Create(new RoomTypeDto{ RoomTypeCode = "123--321", RoomTypeDesignation ="Test Designation", RoomTypeDescription ="Test Description" });
        
        // Assert
        Assert.IsNotNull(result);
    }

}