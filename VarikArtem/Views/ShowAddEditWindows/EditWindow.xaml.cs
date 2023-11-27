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
using VarikArtem.DBRequests;
using VarikArtem.Models;

namespace VarikArtem.Views.ShowAddEditWindows
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        Programmer programmer1 = new Programmer();
        public List<Company> spisokCmp;
        public List<Level> levelCmp;

        public EditWindow(Programmer programmer)
        {
            InitializeComponent();
            programmer1 = programmer;
            spisokCmp = CompaniesDB.GetCompanies();
            levelCmp = LevelsDB.GetLevels();
            LoadingDataToControls();
            ProgerCompanyCb.ItemsSource = spisokCmp;
            ProgerLevelCb.ItemsSource = levelCmp;
        }
        private void LoadingDataToControls()
        {
            ProgerNameTbx.Text = programmer1.Name;
            ProgerAgeTbx.Text = Convert.ToString(programmer1.Age);
            ProgerExpTbx.Text = Convert.ToString(programmer1.Experience);
            ProgerCompanyCb.SelectedItem = spisokCmp.FirstOrDefault(item => item.Id == programmer1.IdCompany);
            ProgerLevelCb.SelectedItem = levelCmp.FirstOrDefault(item => item.Id == programmer1.IdLevel);
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            //изменить запись внутри базы данных,
            //изменить запись внутри коллекции(в ContentWindow как мы делали в AddProgerWindow)
            Company company = ProgerCompanyCb.SelectedItem as Company;
            Level level1 = ProgerLevelCb.SelectedItem as Level;
            if (company == null || level1 == null)
            {
                return;
            }
            string nameText = ProgerNameTbx.Text;
            string ageText = ProgerAgeTbx.Text;
            int age = Convert.ToInt32(ageText);
            string experienceText = ProgerExpTbx.Text;
            int experience = Convert.ToInt32(experienceText);
            int levelId = level1.Id;
            int companyId = company.Id;
            Programmer programmer = new Programmer(programmer1.Id, nameText, age, experience, levelId, companyId);
            ProgersDb.EditProgrammer(programmer);
            this.Close();
        }
    }
}
