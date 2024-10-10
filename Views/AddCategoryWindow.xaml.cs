using System.Windows;
using System.Windows.Controls;
using Negosud.Models.Entities;
using Negosud.Services;

namespace Negosud.Views
{
    public partial class AddCategoryWindow : Window
    {
        private readonly CategoryService _categoryService;

        public AddCategoryWindow(CategoryService categoryService)
        {
            InitializeComponent();
            _categoryService = categoryService;
            LoadCategories();
        }

        private void LoadCategories()
        {
            var categories = _categoryService.GetAllCategories();
            lstCategories.ItemsSource = categories;
        }

        private void btnAddCategory_Click(object sender, RoutedEventArgs e)
        {
            var newCategory = new Category
            {
                Name = txtCategoryName.Text
            };

            _categoryService.AddCategory(newCategory);
            LoadCategories();
            MessageBox.Show("Catégorie ajoutée avec succès !");
        }

        private void btnDeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (lstCategories.SelectedItem is Category selectedCategory)
            {
                _categoryService.DeleteCategory(selectedCategory.CategoryID); 
                LoadCategories();
                MessageBox.Show("Catégorie supprimée avec succès !");
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une catégorie à supprimer.");
            }
        }

        private void btnUpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            if (lstCategories.SelectedItem is Category selectedCategory)
            {
                selectedCategory.Name = txtCategoryName.Text;
                _categoryService.UpdateCategory(selectedCategory);
                LoadCategories();
                MessageBox.Show("Catégorie mise à jour avec succès !");
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une catégorie à modifier.");
            }
        }

        private void lstCategories_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lstCategories.SelectedItem is Category selectedCategory)
            {
                txtCategoryName.Text = selectedCategory.Name;
            }
        }
    }
}
