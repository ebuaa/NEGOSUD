using NEGOSUDAPI.Models.Entities;

namespace NEGOSUDAPI.Services.CategoriesServices
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int categoryId);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int categoryId);
    }
}
