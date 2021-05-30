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
            Window window = Role switch
            {
                Role.Администратор => new AdminWindow(),
                Role.Преподаватель => new TeacherWindow(),
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
