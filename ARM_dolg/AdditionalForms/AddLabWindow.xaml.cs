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
using Microsoft.EntityFrameworkCore;

namespace ARM_dolg.AdditionalForms
{
    /// <summary>
    /// Логика взаимодействия для AddLabWindow.xaml
    /// </summary>
    public partial class AddLabWindow : Window
    {
        public AddLabWindow()
        {
            InitializeComponent();
            LabData.SelectedDate = DateTime.Today;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dc = new DolgContext())
            {
                dc.Labs.Add(new Lab { ГруппаПреподаватель = (Lab.SelectedItem as GroupTeacher).Id, ДатаЗанятия = LabData.SelectedDate.Value });
                try
                {
                    dc.SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show("Проверьте корректность вводыимых значений (скорее всего значение в базе повторяется, попробуйте другое)", "Ошибка. Откат изменений!");
                }
            }
        }

        private void Lab_DropDownOpened(object sender, EventArgs e)
        {
            using (var dc = new DolgContext())
            {
                dc.GroupTeachers.Include(a => a.НомерГруппыNavigation).Load();
                dc.GroupTeachers.Include(a => a.ПреподавательNavigation).Load();
                dc.GroupTeachers.Include(a => a.УчебныйПредметNavigation).Load();
                Lab.ItemsSource = dc.GroupTeachers.ToList();
            }
        }
    }
}
