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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VarikArtem.DBRequests;
using VarikArtem.Models;

namespace VarikArtem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserDb userDb = new UserDb();
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            string loginText = LoginTb.Text;
            string passwordText = PasswordTb.Password;
            InfoLabel.Content = ("Ваши данные: " + loginText + passwordText);
            User user = userDb.GetUsers().FirstOrDefault(u => u.Login == loginText);
            if (user == null)
            {
                InfoLabel.Content = "Такого пользователя нет";
                return;
            }
            if(user.Password!= passwordText)
            {
                InfoLabel.Content = "Пароль введен неверно";
                return;
            }
            InfoLabel.Content = "Вы успешно вошли";
            SignIn();
        }
        public void SignIn()
        {
            ContentWindow contentWindow = new ContentWindow();
            contentWindow.Show();
            this.Close();
        }

        private void IfNotReg_Click(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new RegWindow();
            regWindow.Show();
            this.Close();
        }
    }
}
