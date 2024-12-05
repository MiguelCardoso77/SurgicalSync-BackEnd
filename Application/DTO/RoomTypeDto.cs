namespace DDDNetCore.Application.DTO;

/**
 * This class is a Data Transfer Object (DTO) that represents the RoomType entity.
 * It is used to transfer data between the Application and the Infrastructure layers.
 */
public class RoomTypeDto
{
    public string RoomTypeCode { get; set; }
    public string RoomTypeDesignation { get; set; }
    public string RoomTypeDescription { get; set; }
}