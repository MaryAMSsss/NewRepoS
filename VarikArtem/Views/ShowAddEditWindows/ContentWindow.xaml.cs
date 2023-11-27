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
using VarikArtem.Views.ShowAddEditWindows;

namespace VarikArtem
{
    /// <summary>
    /// Логика взаимодействия для ContentWindow.xaml
    /// </summary>
    public partial class ContentWindow : Window
    {
        public int TotalRecordsOnPage = 5;
        public int CurrentPage = 0;
        public int MaxPageCount
        {
            get { return proger.Count / TotalRecordsOnPage; }
        }
        public List<Company> spisokCmp;
        public List<Level> levelCmp;

        ObservableCollection<Programmer> proger = new ObservableCollection<Programmer>();

        ObservableCollection<Programmer> progersOnPage = new ObservableCollection<Programmer>();
        public ContentWindow()
        {
            InitializeComponent();
            ProgersListBox.ItemsSource = progersOnPage;
            LoadingData();
            UpdatePages();
        }
        private void UpdatePages()
        {
            progersOnPage.Clear();
            for(int i = CurrentPage * TotalRecordsOnPage; i < CurrentPage * TotalRecordsOnPage + TotalRecordsOnPage; i++)
            {
                if(i >= proger.Count())
                {
                    break;
                }
                progersOnPage.Add(proger[i]);
            }
            UpdateLabels();
        }
        private void UpdateLabels() 
        {
            maxPage.Content = MaxPageCount;
            currentPage.Content = CurrentPage;
            currentPage.Foreground = Brushes.Red;
            currentPage.FontWeight = FontWeights.Bold;
            if (CurrentPage - 1 < 0)
            {
                lastPage.Content = ' ';
                nextPage.Content = CurrentPage + 1;
            }
            else if (CurrentPage + 1 > MaxPageCount)
            {
                nextPage.Content = ' ';
                lastPage.Content = CurrentPage - 1;
            }
            else
            {
                nextPage.Content = CurrentPage + 1;
                lastPage.Content = CurrentPage - 1;
            }
        }
        private void GoToPrevPage_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentPage == 0)
            {
                return;
            }
            CurrentPage -= 1;
            UpdatePages();
        }
        private void GoToNextPage_Click(object sender, RoutedEventArgs e)
        {
            if(CurrentPage == MaxPageCount)
            {
                return;
            }
            CurrentPage += 1;
            UpdatePages();
        }

        private void LoadingData()
        {
            foreach(Programmer programmer in ProgersDb.GetProgrammers())
            {
                proger.Add(programmer);
            }
            UpdatePages();
            //ProgersListBox.ItemsSource = proger;
            spisokCmp = CompaniesDB.GetCompanies();
            levelCmp = LevelsDB.GetLevels();
        }
        private void BackToLoginBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddProgrammerWindow addProgrammerWindow = new AddProgrammerWindow(proger);
            addProgrammerWindow.Show();
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Programmer selectedProger = ProgersListBox.SelectedItem as Programmer;

            if (selectedProger == null)
                MessageBox.Show("Вы ничего не выбрали");
                //return;
            else
            {
                RemoveProgrammer(selectedProger);
            }
        }
        private void RemoveProgrammer(Programmer selectedProger)
        {
            proger.Remove(selectedProger);
            ProgersDb.RemoveProgrammer(selectedProger);
            UpdatePages();
        }
        private void UpdateProgerList(List<Programmer> newListProger)
        {
            proger.Clear();
            foreach (Programmer programmer in newListProger)
            {
                proger.Add(programmer);
            }
        }
        private void ProgerSort_Combobox(object sender, SelectionChangedEventArgs e)
        {
            string selectedItem = SortProgersCombobox.SelectedItem as string;
            if(selectedItem == null)
            {
                return;
            }
            if(selectedItem == "По имени")
            {
                List<Programmer> newListProger = proger.OrderBy(item => item.Name).ToList();
                UpdateProgerList(newListProger);
            }
            if (selectedItem == "По возрасту")
            {
                List<Programmer> newListProger = proger.OrderBy(item => item.Age).ToList();
                UpdateProgerList(newListProger);
            }
        }
        private void FiltrTextChanged(object sender, TextChangedEventArgs e)
        {
            ProgerFiltr();
        }
        private void ProgerFiltr_Combobox(object sender, SelectionChangedEventArgs e)
        {
            ProgerFiltr();
        }
        private void ProgerFiltr()
        {
            string selectedItem = FiltrProgersCombobox.SelectedItem as string;
            string selectFiltrTbx = FiltrTbx.Text;
            if (selectedItem == null)
            {
                return;
            }
            if (selectedItem == "По имени")
            {  
                List<Programmer> newListProger = proger.Where(item => item.Name == selectFiltrTbx).ToList();
                UpdateProgerList(newListProger);
            }
            if (selectedItem == "По возрасту" & int.TryParse(selectFiltrTbx, out _))
            {
                List<Programmer> newListProger = ProgersDb.GetProgrammers().Where(item => item.Age == Int32.Parse(selectFiltrTbx)).ToList();
                UpdateProgerList(newListProger);
            }
            if (selectedItem == "Все типы")
            {
                UpdateProgerList(ProgersDb.GetProgrammers());
            }
        }
        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string enteredText = SearchTbx.Text.ToLower();
            if (enteredText == null)
            {
                return;
            }
            List<Programmer> newListProgrammer = ProgersDb.GetProgrammers().Where(item => item.Name.ToLower().Contains(enteredText)).ToList();
            UpdateProgerList(newListProgrammer);
        }
        private void ProgersListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditWindow editWindow = new EditWindow(ProgersListBox.SelectedItem as Programmer);
            editWindow.Show();
        }
        
    }
}
