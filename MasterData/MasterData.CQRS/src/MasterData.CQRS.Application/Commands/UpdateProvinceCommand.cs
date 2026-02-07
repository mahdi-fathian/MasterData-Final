using MediatR;

namespace MasterData.CQRS.Application.Commands;

/// <summary>
/// دستور بروزرسانی استان - Update Province Command
/// </summary>
public record UpdateProvinceCommand(int Id, string Name) : IRequest<bool>;
