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
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.EntityFrameworkCore;

namespace ARM_dolg
{
    /// <summary>
    /// Логика взаимодействия для StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        Student Student { get; set; }
        public StudentWindow()
        {
            InitializeComponent();
        }

        public StudentWindow(Student student)
        {
            InitializeComponent();
            Student = student;
            Title = student.Фио;            
            using (var dc = new DolgContext())
            {
                Dolgi.ItemsSource = (from studentLab in dc.StudentLabs
                                     join lab in dc.Labs on studentLab.ПрактическияРабота equals lab.Id
                                     join groupTeacher in dc.GroupTeachers on lab.ГруппаПреподаватель equals groupTeacher.Id
                                     join subj in dc.StudSubjects on groupTeacher.УчебныйПредмет equals subj.Id
                                     where studentLab.Студент == Student.Id && studentLab.Статус != "Зач"
                                     select new
                                     {
                                         ДатаЗанятия = lab.ДатаЗанятия,
                                         Предмет = subj.Название,
                                         Статус = studentLab.Статус
                                     }).ToList();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Dolgi.SelectedItem is null || Dolgi.SelectedItems.Count != 1)
            {
                MessageBox.Show("Для печати заявление выберите одно занятие для отработки");
                return;
            }
            object oMissing = System.Reflection.Missing.Value;

            Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = true;
            
            using (var dc = new DolgContext())
            {
                var lab = Dolgi.SelectedItem;
                object oTemplate = @"D:\ARM_dolg\Отработка.dotx";
                oDoc = oWord.Documents.Add(ref oTemplate, ref oMissing, ref oMissing, ref oMissing);
                object oName = @"Фио";
                oDoc.Bookmarks[ref oName].Range.Text = Student.Фио;
                object oGroup = @"Группа";
                oDoc.Bookmarks[ref oGroup].Range.Text = dc.StudGroups.Single(g => g.Id == Student.НомерГруппы).Номер;
                object oToday = @"Сегодня";
                oDoc.Bookmarks[ref oToday].Range.Text = DateTime.Today.ToShortDateString();
                object oDate = @"Дата";
                oDoc.Bookmarks[ref oDate].Range.Text = ((DateTime)lab.GetType().GetProperty("ДатаЗанятия").GetValue(lab)).ToShortDateString();
                object oSubj = @"Предмет";
                oDoc.Bookmarks[ref oSubj].Range.Text = (string)lab.GetType().GetProperty("Предмет").GetValue(lab);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            object oMissing = System.Reflection.Missing.Value;

            Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = true;
            using (var dc = new DolgContext())
            {
                object oTemplate = @"D:\ARM_dolg\Отчисление.dotx";
                oDoc = oWord.Documents.Add(ref oTemplate, ref oMissing, ref oMissing, ref oMissing);
                object oName = @"Фио";
                oDoc.Bookmarks[ref oName].Range.Text = Student.Фио;
                object oGroup = @"Группа";
                oDoc.Bookmarks[ref oGroup].Range.Text = dc.StudGroups.Single(g => g.Id == Student.НомерГруппы).Номер;
                object oToday = @"Сегодня";
                oDoc.Bookmarks[ref oToday].Range.Text = DateTime.Today.ToShortDateString();
            }
        }
    }
}
