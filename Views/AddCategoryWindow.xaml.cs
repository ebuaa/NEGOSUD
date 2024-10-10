using System.Windows;
using Negosud.Models.Entities;
using Negosud.Services;

namespace Negosud.Views
{
    public partial class AddCategoryWindow : Window
    {
        private readonly CategoryService _categoryService;
        private readonly Category _categoryToEdit;

        public AddCategoryWindow(CategoryService categoryService)
        {
            InitializeComponent();
            _categoryService = categoryService;
        }

        public AddCategoryWindow(CategoryService categoryService, Category categoryToEdit)
        {
            InitializeComponent();
            _categoryService = categoryService;
            _categoryToEdit = categoryToEdit;
            txtCategoryName.Text = _categoryToEdit.Name;
        }

        private void btnSaveCategory_Click(object sender, RoutedEventArgs e)
        {
            if (_categoryToEdit == null)
            {
                var newCategory = new Category
                {
                    Name = txtCategoryName.Text,
                    Description = "Wines that are made out of Grapes"
                };
                _categoryService.AddCategory(newCategory);
                MessageBox.Show("Catégorie ajoutée avec succès !");
            }
            else
            {
                _categoryToEdit.Name = txtCategoryName.Text;
                _categoryService.UpdateCategory(_categoryToEdit);
                MessageBox.Show("Catégorie modifiée avec succès !");
            }

            this.Close(); 
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
