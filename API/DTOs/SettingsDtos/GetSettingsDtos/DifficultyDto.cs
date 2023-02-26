using API.DTOs.SettingsDtos.EditSettingsDtos;

namespace API.DTOs.SettingsDtos.GetSettingsDtos;

public sealed record DifficultyDto(Guid Id, string Name) : DifficultyIdDto(Id);