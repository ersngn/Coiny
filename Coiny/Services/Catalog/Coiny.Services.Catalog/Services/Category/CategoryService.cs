using System.Net;
using AutoMapper;
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
            return Response<List<CategoryDto>>.Fail(new ErrorDto()
            {
                Error = Error.CategoriesNotFound, ErrorCode = (int)Error.CategoriesNotFound,
                Message = ErrorConstants.CategoriesNotFound
            });
        }

        return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories));
    }

    public async Task<Response<List<CategoryDto>>> GetSubCategoriesByMainCategoryIdAsync(string mainCategoryId)
    {
        var categories = await _categoryRepository.GetListAsync(e => e.MainCategoryId == mainCategoryId);

        if (categories.Count <= 0)
        {
            return Response<List<CategoryDto>>.Fail(new ErrorDto()
            {
                Error = Error.CategoriesNotFound, ErrorCode = (int)Error.CategoriesNotFound,
                Message = ErrorConstants.CategoriesNotFound
            });
        }

        return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories));
    }

    public async Task<Response<CategoryDto>> CreateMainCategoryAsync(MainCategoryCreateDto createDto)
    {
        var response = new Response<CategoryDto>();

        try
        {
            var category = _mapper.Map<Models.Category.Category>(createDto);
            
            category.Id = new ObjectId().ToString();
            category.MainCategoryId = null;
            category.IsActive = true;
            category.IsDeleted = false;

            var result = _categoryRepository.AddAsync(category);
            if (!result.IsCompleted)
            {
                response=Response<CategoryDto>.Fail(new ErrorDto
                {
                    Error = Error.CategoryNotCreated, 
                    ErrorCode = (int)Error.CategoryNotCreated,
                    Message = ErrorConstants.CategoryNotCreated
                });

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

            if (category==null)
            {
                response=Response<CategoryDto>.Fail(new ErrorDto
                {
                    Error = Error.CategoryNotFound, 
                    ErrorCode = (int)Error.CategoryNotFound,
                    Message = ErrorConstants.CategoryNotFound
                });

                return response;
            }

            category = _mapper.Map<Models.Category.Category>(updateDto);

            var result = _categoryRepository.UpdateAsync(updateDto.Id,category);

            if (!result.IsCompleted)
            {
                response=Response<CategoryDto>.Fail(new ErrorDto
                {
                    Error = Error.DeleteProcessFailed, 
                    ErrorCode = (int)Error.DeleteProcessFailed,
                    Message = ErrorConstants.DeleteProcessFailed
                });

                return response;
            }
            
            response = Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(result.Result));
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
                response = Response<CategoryDto>.Fail(new ErrorDto()
                {
                    Error = Error.MainCategoryIdNull,
                    ErrorCode = (int)Error.MainCategoryIdNull,
                    Message = ErrorConstants.MainCategoryIdNull
                });

                return response;
            }

            var mainCategory = _categoryRepository.GetByIdAsync(createDto.MainCategoryId);

            if (!mainCategory.IsCompletedSuccessfully)
            {
                response = Response<CategoryDto>.Fail(new ErrorDto()
                {
                    Error = Error.MainCategoryNotFound,
                    ErrorCode = (int)Error.MainCategoryNotFound,
                    Message = ErrorConstants.MainCategoryNotFound
                });

                return response;
            }

            var category = _mapper.Map<Models.Category.Category>(createDto);
            
            category.Id = new ObjectId().ToString();
            category.MainCategoryId = mainCategory.Result.Id;
            category.IsActive = true;
            category.IsDeleted = false;

            var result = _categoryRepository.AddAsync(category);
            if (!result.IsCompleted)
            {
                response=Response<CategoryDto>.Fail(new ErrorDto
                {
                    Error = Error.CategoryNotCreated, 
                    ErrorCode = (int)Error.CategoryNotCreated,
                    Message = ErrorConstants.CategoryNotCreated
                });

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
                response = Response<CategoryDto>.Fail(new ErrorDto()
                {
                    Error = Error.MainCategoryIdNull,
                    ErrorCode = (int)Error.MainCategoryIdNull,
                    Message = ErrorConstants.MainCategoryIdNull
                });

                return response;
            }

            var mainCategory = _categoryRepository.GetByIdAsync(updateDto.MainCategoryId);

            if (!mainCategory.IsCompletedSuccessfully)
            {
                response = Response<CategoryDto>.Fail(new ErrorDto()
                {
                    Error = Error.MainCategoryNotFound,
                    ErrorCode = (int)Error.MainCategoryNotFound,
                    Message = ErrorConstants.MainCategoryNotFound
                });

                return response;
            }

            var category = _mapper.Map<Models.Category.Category>(updateDto);
            
            category.Id = new ObjectId().ToString();
            category.MainCategoryId = mainCategory.Result.Id;
            category.IsActive = true;
            category.IsDeleted = false;

            var result = _categoryRepository.UpdateAsync(updateDto.Id,category);
            if (!result.IsCompleted)
            {
                response=Response<CategoryDto>.Fail(new ErrorDto
                {
                    Error = Error.CategoryNotCreated, 
                    ErrorCode = (int)Error.CategoryNotCreated,
                    Message = ErrorConstants.CategoryNotCreated
                });

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
            return Response<CategoryDto>.Fail(new ErrorDto()
            {
                Error = Error.CategoryNotFound, 
                ErrorCode = (int)Error.CategoryNotFound,
                Message = ErrorConstants.CategoryNotFound
            });
        }
        
        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category));
    }

    public async Task<Response<CategoryDto?>> DeleteAsync(string id)
    {
        var response = new Response<CategoryDto>();

        try
        {
            var category = await _categoryRepository.GetByIdAsync(id);

            if (category==null)
            {
                response=Response<CategoryDto>.Fail(new ErrorDto
                {
                    Error = Error.CategoryNotFound, 
                    ErrorCode = (int)Error.CategoryNotFound,
                    Message = ErrorConstants.CategoryNotFound
                });

                return response;
            }

            var result = _categoryRepository.DeleteAsync(category);

            if (!result.IsCompleted)
            {
                response=Response<CategoryDto>.Fail(new ErrorDto
                {
                    Error = Error.DeleteProcessFailed, 
                    ErrorCode = (int)Error.DeleteProcessFailed,
                    Message = ErrorConstants.DeleteProcessFailed
                });

                return response;
            }
            
            response=Response<CategoryDto>.Success(HttpStatusCode.OK);
        }
        catch (Exception e)
        {
            response = Response<CategoryDto>.Fail(e.Message);
        }

        return response;


    }
}