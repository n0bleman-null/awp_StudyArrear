using ARM_dolg.AdditionalForms;
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

namespace ARM_dolg
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new RegisterWindow();
            window.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var window = new GroupSubjWindow("Добавить учебную группу");
            window.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var window = new GroupSubjWindow("Добавить учебный предмет");
            window.Show();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var window = new GroupToTeacherWindow();
            window.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var window = new AddLabWindow();
            window.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var window = new AddStatusToLab();
            window.Show();
        }
    }
}
