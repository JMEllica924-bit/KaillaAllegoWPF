using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Kailla
{
    public partial class MainWindow : Window
    {
        // ARRAYS (fixed size)
        string[] fullnames = new string[100];
        string[] ages = new string[100];
        string[] genders = new string[100];
        string[] puroks = new string[100];

        int currentIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        // SAVE BUTTON
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string fullname = txtFullname.Text;
            string age = txtAge.Text;
            string gender = (comboGender.SelectedItem as ComboBoxItem)?.Content.ToString();
            string purok = GetSelectedPurok();

            if (string.IsNullOrWhiteSpace(fullname) ||
                string.IsNullOrWhiteSpace(age) ||
                gender == null ||
                purok == "")
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            if (currentIndex < fullnames.Length)
            {
                fullnames[currentIndex] = fullname;
                ages[currentIndex] = age;
                genders[currentIndex] = gender;
                puroks[currentIndex] = purok;

                currentIndex++;
            }
            else
            {
                MessageBox.Show("Array is full.");
            }

            RefreshGrid();
            ClearFields();
        }

        // DELETE BUTTON
        private void btnDeleteData_Click(object sender, RoutedEventArgs e)
        {
            int index = dataGrid.SelectedIndex;

            if (index >= 0 && index < currentIndex)
            {
                for (int i = index; i < currentIndex - 1; i++)
                {
                    fullnames[i] = fullnames[i + 1];
                    ages[i] = ages[i + 1];
                    genders[i] = genders[i + 1];
                    puroks[i] = puroks[i + 1];
                }

                currentIndex--;

                RefreshGrid();
                btnDeleteData.IsEnabled = false;
            }
        }

        // SELECT ROW
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedIndex >= 0)
            {
                btnDeleteData.IsEnabled = true;
            }
        }

        // REFRESH DATAGRID
        private void RefreshGrid()
        {
            var list = new List<object>();

            for (int i = 0; i < currentIndex; i++)
            {
                list.Add(new
                {
                    Fullname = fullnames[i],
                    Age = ages[i],
                    Gender = genders[i],
                    Purok = puroks[i]
                });
            }

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = list;
        }

        // GET PUROK
        private string GetSelectedPurok()
        {
            if (rbOne.IsChecked == true) return "1";
            if (rbTwo.IsChecked == true) return "2";
            if (rbThree.IsChecked == true) return "3";
            if (rbFour.IsChecked == true) return "4";
            if (rbFive.IsChecked == true) return "5";
            return "";
        }

        // CLEAR INPUTS
        private void ClearFields()
        {
            txtFullname.Text = "";
            txtAge.Text = "";
            comboGender.SelectedIndex = -1;

            rbOne.IsChecked = false;
            rbTwo.IsChecked = false;
            rbThree.IsChecked = false;
            rbFour.IsChecked = false;
            rbFive.IsChecked = false;
        }
    }
}