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
    /// Логика взаимодействия для InstructorsWidow.xaml
    /// </summary>
    public partial class InstructorsWidow : Window
    {
        public InstructorsWidow()
        {
            InitializeComponent();
            updateGrid();
        }

        static DataContext db = new DataContext(Properties.Settings.Default.connectionAS);
        Table<Classes.Instructors> instructors = db.GetTable<Classes.Instructors>();
        bool trigger = false;

        private void updateGrid()// Обновление таблицы инструкторов
        {
            var stud = instructors.Where(x => x.status == true);
            dgInstructors.ItemsSource = stud;
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)//Кнопка "Поиск"
        {
            try
            {
                DataContext db = new DataContext(Properties.Settings.Default.connectionAS);
                Table<Classes.Instructors> st = db.GetTable<Classes.Instructors>();
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

                            dgInstructors.ItemsSource = find1;
                            num = 0;
                            break;
                        }
                        tbSearch.Text = "";

                    }
                    if (dgInstructors.ItemsSource == null || num != 0)
                    {
                        MessageBox.Show("Инструктор не найден");
                    }
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show($"Введите фамилию инструктора");
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)//Кнопка "Добавить"
        {
            lblSearch.Visibility = Visibility.Collapsed;
            tbSearch.Visibility = Visibility.Collapsed;
            btnSearch.Visibility = Visibility.Collapsed;

            lblCval.Visibility = Visibility.Visible;
            lblFio.Visibility = Visibility.Visible;
            lblPhone.Visibility = Visibility.Visible;
            tbFio.Visibility = Visibility.Visible;
            tbPhoneNum.Visibility = Visibility.Visible;
            cbCval.Visibility = Visibility.Visible;
            btnOk.Visibility = Visibility.Visible;
            BtnOtm.Visibility = Visibility.Visible;
            lblCarName.Visibility = Visibility.Visible;
            tbCarName.Visibility = Visibility.Visible;

            btnAdd.Visibility = Visibility.Collapsed;
            btnRedact.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)//Кнопка "Ок"
        {
            if (trigger == false)
            {
                Classes.Instructors newuser = new Classes.Instructors
                {
                    fio = tbFio.Text,
                    phoneNum = tbPhoneNum.Text,
                    kval = cbCval.Text,
                    workAge = 0,
                    status = true,
                    carNum = tbCarName.Text                   
                };

                db.GetTable<Classes.Instructors>().InsertOnSubmit(newuser);
                db.SubmitChanges();
                updateGrid();

                lblSearch.Visibility = Visibility.Visible;
                tbSearch.Visibility = Visibility.Visible;
                btnSearch.Visibility = Visibility.Visible;

                lblCval.Visibility = Visibility.Collapsed;
                lblFio.Visibility = Visibility.Collapsed;
                lblPhone.Visibility = Visibility.Collapsed;
                tbFio.Visibility = Visibility.Collapsed;
                tbPhoneNum.Visibility = Visibility.Collapsed;
                cbCval.Visibility = Visibility.Collapsed;
                btnOk.Visibility = Visibility.Collapsed;
                BtnOtm.Visibility = Visibility.Collapsed;
                lblCarName.Visibility = Visibility.Collapsed;
                tbCarName.Visibility = Visibility.Collapsed;

                btnAdd.Visibility = Visibility.Visible;
                btnRedact.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
                tbCarName.Text = "";
                tbFio.Text = "";
                tbPhoneNum.Text = "";
            }
            else
            {
                object item = dgInstructors.SelectedItem;
                long vb = Convert.ToInt64((dgInstructors.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                Classes.Instructors spc = instructors.FirstOrDefault(s => s.id.Equals(vb));

                spc.fio = tbFio.Text;
                spc.phoneNum = tbPhoneNum.Text;
                spc.status = true;
                spc.kval = cbCval.Text;
                spc.carNum = tbCarName.Text;

                var SelectQuery =
                    from a in db.GetTable<Classes.Students>()
                    select a;

                db.SubmitChanges();
                dgInstructors.ItemsSource = SelectQuery;
                updateGrid();

                MessageBox.Show("Данные изменены");

                lblSearch.Visibility = Visibility.Visible;
                tbSearch.Visibility = Visibility.Visible;
                btnSearch.Visibility = Visibility.Visible;

                lblCval.Visibility = Visibility.Collapsed;
                lblFio.Visibility = Visibility.Collapsed;
                lblPhone.Visibility = Visibility.Collapsed;
                tbFio.Visibility = Visibility.Collapsed;
                tbPhoneNum.Visibility = Visibility.Collapsed;
                cbCval.Visibility = Visibility.Collapsed;
                btnOk.Visibility = Visibility.Collapsed;
                BtnOtm.Visibility = Visibility.Collapsed;
                lblCarName.Visibility = Visibility.Collapsed;
                tbCarName.Visibility = Visibility.Collapsed;

                btnAdd.Visibility = Visibility.Visible;
                btnRedact.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
                tbCarName.Text = "";
                tbFio.Text = "";
                tbPhoneNum.Text = "";
            }
        }

        private void BtnOtm_Click(object sender, RoutedEventArgs e)//Кнопка "Отмена"
        {
            lblSearch.Visibility = Visibility.Visible;
            tbSearch.Visibility = Visibility.Visible;
            btnSearch.Visibility = Visibility.Visible;

            lblCval.Visibility = Visibility.Collapsed;
            lblFio.Visibility = Visibility.Collapsed;
            lblPhone.Visibility = Visibility.Collapsed;
            tbFio.Visibility = Visibility.Collapsed;
            tbPhoneNum.Visibility = Visibility.Collapsed;
            cbCval.Visibility = Visibility.Collapsed;
            btnOk.Visibility = Visibility.Collapsed;
            BtnOtm.Visibility = Visibility.Collapsed;
            lblCarName.Visibility = Visibility.Collapsed;
            tbCarName.Visibility = Visibility.Collapsed;

            btnAdd.Visibility = Visibility.Visible;
            btnRedact.Visibility = Visibility.Visible;
            btnDelete.Visibility = Visibility.Visible;
            tbCarName.Text = "";
            tbFio.Text = "";
            tbPhoneNum.Text = "";
            trigger = false;
        }

        private void BtnRedact_Click(object sender, RoutedEventArgs e)//Кнопка "Редактировать"
        {
            lblSearch.Visibility = Visibility.Collapsed;
            tbSearch.Visibility = Visibility.Collapsed;
            btnSearch.Visibility = Visibility.Collapsed;

            lblCval.Visibility = Visibility.Visible;
            lblFio.Visibility = Visibility.Visible;
            lblPhone.Visibility = Visibility.Visible;
            tbFio.Visibility = Visibility.Visible;
            tbPhoneNum.Visibility = Visibility.Visible;
            cbCval.Visibility = Visibility.Visible;
            btnOk.Visibility = Visibility.Visible;
            BtnOtm.Visibility = Visibility.Visible;
            lblCarName.Visibility = Visibility.Visible;
            tbCarName.Visibility = Visibility.Visible;

            btnAdd.Visibility = Visibility.Collapsed;
            btnRedact.Visibility = Visibility.Collapsed;
            btnDelete.Visibility = Visibility.Collapsed;

            Table<Classes.Instructors> classess = db.GetTable<Classes.Instructors>();
            object item = dgInstructors.SelectedItem;
            long vb = Convert.ToInt64((dgInstructors.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            tbFio.Text = (from u in classess
                          where u.id == vb
                          select u.fio).FirstOrDefault();
            cbCval.Text = Convert.ToString((from u in classess
                                           where u.id == vb
                                           select u.kval).FirstOrDefault());
            tbPhoneNum.Text = Convert.ToString((from u in classess
                                                where u.id == vb
                                                select u.phoneNum).FirstOrDefault());
            tbCarName.Text = Convert.ToString((from u in classess
                                                where u.id == vb
                                                select u.carNum).FirstOrDefault());
            trigger = true;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)//Кнопка "Удалить"
        {
            object item = dgInstructors.SelectedItem;
            long vb = Convert.ToInt64((dgInstructors.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);

            Classes.Instructors usl = instructors.FirstOrDefault(uslg => uslg.id.Equals(vb));

            usl.status = false;
            db.SubmitChanges();
            updateGrid();
            MessageBox.Show("Выбранный инструктор удалён");
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)//Выход
        {
            ViewWindow viewWindow = new ViewWindow();
            viewWindow.Show();
            this.Close();
        }
    }
}
