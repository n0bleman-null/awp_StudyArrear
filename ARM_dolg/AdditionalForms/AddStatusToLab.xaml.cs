using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для AddStatusToLab.xaml
    /// </summary>
    public enum Status
    {
        Зачет,
        Незачет,
        Отсутствует
    }
    public partial class AddStatusToLab : Window
    {
        Status Status = Status.Незачет;
        public AddStatusToLab()
        {
            InitializeComponent();
        }

        private void Lab_DropDownOpened(object sender, EventArgs e)
        {
            using (var dc = new DolgContext())
            {
                dc.GroupTeachers.Include(s => s.НомерГруппыNavigation).Load();
                dc.GroupTeachers.Include(s => s.ПреподавательNavigation).Load();
                dc.GroupTeachers.Include(s => s.УчебныйПредметNavigation).Load();
                dc.Labs.Include(s => s.ГруппаПреподавательNavigation).Load();
                Lab.ItemsSource = dc.Labs.ToList();
            }
        }

        private void Student_DropDownOpened(object sender, EventArgs e)
        {
            using (var dc = new DolgContext())
            {               
                dc.Students.Include(s => s.НомерГруппыNavigation).Load();
                Student.ItemsSource = dc.Students.ToList();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dc = new DolgContext())
            {
                dc.StudentLabs.Add(new StudentLab
                {
                    ПрактическияРабота = (Lab.SelectedItem as Lab).Id,
                    Студент = (Student.SelectedItem as Student).Id,
                    Статус = Status switch
                    {
                        Status.Зачет => "Зач",
                        Status.Незачет => "Незач",
                        Status.Отсутствует => "Н",
                        _ => throw new NotImplementedException()
                    }
                });
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

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Status = (Status)Enum.Parse(Status.GetType(), sender.GetType().GetProperty("Content").GetValue(sender).ToString());
        }
    }
}
