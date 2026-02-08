using MediatR;

namespace MasterData.CQRS.Application.Commands;


public record DeleteProvinceCommand(int Id) : IRequest<bool>;
