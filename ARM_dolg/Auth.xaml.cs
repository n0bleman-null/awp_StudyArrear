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

namespace ARM_dolg
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public enum Role
    {
        Администратор,
        Преподаватель, 
        Студент
    }
    public partial class AuthWindow : Window
    {
        Role Role = Role.Администратор;
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Group_DropDownOpened(object sender, EventArgs e)
        {
            using (var dc = new DolgContext())
            {
                Group.ItemsSource = dc.StudGroups.ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Teacher teacher = null;
            using (var dc = new DolgContext())
            {
                switch (Role)
                {
                    case Role.Администратор:
                        var admin = dc.Teachers.FirstOrDefault(s => s.Фио == Login.Text && s.Пароль == Password.Text && s.Администратор);
                        if (admin is null)
                        {
                            MessageBox.Show("Ошибка доступа");
                            return;
                        }
                        break;
                    case Role.Преподаватель:
                        teacher = dc.Teachers.FirstOrDefault(s => s.Фио == Login.Text && s.Пароль == Password.Text);
                        if (teacher is null)
                        {
                            MessageBox.Show("Ошибка доступа");
                            return;
                        }
                        break;
                    case Role.Студент:
                        var student = dc.Students.FirstOrDefault(s => s.Фио == Login.Text && s.Пароль == Password.Text && s.НомерГруппы == (Group.SelectedItem as StudGroup).Id);
                        if (student is null)
                        {
                            MessageBox.Show("Ошибка доступа");
                            return;
                        }
                        break;
                    default:
                        break;
                }
            }
            Window window = Role switch
            {
                Role.Администратор => new AdminWindow(),
                Role.Преподаватель => new TeacherWindow(teacher),
                Role.Студент => new StudentWindow(),
                _ => throw new NotImplementedException()
            };
            window.Show();
            this.Close();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Role = (Role)Enum.Parse(Role.GetType(), sender.GetType().GetProperty("Content").GetValue(sender).ToString());

            if (Role is Role.Студент)
                Group.IsEnabled = true;
            else            
                Group.IsEnabled = false;            
        }
    }
}
