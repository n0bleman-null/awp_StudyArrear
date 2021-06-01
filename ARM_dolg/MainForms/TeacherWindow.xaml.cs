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
using ARM_dolg.AdditionalForms;

namespace ARM_dolg
{
    /// <summary>
    /// Логика взаимодействия для TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        Teacher Teacher { get; set; }
        Status Status = Status.Незачет;
        public TeacherWindow()
        {
            InitializeComponent();                        
        }

        public TeacherWindow(Teacher teacher)
        {
            InitializeComponent();
            Teacher = teacher;
            Title = teacher.Фио;
            Data.SelectedDate = DateTime.Today;
            UpdateDolgi();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (var dc = new DolgContext())
            {
                dc.Labs.Add(new Lab { ГруппаПреподаватель = (Para.SelectedItem as GroupTeacher).Id, ДатаЗанятия = Data.SelectedDate.Value });
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (var dc = new DolgContext())
            {
                if (Lab.SelectedItem is null)
                    return;
                var lab = dc.StudentLabs.FirstOrDefault(s => s.ПрактическияРабота == (Lab.SelectedItem as Lab).Id && s.Студент == (Stud.SelectedItem as Student).Id);
                if (lab is null)
                dc.StudentLabs.Add(new StudentLab
                {
                    ПрактическияРабота = (Lab.SelectedItem as Lab).Id,
                    Студент = (Stud.SelectedItem as Student).Id,
                    Статус = Status switch
                    {
                        Status.Зачет => "Зач",
                        Status.Незачет => "Незач",
                        Status.Отсутствует => "Н",
                        _ => throw new NotImplementedException()
                    }
                });
                else
                {
                    lab.Статус = Status switch
                    {
                        Status.Зачет => "Зач",
                        Status.Незачет => "Незач",
                        Status.Отсутствует => "Н",
                        _ => throw new NotImplementedException()
                    };
                    dc.StudentLabs.Update(lab);
                }
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

        private void Para_DropDownOpened(object sender, EventArgs e)
        {
            using (var dc = new DolgContext())
            {
                dc.GroupTeachers.Include(a => a.НомерГруппыNavigation).Load();
                dc.GroupTeachers.Include(a => a.ПреподавательNavigation).Load();
                dc.GroupTeachers.Include(a => a.УчебныйПредметNavigation).Load();
                Para.ItemsSource = dc.GroupTeachers.Where(s => s.Преподаватель == Teacher.Id).ToList();
            }
        }

        private void Lab_DropDownOpened(object sender, EventArgs e)
        {
            Stud.SelectedItem = null;
            using (var dc = new DolgContext())
            {
                dc.GroupTeachers.Include(s => s.НомерГруппыNavigation).Load();
                dc.GroupTeachers.Include(s => s.ПреподавательNavigation).Load();
                dc.GroupTeachers.Include(s => s.УчебныйПредметNavigation).Load();
                dc.Labs.Include(s => s.ГруппаПреподавательNavigation).Load(); 
                var lst = dc.Labs.Where(s => s.ГруппаПреподавательNavigation.Преподаватель == Teacher.Id).ToList();
                if (!(Group.SelectedItem is null))
                    lst = lst.Where(s => s.ГруппаПреподавательNavigation.НомерГруппы == (Group.SelectedItem as StudGroup).Id).ToList();
                if (!(Subj.SelectedItem is null))
                    lst = lst.Where(s => s.ГруппаПреподавательNavigation.УчебныйПредмет == (Subj.SelectedItem as StudSubject).Id).ToList();
                Lab.ItemsSource = lst;
            }
        }

        private void Stud_DropDownOpened(object sender, EventArgs e)
        {
            using (var dc = new DolgContext())
            {
                dc.Students.Include(s => s.НомерГруппыNavigation).Load();
                if (Lab.SelectedItem is null)
                {
                    MessageBox.Show("Выберите сначала лабараторную работу", "Внимание");
                    return;
                }
                Stud.ItemsSource = dc.Students.Where(s => (Lab.SelectedItem as Lab).ГруппаПреподавательNavigation.НомерГруппы == s.НомерГруппы)?.ToList();
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Status = (Status)Enum.Parse(Status.GetType(), sender.GetType().GetProperty("Content").GetValue(sender).ToString());
        }

        private void Group_DropDownOpened(object sender, EventArgs e)
        {
            using (var dc = new DolgContext())
            {
                if (Subj.SelectedItem is null)
                    Group.ItemsSource = dc.StudGroups.Where(s => s.GroupTeachers.Any(x => x.Преподаватель == Teacher.Id)).ToList();
                else
                    Group.ItemsSource = dc.StudGroups.Where(s => s.GroupTeachers.Any(x => x.Преподаватель == Teacher.Id)).Where(s => s.GroupTeachers.Any(x => x.УчебныйПредмет == (Subj.SelectedItem as StudSubject).Id)).ToList();
            }
        }

        private void Subj_DropDownOpened(object sender, EventArgs e)
        {
            using (var dc = new DolgContext())
            {
                if (Group.SelectedItem is null)
                    Subj.ItemsSource = dc.StudSubjects.Where(s => s.GroupTeachers.Any(x => x.Преподаватель == Teacher.Id)).ToList();
                else
                    Subj.ItemsSource = dc.StudSubjects.Where(s => s.GroupTeachers.Any(x => x.Преподаватель == Teacher.Id)).Where(s => s.GroupTeachers.Any(x => x.НомерГруппы == (Group.SelectedItem as StudGroup).Id)).ToList();
            }
        }

        private void DolgiGroup_DropDownOpened(object sender, EventArgs e)
        {
            using (var dc = new DolgContext())
            {
                if (DolgiSubject.SelectedItem is null)
                    DolgiGroup.ItemsSource = dc.StudGroups.ToList();
                else
                    DolgiGroup.ItemsSource = dc.StudGroups.Where(s => s.GroupTeachers.Any(x => x.УчебныйПредмет == (DolgiSubject.SelectedItem as StudSubject).Id)).ToList();
            }
        }

        private void DolgiSubject_DropDownOpened(object sender, EventArgs e)
        {
            using (var dc = new DolgContext())
            {
                if (DolgiGroup.SelectedItem is null)
                    DolgiSubject.ItemsSource = dc.StudSubjects.ToList();
                else
                    DolgiSubject.ItemsSource = dc.StudSubjects.Where(s => s.GroupTeachers.Any(x => x.НомерГруппы == (DolgiGroup.SelectedItem as StudGroup).Id)).ToList();
            }
        }

        private void UpdateDolgi()
        {
            using (var dc = new DolgContext())
            {
                var grouping = DolgiGroup.SelectedItem as StudGroup;
                var subj = DolgiSubject.SelectedItem as StudSubject;
                Dolgi.ItemsSource = (from studentLab in dc.StudentLabs
                                     join student in dc.Students on studentLab.Студент equals student.Id
                                     where studentLab.Статус != "Зач"
                                        && (grouping == null ? true : student.НомерГруппы == grouping.Id)
                                        && (subj == null ? true : studentLab.ПрактическияРаботаNavigation.ГруппаПреподавательNavigation.УчебныйПредмет == subj.Id)
                                     group student by student.Фио into studgroup
                                     select new
                                     {
                                         Студент = studgroup.Key,
                                         Количество = studgroup.Count()
                                     }).OrderBy(s => s.Студент).ToList();
            }
        }

        private void DolgiGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateDolgi();
        }
    }
}
