using eScout_v2.Application.Entities;

namespace eScout_v2.Application.UseCases.Inputs;

public record ActivityInput(string Title, string Description, Guid PlaceId, DateTime StartTime, DateTime EndTime);