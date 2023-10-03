namespace webserver0.Products;

public class ProductService : IProductService
{
  private readonly ProductGrpc.ProductGrpcClient _productGrpcClient;

  public ProductService(ProductGrpc.ProductGrpcClient productGrpcClient)
  {
    _productGrpcClient = productGrpcClient;
  }

  public async Task<List<string>> GetProductListAsync()
  {
    var getProductListRequest = new GetProductListRequest();
    getProductListRequest.Ids.AddRange(new List<string> { "Grpc", "request", "from", "webserver0" });

    var getProductListResponse = await _productGrpcClient.GetProductListAsync(getProductListRequest);

    var result = new List<string> { "From", "webserver0" };
    result.AddRange(getProductListResponse.Items.Select(i => i.Name));

    return result;
  }
}
