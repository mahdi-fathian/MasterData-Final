using MediatR;

namespace MasterData.CQRS.Application.Commands;

/// <summary>
/// دستور ثبت استان جدید - Register Province Command
/// </summary>
public record RegisterProvinceCommand(string Name) : IRequest<RegisterProvinceCommandResult>;

/// <summary>
/// نتیجه دستور ثبت استان - Register Province Command Result
/// </summary>
public record RegisterProvinceCommandResult(int Id, string Name, DateTime CreatedAt);
