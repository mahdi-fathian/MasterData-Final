using MediatR;

namespace MasterData.CQRS.Application.Commands;

/// <summary>
/// دستور حذف استان - Delete Province Command
/// </summary>
public record DeleteProvinceCommand(int Id) : IRequest<bool>;
