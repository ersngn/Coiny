using AutoMapper;
using Coiny.Services.Catalog.Dtos.Category;
using Coiny.Services.Catalog.Dtos.Product;
using Coiny.Services.Catalog.Models.Category;
using Coiny.Services.Catalog.Models.Product;

namespace Coiny.Services.Catalog.Common.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        #region Create Mapping

        #region Product
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, ProductCreateDto>().ReverseMap();
        CreateMap<Product, ProductUpdateDto>().ReverseMap();
        #endregion

        #region Category
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, SubCategoryCreateDto>().ReverseMap();
        CreateMap<Category, SubCategoryUpdateDto>().ReverseMap();
        CreateMap<Category, MainCategoryCreateDto>().ReverseMap();
        CreateMap<Category, MainCategoryUpdateDto>().ReverseMap();
        #endregion


        #endregion
    }
}