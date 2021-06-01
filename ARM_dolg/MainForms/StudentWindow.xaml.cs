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
            //Sum.Text += Dolgi.Items.Count * 18;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            object oMissing = System.Reflection.Missing.Value;

            Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = true;

            object oTemplate = @"D:\ARM_dolg\отчисление.dotx";
            oDoc = oWord.Documents.Add(ref oTemplate, ref oMissing, ref oMissing, ref oMissing);
            object oBookmark = @"Студент";
            oDoc.Bookmarks[ref oBookmark].Range.Text = Student.Фио;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
