using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.Generic;
using WpfApp1.Services;
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        private readonly UserService _userService;

        public AdminPanel(MainWindow mainWindow)
        {
            InitializeComponent();
            _userService = new UserService();
            LoadUsers();
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.Topmost = true;
        }

        private void LoadUsers()
        {
            var users = _userService.GetAllUsers();
            UsersGrid.ItemsSource = users;
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var user = new User
            {
                Username = UsernameTextBox.Text,
                Password = PasswordBox.Password,
                Role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString()
            };

            if (_userService.CreateUser(user))
            {
                MessageBox.Show("User added successfully!");
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Failed to add user.");
            }
        }

        private void UpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem is User selectedUser)
            {
                selectedUser.Password = PasswordBox.Password;
                selectedUser.Role = (RoleComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                if (_userService.UpdateUser(selectedUser))
                {
                    MessageBox.Show("User updated successfully!");
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Failed to update user.");
                }
            }
            else
            {
                MessageBox.Show("Please select a user to update.");
            }
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem is User selectedUser)
            {
                if (_userService.DeleteUser(selectedUser.Id))
                {
                    MessageBox.Show("User deleted successfully!");
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Failed to delete user.");
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}