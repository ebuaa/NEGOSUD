using NEGOSUDAPI.Models.Entities;

namespace NEGOSUDAPI.Services.SuppliersServices
{
    public interface ISuppliersService
    {
        Task<IEnumerable<Supplier>> GetAllSuppliersAsync();
        Task<Supplier?> GetSupplierByIdAsync(int supplierId);
        Task AddSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(int supplierId);
    }
}
