using AutoMapper;
using Coiny.Services.Catalog.Repositories.Product;

namespace Coiny.Services.Catalog.Services.Product;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    
}