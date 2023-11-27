using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VarikArtem.DBRequests;
using VarikArtem.Models;

namespace VarikArtem
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        UserDb userDb = new UserDb();
        public RegWindow()
        {
            
            InitializeComponent();
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            char[] specialSymbols = { '!', '#', '$', '%', '&', '(', ')', '*', ',', '+', '-' };
            string str = new String(specialSymbols);
            string loginText = LoginTb.Text;
            string passwordText = PasswordTb.Password;
            User user = userDb.GetUsers().FirstOrDefault(u => u.Login == loginText);
            if (user == null)
            {
                InfoLabel.Content = "Вы уже зарегистрированы в системе";
                return;
            }
            else if (passwordText.Length >= 6 & Regex.IsMatch(passwordText, @"[0-9]") & 
                    passwordText.Contains(str) & Regex.IsMatch(passwordText, @"[A-Z]") & 
                    Regex.IsMatch(passwordText, @"[a-z]"))
            {
                User userr = new User
                {
                    Id = user.Id,
                    Login = loginText,
                    Password = passwordText,
                    IdProgrammer = user.IdProgrammer,
                };
                userDb.AddUser(userr);
            }
           else InfoLabel.Content = "Пароль должен содержать не менее 6 символов,\n" +
                                "строчные и заглавные буквы, минимум 1 спецсимвол и цифру";
            userDb.AddUser(user);
        }
        public void SignUp()
        {
            ContentWindow contentWindow = new ContentWindow();
            contentWindow.Show();
            this.Close();
        }
    }
}
