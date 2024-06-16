using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler
    (IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public ILogger<GetProductByIdQueryHandler> Logger { get; } = logger;

    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        Logger.LogInformation("GetProductByIdQueryHandler.Called with {@query}", query);
        
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        return new GetProductByIdResult(product);
    }
}
