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
    /// Логика взаимодействия для StudentsWindow.xaml
    /// </summary>
    public partial class StudentsWindow : Window
    {
        public StudentsWindow()
        {
            InitializeComponent();
            updateGrid();
        }
        //Подключение таблицы из БД
        static DataContext db = new DataContext(Properties.Settings.Default.connectionAS);
        Table<Classes.Students> students = db.GetTable<Classes.Students>();
        public bool trigger = false;

        private void updateGrid()// Обновление таблицы курсантов
        {
            var stud = students.Where(x => x.status == true);
            dgStudents.ItemsSource = stud;
        }
        
        private void BtnSearch_Click(object sender, RoutedEventArgs e)//Поиск по фамилии
        {
            try
            {
                DataContext db = new DataContext(Properties.Settings.Default.connectionAS);
                Table<Classes.Students> st = db.GetTable<Classes.Students>();
                string[] fams = (from fam in st//заполняет массив фамилиями
                                 select fam.fio).ToArray();
                string find = tbSearch.Text;
                int num = 0;
                if (tbSearch.Text != "")
                {
                    foreach (string s in fams)
                    {

                        string z = s;
                        string[] x = z.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        string c = x[0];
                        if (find != c)
                        {
                            num++;
                        }
                        if (find == c)
                        {
                            var find1 = (from f1 in st
                                         where f1.fio.StartsWith(find)
                                         select f1);

                            dgStudents.ItemsSource = find1;
                            num = 0;
                            break;
                        }
                        tbSearch.Text = "";

                    }
                    if (dgStudents.ItemsSource == null || num != 0)
                    {
                        MessageBox.Show("Курсант не найден");
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show($"Введите фамилию курсанта");
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)//Выход
        {
            ViewWindow viewWindow = new ViewWindow();
            viewWindow.Show();
            this.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)//Кнопка "Добавить"
        {
            lblSearch.Visibility = Visibility.Collapsed;
            tbSearch.Visibility = Visibility.Collapsed;
            btnSearch.Visibility = Visibility.Collapsed;

            lblCategory.Visibility = Visibility.Visible;
            lblFio.Visibility = Visibility.Visible;
            lblPassport.Visibility = Visibility.Visible;
            tbFio.Visibility = Visibility.Visible;
            tbPassport.Visibility = Visibility.Visible;
            cbCat.Visibility = Visibility.Visible;
            btnOk.Visibility = Visibility.Visible;
            BtnOtm.Visibility = Visibility.Visible;

            btnAdd.Visibility = Visibility.Collapsed;
            btnRedact.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)//Кнопка "Ок" при нажатии "Добавить"
        {
            if (trigger == false)
            {
                Classes.Students newuser = new Classes.Students
                {
                    fio = tbFio.Text,
                    passport = Convert.ToInt32(tbPassport.Text),
                    сategory = cbCat.Text,
                    gaiExam = false,
                    status = true,
                    theoryExam = false,
                    carTime = 0
                };

                db.GetTable<Classes.Students>().InsertOnSubmit(newuser);
                db.SubmitChanges();
                updateGrid();

                lblSearch.Visibility = Visibility.Visible;
                tbSearch.Visibility = Visibility.Visible;
                btnSearch.Visibility = Visibility.Visible;

                lblCategory.Visibility = Visibility.Collapsed;
                lblFio.Visibility = Visibility.Collapsed;
                lblPassport.Visibility = Visibility.Collapsed;
                tbFio.Visibility = Visibility.Collapsed;
                tbPassport.Visibility = Visibility.Collapsed;
                cbCat.Visibility = Visibility.Collapsed;
                btnOk.Visibility = Visibility.Collapsed;
                BtnOtm.Visibility = Visibility.Collapsed;

                btnAdd.Visibility = Visibility.Visible;
                btnRedact.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
                tbFio.Text = "";
                tbPassport.Text = "";
            }
            else
            {
                object item = dgStudents.SelectedItem;
                long vb = Convert.ToInt64((dgStudents.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                Classes.Students spc = students.FirstOrDefault(s => s.id.Equals(vb));
                spc.fio = tbFio.Text;
                spc.passport = Convert.ToInt32(tbPassport.Text);
                spc.status = true;
                spc.сategory = cbCat.Text;
                var SelectQuery =
                    from a in db.GetTable<Classes.Students>()
                    select a;
                db.SubmitChanges();
                dgStudents.ItemsSource = SelectQuery;
                updateGrid();
                MessageBox.Show("Данные изменены");
                lblSearch.Visibility = Visibility.Visible;
                tbSearch.Visibility = Visibility.Visible;
                btnSearch.Visibility = Visibility.Visible;

                lblCategory.Visibility = Visibility.Collapsed;
                lblFio.Visibility = Visibility.Collapsed;
                lblPassport.Visibility = Visibility.Collapsed;
                tbFio.Visibility = Visibility.Collapsed;
                tbPassport.Visibility = Visibility.Collapsed;
                cbCat.Visibility = Visibility.Collapsed;
                btnOk.Visibility = Visibility.Collapsed;
                BtnOtm.Visibility = Visibility.Collapsed;

                btnAdd.Visibility = Visibility.Visible;
                btnRedact.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
                tbFio.Text = "";
                tbPassport.Text = "";
            }
        }

        private void BtnOtm_Click(object sender, RoutedEventArgs e)//Кнопка "Отмена" при нажатии "Добавить"
        {
            tbFio.Text = "";
            tbPassport.Text = "";

            lblSearch.Visibility = Visibility.Visible;
            tbSearch.Visibility = Visibility.Visible;
            btnSearch.Visibility = Visibility.Visible;

            lblCategory.Visibility = Visibility.Collapsed;
            lblFio.Visibility = Visibility.Collapsed;
            lblPassport.Visibility = Visibility.Collapsed;
            tbFio.Visibility = Visibility.Collapsed;
            tbPassport.Visibility = Visibility.Collapsed;
            cbCat.Visibility = Visibility.Collapsed;
            btnOk.Visibility = Visibility.Collapsed;
            BtnOtm.Visibility = Visibility.Collapsed;

            btnAdd.Visibility = Visibility.Visible;
            btnRedact.Visibility = Visibility.Visible;
            btnDelete.Visibility = Visibility.Visible;
            trigger = false;
        }

        private void BtnRedact_Click(object sender, RoutedEventArgs e)//Кнопка "Редактировать"
        {
            lblSearch.Visibility = Visibility.Collapsed;
            tbSearch.Visibility = Visibility.Collapsed;
            btnSearch.Visibility = Visibility.Collapsed;

            lblCategory.Visibility = Visibility.Visible;
            lblFio.Visibility = Visibility.Visible;
            lblPassport.Visibility = Visibility.Visible;
            tbFio.Visibility = Visibility.Visible;
            tbPassport.Visibility = Visibility.Visible;
            cbCat.Visibility = Visibility.Visible;
            btnOk.Visibility = Visibility.Visible;
            BtnOtm.Visibility = Visibility.Visible;

            btnAdd.Visibility = Visibility.Collapsed;
            btnRedact.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;

            Table<Classes.Students> classess = db.GetTable<Classes.Students>();
            object item = dgStudents.SelectedItem;
            long vb = Convert.ToInt64((dgStudents.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            tbFio.Text = (from u in classess
                              where u.id == vb
                              select u.fio).FirstOrDefault();
            cbCat.Text = Convert.ToString((from u in classess
                                                        where u.id == vb
                                                        select u.сategory).FirstOrDefault());
            tbPassport.Text = Convert.ToString((from u in classess
                          where u.id == vb
                          select u.passport).FirstOrDefault());
            trigger = true;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)//Кнопка "Удалить"
        {
            object item = dgStudents.SelectedItem;
            long vb = Convert.ToInt64((dgStudents.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);

            Classes.Students usl = students.FirstOrDefault(uslg => uslg.id.Equals(vb));

            usl.status = false;
            db.SubmitChanges();
            updateGrid();
            MessageBox.Show("Выбранный курсант удалён");
        }
    }
}
