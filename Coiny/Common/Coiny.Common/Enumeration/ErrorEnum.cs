namespace Coiny.Common.Enumeration;

public enum Error
{
    #region Catalog

    UnknownError = 1000,
    
    #region Catalog.Category
    
    CategoriesNotFound     = 10001,
    CategoryNotFound       = 1002,
    DeleteProcessFailed    = 1003,
    CategoryNotCreated     = 1004,
    UpdateProcessFailed    = 1005,
    MainCategoryIdNull      = 1006,
    MainCategoryNotFound    = 1007,
    

    #endregion
    

    #endregion
}