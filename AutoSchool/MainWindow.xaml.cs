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
using System.Data.Linq;
using System.Data;
using System.Data.Linq.Mapping;

namespace AutoSchool
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnRegistory_Click(object sender, RoutedEventArgs e)
        {
            Registration reg = new Registration();
            reg.Show();
            this.Close();
        }

        private void BtnEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string password = tbPassword.Text;
            string name = "";
            bool connected = false;

            //try
            //{
            DataContext dc = new DataContext(Properties.Settings.Default.connectionAS);
            Table<Classes.Users> users = dc.GetTable<Classes.Users>();
            foreach (var user in users)
            {
                if (user.login == tbLogin.Text)
                {
                    string role = user.role;
                    if (role == "Admin")
                    {
                        if (user.password == tbPassword.Text)
                        {                           
                            ViewWindow viewWindow = new ViewWindow();
                            viewWindow.Show();
                            this.Close();

                            connected = true;
                            name = user.fio;
                        }
                    }
                    else
                    {
                        if (user.password == tbPassword.Text)
                        {
                            ViewWindow viewWindow = new ViewWindow();
                            viewWindow.Show();
                            this.Close();

                            connected = true;
                            name = user.fio;
                        }
                    }
                }
            }


            if (connected)
            {
                MessageBox.Show($"Вы вошли как {name}");
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}
