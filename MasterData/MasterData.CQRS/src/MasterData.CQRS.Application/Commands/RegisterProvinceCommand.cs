using MediatR;

namespace MasterData.CQRS.Application.Commands;


public record RegisterProvinceCommand(string Name) : IRequest<RegisterProvinceCommandResult>;


public record RegisterProvinceCommandResult(int Id, string Name, DateTime CreatedAt);
