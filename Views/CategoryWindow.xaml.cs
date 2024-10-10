using Negosud.Models.Entities;
using Negosud.Services;
using System.Windows;

namespace Negosud.Views
{
    public partial class CategoryWindow : Window
    {
        private CategoryService _categoryService;

        public CategoryWindow(CategoryService categoryService)
        {
            InitializeComponent();
            _categoryService = categoryService;
            LoadCategories();
        }

        private void LoadCategories()
        {
            lvCategories.ItemsSource = _categoryService.GetAllCategories();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Category added successfully!");
            LoadCategories();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvCategories.SelectedItem is Category selectedCategory)
            {
                _categoryService.DeleteCategory(selectedCategory.CategoryID);
                MessageBox.Show("Category deleted successfully!");
                LoadCategories();
            }
            else
            {
                MessageBox.Show("Please select a category to delete.");
            }
        }
    }
}
