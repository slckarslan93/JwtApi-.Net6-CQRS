using JwtApp.Back.Core.Application.Enums;
using JwtApp.Back.Core.Application.Features.CQRS.Commands;
using JwtApp.Back.Core.Application.Interfaces;
using JwtApp.Back.Core.Domain;
using MediatR;

namespace JwtApp.Back.Core.Application.Features.CQRS.Handlers
{
    public class ReqisterUserCommandHandler : IRequestHandler<ReqisterUserCommandRequest>
    {
        private readonly IRepository<AppUser> repository;

        public ReqisterUserCommandHandler(IRepository<AppUser> repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(ReqisterUserCommandRequest request, CancellationToken cancellationToken)
        {
            await this.repository.CreateAsync(new AppUser
            {
                AppRoleId=(int)RoleType.member,
                Password=request.Password,
                UserName=request.Username,
            });
            return Unit.Value;  
        }
    }
}
