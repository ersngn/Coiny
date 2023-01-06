using Coiny.Common.Dtos.Base;
using Coiny.Services.Catalog.Dtos.Category;

namespace Coiny.Services.Catalog.Services.Category;

public interface ICategoryService
{
    Task<Response<List<CategoryDto>>> GetMainCategoriesAsync();
    Task<Response<List<CategoryDto>>> GetSubCategoriesByMainCategoryIdAsync(string mainCategoryId);
    Task<Response<CategoryDto>> CreateMainCategoryAsync(MainCategoryCreateDto createDto);
    Task<Response<CategoryDto>> UpdateMainCategoryAsync(MainCategoryUpdateDto updateDto);
    Task<Response<CategoryDto>> CreateSubCategoryAsync(SubCategoryCreateDto createDto);
    Task<Response<CategoryDto>> UpdateSubCategoryAsync(SubCategoryUpdateDto updateDto);
    Task<Response<CategoryDto>> GetByIdAsync(string id);
    Task<Response<CategoryDto?>> DeleteAsync(string id);
    Task<Response<List<CategoryTreeDto>>> GetCategoryTreesAsync();


}