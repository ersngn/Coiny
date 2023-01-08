using Coiny.Common.Dtos.Base;
using Coiny.Services.Catalog.Dtos.Category;
using Coiny.Services.Catalog.Services.Category;
using Microsoft.AspNetCore.Mvc;

namespace Coiny.Services.Catalog.Controllers;

public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("main")]
    public async Task<Response<List<CategoryDto>>> GetMainCategories()
    {
        var categories = await _categoryService.GetMainCategoriesAsync();
        return categories;
    }

    [HttpGet("main/{mainCategoryId}/sub-categories")]
    public async Task<Response<List<CategoryDto>>> GetSubCategoriesByMainCategoryId([FromRoute] string mainCategoryId)
    {
        var subCategories = await _categoryService.GetSubCategoriesByMainCategoryIdAsync(mainCategoryId);
        return subCategories;
    }

    [HttpPost("main")]
    public async Task<Response<CategoryDto>> CreateMainCategory([FromBody] MainCategoryCreateDto request)
    {
        var createdCategory = await _categoryService.CreateMainCategoryAsync(request);
        return createdCategory;
    }

    [HttpPut("main")]
    public async Task<Response<CategoryDto>> UpdatedMainCategory([FromBody] MainCategoryUpdateDto request)
    {
        var updatedCategory = await _categoryService.UpdateMainCategoryAsync(request);
        return updatedCategory;
    }
    
    [HttpPost("sub")]
    public async Task<Response<CategoryDto>> CreateSubCategory([FromBody] SubCategoryCreateDto request)
    {
        var createdCategory = await _categoryService.CreateSubCategoryAsync(request);
        return createdCategory;
    }

    [HttpPut("sub")]
    public async Task<Response<CategoryDto>> UpdatedMainCategory([FromBody] SubCategoryUpdateDto request)
    {
        var updatedCategory = await _categoryService.UpdateSubCategoryAsync(request);
        return updatedCategory;
    }

    [HttpGet("{id}")]
    public async Task<Response<CategoryDto>> GetById([FromRoute] string id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        return category;
    }
    
    [HttpDelete("{id}")]
    public async Task<Response<CategoryDto?>> Delete([FromRoute] string id)
    {
        var category = await _categoryService.DeleteAsync(id);
        return category;
    }

    [HttpGet("tree")]
    public async Task<Response<List<CategoryTreeDto>>> GetCategoryTree()
    {
        var categoryTree = await _categoryService.GetCategoryTreesAsync();
        return categoryTree;
    }

}