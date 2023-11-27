using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using VarikArtem.DBRequests;
using VarikArtem.Models;

namespace VarikArtem
{
    /// <summary>
    /// Логика взаимодействия для AddProgrammerWindow.xaml
    /// </summary>
    public partial class AddProgrammerWindow : Window
    {
        ObservableCollection<Programmer> programmers1 = new ObservableCollection<Programmer>();
        public AddProgrammerWindow(ObservableCollection<Programmer> programmers)
        {
            InitializeComponent();
            programmers1 = programmers;
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string nameText=ProgerNameTbx.Text;
            string ageText = ProgerAgeTbx.Text;
            int age = Convert.ToInt32(ageText);
            string experienceText = ProgerExpTbx.Text;
            int experience = Convert.ToInt32(experienceText);
            string levelText = ProgerLevelCb.Text;
            string companyText = ProgerCompanyCb.Text;
            if (nameText != null && ageText != null && levelText != null && companyText != null)
            {
                if (age > 0 & age < 100 & experience < age)
                {
                    Programmer programmer = new Programmer();
                    programmer.Name = nameText;
                    programmer.Age = age;
                    programmer.Experience = experience;
                    programmer.IdLevel = Convert.ToInt32(levelText);
                    programmer.IdCompany = Convert.ToInt32(companyText);
                    ProgersDb.AddProgrammers(programmer);
                    programmers1.Add(programmer);
                    this.Close();
                    MessageBox.Show("Пользователь успешно добавлен");
                }
                else InfoLabel.Content = "Отредактируйте возраст(>0 и <100) или стаж";
            }
            else InfoLabel.Content = "Заполните пустые поля";
        }
    }
}
