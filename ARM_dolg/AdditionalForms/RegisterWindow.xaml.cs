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
    public partial class RegisterWindow : Window
    {
        Role Role = Role.Администратор;
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dc = new DolgContext())
            {
                switch (Role)
                {
                    case Role.Администратор:
                        dc.Teachers.Add(new Teacher { Фио = Login.Text, Пароль = Password.Text, Администратор = true });
                        break;
                    case Role.Преподаватель:
                        dc.Teachers.Add(new Teacher { Фио = Login.Text, Пароль = Password.Text, Администратор = false });
                        break;
                    case Role.Студент:
                        dc.Students.Add(new Student { Фио = Login.Text, Пароль = Password.Text, НомерГруппы = (Group.SelectedItem as StudGroup).Id });
                        break;
                    default:
                        break;
                }
                try
                {
                    dc.SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show("Проверьте корректность вводыимых значений (скорее всего значение в базе повторяется, попробуйте другое).", "Ошибка. Откат изменений!");
                }

            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Role = (Role)Enum.Parse(Role.GetType(), sender.GetType().GetProperty("Content").GetValue(sender).ToString());

            if (Role is Role.Студент)
                Group.IsEnabled = true;
            else            
                Group.IsEnabled = false;
            
        }

        private void Group_DropDownOpened(object sender, EventArgs e)
        {
            using (var dc = new DolgContext())
            {
                Group.ItemsSource = dc.StudGroups.ToList();
            }
        }
    }
}
