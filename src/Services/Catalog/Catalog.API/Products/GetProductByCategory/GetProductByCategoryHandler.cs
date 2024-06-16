
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler
    (IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public ILogger<GetProductByCategoryQueryHandler> Logger { get; } = logger;

    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        Logger.LogInformation("GetProductByCategoryQueryHandler.Called with {@query}", query);
        
        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);

        return new GetProductByCategoryResult(products);
    }
}
