using MediatR;

namespace MasterData.CQRS.Application.Commands;


public record UpdateProvinceCommand(int Id, string Name) : IRequest<bool>;
