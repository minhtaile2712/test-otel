namespace webserver0.Products;

public interface IProductService
{
  public Task<List<string>> GetProductListAsync();
}
