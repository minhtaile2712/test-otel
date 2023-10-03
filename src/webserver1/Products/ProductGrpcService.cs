using Grpc.Core;

namespace webserver1.Products;

public class ProductGrpcService : ProductGrpc.ProductGrpcBase
{
  public ProductGrpcService()
  {

  }

  public override Task<GetProductListResponse> GetProductList(GetProductListRequest request, ServerCallContext context)
  {
    Console.WriteLine(request.Ids);

    var productList = new List<Product> {
      new() { Id = "one", Name = "From", Price = 1000 },
      new() { Id = "two", Name = "webserver1", Price = 2000 }
    };
    var result = new GetProductListResponse { };
    result.Items.AddRange(productList);
    return Task.FromResult(result);
  }
}