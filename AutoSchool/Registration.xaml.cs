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
using System.Data.Linq;
using System.Data;
using System.Data.Linq.Mapping;

namespace AutoSchool
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataContext dc = new DataContext(Properties.Settings.Default.connectionAS);

                if (TxtBxfio.Text != "" && TxtBxfio.Text != "" && TxtBxPassReg.Text != "")
                {
                    Classes.Users newuser = new Classes.Users {fio = TxtBxfio.Text, login = TxtBxLog.Text, password = TxtBxPassReg.Text, role = "Secretary", status = true };

                    dc.GetTable<Classes.Users>().InsertOnSubmit(newuser);
                    dc.SubmitChanges();
                }

                MessageBox.Show("Пользователь добавлен");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Неверные данные");
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
