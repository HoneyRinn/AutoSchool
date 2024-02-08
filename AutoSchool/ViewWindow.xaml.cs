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
    /// Логика взаимодействия для ViewWindow.xaml
    /// </summary>
    public partial class ViewWindow : Window
    {
        public ViewWindow()
        {
            InitializeComponent();
        }
        //Меню перехода между формами
        private void BtnStud_Click(object sender, RoutedEventArgs e)
        {
            StudentsWindow studentsWindow = new StudentsWindow();
            studentsWindow.Show();
            this.Close();
        }

        private void BtnInstructors_Click(object sender, RoutedEventArgs e)
        {
            InstructorsWidow instructorsWidow = new InstructorsWidow();
            instructorsWidow.Show();
            this.Close();
        }
    }
}
