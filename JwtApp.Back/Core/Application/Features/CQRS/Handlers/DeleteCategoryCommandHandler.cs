using JwtApp.Back.Core.Application.Features.CQRS.Commands;
using JwtApp.Back.Core.Application.Interfaces;
using JwtApp.Back.Core.Domain;
using MediatR;

namespace JwtApp.Back.Core.Application.Features.CQRS.Handlers
{
    public class DeleteCategoryCommandHandler:IRequestHandler<DeleteCategoryCommandRequest>
    {
        private readonly IRepository<Category> repository;

        public DeleteCategoryCommandHandler(IRepository<Category> repository)
        {
            this.repository = repository;
        
        }

        public async Task<Unit> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        { 
            var deletedEntitiy=await this.repository.GetByIdAsync(request.Id);
            if (deletedEntitiy!=null)
            {
                await this.repository.RemoveAsync(deletedEntitiy);
            }
            return Unit.Value;
        }
    }

}
