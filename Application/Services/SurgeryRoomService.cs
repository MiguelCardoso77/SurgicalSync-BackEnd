using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Application.DTO;
using DDDNetCore.Application.Mappers;
using DDDNetCore.Domain.Shared;
using DDDNetCore.Domain.SurgeryRooms;
using Microsoft.Extensions.Logging;

namespace DDDNetCore.Application.Services
{
    /**
     * Provides services for managing surgery rooms, including CRUD operations.
     */
    public class SurgeryRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISurgeryRoomsRepository _repo;
        private readonly ILogger _logger;
        private readonly SurgeryRoomMapper _mapper;
        /**
         * Initializes a new instance of the SurgeryRoomService class.
         *
         * @param unitOfWork Unit of work for transaction management.
         * @param repo Repository for surgery room data access.
         * @param logger Logger instance for logging events.
         * @param mapper Mapper for converting between domain objects and DTOs.
         */
        public SurgeryRoomService(IUnitOfWork unitOfWork, ISurgeryRoomsRepository repo,
            ILogger<SurgeryRoomService> logger, SurgeryRoomMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._repo = repo;
            this._logger = logger;
            this._mapper = mapper;
        }
        /**
         * Retrieves a surgery room by its room number.
         *
         * @param roomNumber The unique identifier of the surgery room.
         * @return The corresponding SurgeryRoomDto if found, otherwise null.
         */
        public async Task<SurgeryRoomDto> GetByIdAsync(RoomNumber roomNumber)
        {
            var surgeryRoom = await _repo.GetByIdAsync(roomNumber);

            if (surgeryRoom == null)
            {
                return null;
            }
            
            var response = _mapper.ToDto(surgeryRoom);
            
            return response;
        }
        /**
         * Retrieves all surgery rooms.
         *
         * @return A list of all SurgeryRoomDto.
         */
        public async Task<List<SurgeryRoomDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            
            List<SurgeryRoomDto> surgeryRoomDto = list.ConvertAll<SurgeryRoomDto>(sr => _mapper.ToDto(sr));

            return surgeryRoomDto;
        }
        /**
         * Adds a new surgery room.
         *
         * @param surgeryRoomDto The surgery room data to add.
         * @return The added SurgeryRoomDto.
         */
        public async Task<SurgeryRoomDto> AddAsync(SurgeryRoomDto surgeryRoomDto)
        {
            var list = await this._repo.GetAllAsync();
            
            var roomNumber = GenerateRoomNumber(surgeryRoomDto, list);
            
            var domainObj = _mapper.FromDto(surgeryRoomDto, roomNumber);
            
            await this._repo.AddAsync(domainObj);
            await this._unitOfWork.CommitAsync();
            
            _logger.LogInformation("Surgery room with Id: {RoomNumber} has been created.", domainObj.Id.AsString());
            
            var dto = _mapper.ToDto(domainObj);
            return dto;
        }
        /**
         * Updates an existing surgery room.
         *
         * @param surgeryRoomDto The surgery room data to update.
         * @return The updated SurgeryRoomDto, or null if not found.
         */
        public async Task<SurgeryRoomDto> UpdateAsync(SurgeryRoomDto surgeryRoomDto)
        {
            var surgeryRoom = await _repo.GetByIdAsync(new RoomNumber(surgeryRoomDto.RoomNumber));

            if (surgeryRoom == null)
            {
                return null;
            }
            
            surgeryRoom.ChangeMaintenanceSlots(new MaintenanceSlots(surgeryRoomDto.MaintenanceSlots));
            
            surgeryRoom.ChangeCurrentStatus(Enum.Parse<CurrentStatus>(surgeryRoomDto.CurrentStatus));
            
            await _unitOfWork.CommitAsync();
            
            return _mapper.ToDto(surgeryRoom);
        }
        /**
         * Inactivates (removes) a surgery room.
         *
         * @param roomNumber The unique identifier of the surgery room to remove.
         * @return The removed SurgeryRoomDto, or null if not found.
         */
        public async Task<SurgeryRoomDto> InactivateAsync(RoomNumber roomNumber)
        {
            var surgeryRoom = await _repo.GetByIdAsync(roomNumber);

            if (surgeryRoom == null)
            {
                return null;
            }
            
            _repo.Remove(surgeryRoom);
            await _unitOfWork.CommitAsync();
            
            return _mapper.ToDto(surgeryRoom);
        }
        /**
         * Generates a unique room number for a new surgery room.
         *
         * @param surgeryRoomDto The surgery room data for which to generate a room number.
         * @param list List of existing surgery rooms to ensure uniqueness.
         * @return A unique RoomNumber.
         */
        public RoomNumber GenerateRoomNumber(SurgeryRoomDto surgeryRoomDto, List<SurgeryRoom> list)
        {
            bool exists = list.Any(sr => sr.Id.AsString() == surgeryRoomDto.RoomNumber);

            if (exists)
            {
                return new RoomNumber(surgeryRoomDto.RoomNumber);
            }

            if (list.Count > 0)
            {
                var lastSurgeryRoom = list.Last();
        
                var lastId = lastSurgeryRoom.Id.AsString();

                int newId = int.Parse(lastId) + 1;
                return new RoomNumber(newId.ToString());
            }
            else
            {
                return new RoomNumber("1");
            }
        }
    }
}