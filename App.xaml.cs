using System.Windows;
using Negosud.Data;
using Negosud.Services;

namespace Negosud
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var context = new NegosudContext();

            var categoryService = new CategoryService(context);
            var supplierService = new SupplierService(context);
            var productService = new ProductService(context, categoryService, supplierService);
            var customerService = new CustomerService(context);
            var orderService = new OrderService(context);

            var mainWindow = new Views.MainWindow(productService, categoryService, supplierService, customerService, orderService);
            
        }
    }
}
