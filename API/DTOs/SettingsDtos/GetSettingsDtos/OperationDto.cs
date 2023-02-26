using API.DTOs.SettingsDtos.EditSettingsDtos;

namespace API.DTOs.SettingsDtos.GetSettingsDtos;

public record OperationDto(Guid Id, string Name) : OperationIdDto(Id);