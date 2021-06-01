using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ARM_dolg.AdditionalForms
{
    /// <summary>
    /// Логика взаимодействия для GroupToTeacherWindow.xaml
    /// </summary>
    public partial class GroupToTeacherWindow : Window
    {
        public GroupToTeacherWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dc = new DolgContext())
            {
                dc.GroupTeachers.Add(new GroupTeacher { Преподаватель = (Teacher.SelectedItem as Teacher).Id, НомерГруппы = (Group.SelectedItem as StudGroup).Id, УчебныйПредмет = (Subj.SelectedItem as StudSubject).Id });
                try
                {
                    dc.SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show("Проверьте корректность вводыимых значений (скорее всего значение в базе повторяется, попробуйте другое)", "Ошибка. Откат изменений!");
                }
            }

            this.Close();
        }

        private void Group_DropDownOpened(object sender, EventArgs e)
        {
            using (var dc = new DolgContext())
            {
                Group.ItemsSource = dc.StudGroups.ToList();
            }
        }

        private void Teacher_DropDownOpened(object sender, EventArgs e)
        {
            using (var dc = new DolgContext())
            {
                Teacher.ItemsSource = dc.Teachers.ToList();
            }
        }

        private void Subj_DropDownOpened(object sender, EventArgs e)
        {
            using (var dc = new DolgContext())
            {
                Subj.ItemsSource = dc.StudSubjects.ToList();
            }
        }
    }
}
