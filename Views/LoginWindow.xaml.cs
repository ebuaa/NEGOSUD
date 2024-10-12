using System.Windows;

namespace Negosud.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string adminUsername = "admin";
            string adminPassword = "password123";

            if (txtUsername.Text == adminUsername && txtPassword.Password == adminPassword)
            {
                this.DialogResult = true;
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}