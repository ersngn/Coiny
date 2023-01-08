using System.Net;
using AutoMapper;
using AutoMapper.Internal;
using Coiny.Common.Constants;
using Coiny.Common.Dtos.Base;
using Coiny.Common.Enumeration;
using Coiny.Services.Catalog.Dtos.Category;
using Coiny.Services.Catalog.Repositories.Category;
using MongoDB.Bson;

namespace Coiny.Services.Catalog.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<Response<List<CategoryDto>>> GetMainCategoriesAsync()
    {
        var categories = await _categoryRepository.GetListAsync(e => e.MainCategoryId == null);

        if (categories.Count <= 0)
        {
            return Response<List<CategoryDto>>.Fail(Error.CategoriesNotFound,
                ErrorConstants.CategoriesNotFound);
        }

        return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories));
    }

    public async Task<Response<List<CategoryDto>>> GetSubCategoriesByMainCategoryIdAsync(string mainCategoryId)
    {
        var categories = await _categoryRepository.GetListAsync(e => e.MainCategoryId == mainCategoryId);

        if (categories.Count <= 0)
        {
            return Response<List<CategoryDto>>.Fail(Error.CategoriesNotFound,
                ErrorConstants.CategoriesNotFound);
        }

        return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories));
    }

    public async Task<Response<CategoryDto>> CreateMainCategoryAsync(MainCategoryCreateDto createDto)
    {
        var response = new Response<CategoryDto>();

        try
        {
            var category = _mapper.Map<Models.Category.Category>(createDto);

            category.Id = ObjectId.GenerateNewId().ToString();
            category.MainCategoryId = null;
            category.IsActive = true;
            category.IsDeleted = false;

            var result = _categoryRepository.AddAsync(category);
            if (result.IsFaulted)
            {
                response=Response<CategoryDto>.Fail(Error.CategoryNotCreated, ErrorConstants.CategoryNotCreated);
                return response;
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);
            
            response=Response<CategoryDto>.Success(categoryDto);
        }
        catch (Exception e)
        {
            response = Response<CategoryDto>.Fail(e.Message);
        }

        return response;
    }

    public async Task<Response<CategoryDto>> UpdateMainCategoryAsync(MainCategoryUpdateDto updateDto)
    {
        var response = new Response<CategoryDto>();

        try
        {
            var category = await _categoryRepository.GetByIdAsync(updateDto.Id);

            if (category == null)
            {
                response=Response<CategoryDto>.Fail(Error.CategoryNotFound,ErrorConstants.CategoryNotFound);
                return response;
            }

            category = _mapper.Map<Models.Category.Category>(updateDto);

            var result = _categoryRepository.UpdateAsync(updateDto.Id, category);

            if (result.IsFaulted)
            {
                response = Response<CategoryDto>.Fail(Error.UpdateProcessFailed, ErrorConstants.UpdateProcessFailed);
                return response;
            }

            var categoryDto = _mapper.Map<CategoryDto>(result.Result);
            response = Response<CategoryDto>.Success(categoryDto);
        }
        catch (Exception e)
        {
            response = Response<CategoryDto>.Fail(e.Message);
        }

        return response;
    }

    public async Task<Response<CategoryDto>> CreateSubCategoryAsync(SubCategoryCreateDto createDto)
    {
        var response = new Response<CategoryDto>();

        try
        {
            if (createDto.MainCategoryId == null)
            {
                response = Response<CategoryDto>.Fail(Error.MainCategoryIdNull, ErrorConstants.MainCategoryIdNull);
                return response;
            }

            var mainCategory = _categoryRepository.GetByIdAsync(createDto.MainCategoryId);

            if (mainCategory.IsFaulted)
            {
                response = Response<CategoryDto>.Fail(Error.MainCategoryNotFound, ErrorConstants.MainCategoryNotFound);
                return response;
            }

            var category = _mapper.Map<Models.Category.Category>(createDto);

            category.Id = ObjectId.GenerateNewId().ToString();
            category.MainCategoryId = mainCategory.Result.Id;
            category.IsActive = true;
            category.IsDeleted = false;

            var result = _categoryRepository.AddAsync(category);
            if (result.IsFaulted)
            {
                response = Response<CategoryDto>.Fail(Error.CategoryNotCreated, ErrorConstants.CategoryNotFound);
                return response;
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);
            
            response=Response<CategoryDto>.Success(categoryDto);
        }
        catch (Exception e)
        {
            response = Response<CategoryDto>.Fail(e.Message);
        }

        return response;
    }

    public async Task<Response<CategoryDto>> UpdateSubCategoryAsync(SubCategoryUpdateDto updateDto)
    {
       var response = new Response<CategoryDto>();

        try
        {
            if (updateDto.MainCategoryId == null)
            {
                response = Response<CategoryDto>.Fail(Error.MainCategoryIdNull,ErrorConstants.MainCategoryIdNull);
                return response;
            }

            var mainCategory = _categoryRepository.GetByIdAsync(updateDto.MainCategoryId);

            if (mainCategory.IsFaulted)
            {
                response = Response<CategoryDto>.Fail(Error.MainCategoryNotFound, ErrorConstants.MainCategoryNotFound);
                return response;
            }

            var category = _mapper.Map<Models.Category.Category>(updateDto);
            
            category.Id = new ObjectId().ToString();
            category.MainCategoryId = mainCategory.Result.Id;
            category.IsActive = true;
            category.IsDeleted = false;

            var result = _categoryRepository.UpdateAsync(updateDto.Id,category);
            if (result.IsFaulted)
            {
                response=Response<CategoryDto>.Fail(Error.CategoryNotCreated,ErrorConstants.CategoryNotCreated);

                return response;
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);
            
            response = Response<CategoryDto>.Success(categoryDto);
        }
        catch (Exception e)
        {
            response = Response<CategoryDto>.Fail(e.Message);
        }

        return response;
    }

    public async Task<Response<CategoryDto>> GetByIdAsync(string id)
    {
        var category = await _categoryRepository.GetAsync(e => e.Id == id);
        
        if (category == null)
        {
            return Response<CategoryDto>.Fail(Error.CategoryNotFound, ErrorConstants.CategoryNotFound);
        }
        
        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category));
    }

    public async Task<Response<CategoryDto?>> DeleteAsync(string id)
    {
        Response<CategoryDto> response;

        try
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                response = Response<CategoryDto>.Fail(Error.CategoryNotFound, ErrorConstants.CategoryNotFound);
                return response;
            }
            if (string.IsNullOrWhiteSpace(category.MainCategoryId))
            {
                var result = _categoryRepository.DeleteAsync(category);

                if (result.IsFaulted)
                {
                    response = Response<CategoryDto>.Fail(Error.DeleteProcessFailed, ErrorConstants.DeleteProcessFailed);
                    return response;
                }
            }
            else
            {
                var mainCategory = await _categoryRepository.GetByIdAsync(category.MainCategoryId);
                if (mainCategory == null)
                {
                    response = Response<CategoryDto>.Fail(Error.MainCategoryNotFoundForThisCategory,
                        ErrorConstants.MainCategoryNotFoundForThisCategory);
                    return response;
                }

                var subCategoriesByMainCategory =
                    _categoryRepository.Get(e => e.MainCategoryId == mainCategory.Id).ToList();
                if (subCategoriesByMainCategory.Count>0)
                {
                    foreach (var subCategory in subCategoriesByMainCategory)
                    {
                        await _categoryRepository.DeleteAsync(subCategory);
                    }
                }
            }
            response = Response<CategoryDto>.Success(HttpStatusCode.OK);
        }
        catch (Exception e)
        {
            response = Response<CategoryDto>.Fail(e.Message);
        }

        return response;
    }

    public async Task<Response<List<CategoryTreeDto>>> GetCategoryTreesAsync()
    {
        Response<List<CategoryTreeDto>> response;
        var responseData = new List<CategoryTreeDto>();
        try
        {
            var mainCategories = await _categoryRepository.GetListAsync(e => e.IsActive && e.MainCategoryId == null);

            if  (mainCategories.Count<=0)
            {
                response = Response<List<CategoryTreeDto>>.Fail(Error.MainCategoriesNotFound,
                    ErrorConstants.MainCategoriesNotFound);

                return response;
            }

            foreach (var mainCategory in mainCategories)
            {
                var subCategories =
                    await _categoryRepository.GetListAsync(e => e.IsActive && e.MainCategoryId == mainCategory.Id); 
                var categoryTreeDto = _mapper.Map<CategoryTreeDto>(mainCategory);
                if (subCategories.Count > 0)
                    categoryTreeDto.SubCategories = new List<CategoryDto>(_mapper.Map<List<CategoryDto>>(subCategories));
                
                responseData.Add(categoryTreeDto);
                
            }
            response = Response<List<CategoryTreeDto>>.Success(responseData);
        }
        catch (Exception e)
        {
            response = Response<List<CategoryTreeDto>>.Fail(e.Message);
            return response;
        }

        return response;
    }
}