using System;
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
    /// Логика взаимодействия для GroupSubjWindow.xaml
    /// </summary>
    public partial class GroupSubjWindow : Window
    {
        public GroupSubjWindow(string title)
        {
            InitializeComponent();
            Title = title;
            switch (Title)
            {
                case "Добавить учебную группу":
                    Name.Text = "Номер";
                    break;
                case "Добавить учебный предмет":
                    Name.Text = "Название";
                    break;
                default:
                    throw new Exception("Error in name");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (var dc = new DolgContext())
            {
                switch (Title)
                {
                    case "Добавить учебную группу":
                        dc.StudGroups.Add(new StudGroup { Номер = Input.Text});
                        break;
                    case "Добавить учебный предмет":
                        dc.StudSubjects.Add(new StudSubject { Название = Input.Text });
                        break;
                    default:
                        throw new Exception("Error in name");
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
            this.Close();
        }
    }
}
