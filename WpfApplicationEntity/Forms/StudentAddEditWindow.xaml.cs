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

namespace WpfApplicationEntity.Forms
{
    /// <summary>
    /// Логика взаимодействия для StudentAddEditWindow.xaml
    /// </summary>
    public partial class StudentAddEditWindow : Window
    {
        private readonly bool add_edit;
        private readonly int id;
        public StudentAddEditWindow()
        {
            InitializeComponent();
        }
        public StudentAddEditWindow(bool add_edit, int id = 0)
        {
            InitializeComponent();
            this.add_edit = add_edit;
            this.id = id;
            using (WFAEntity.API.MyDBContext objectMyDBContext =
                        new WFAEntity.API.MyDBContext())
            {
                if (this.add_edit == false)
                {
                    WFAEntity.API.Student objectStudent = WFAEntity.API.DatabaseRequest.GetStudentById(objectMyDBContext, this.id);
                    textBlockAddEditSurname.Text = objectStudent.Surname;
                    textBlockAddEditName.Text = objectStudent.Name;
                    textBlockAddEditPatranomyc.Text = objectStudent.Patronymic;

                    ButtonAddEditGroup.Content = "Изменить";
                }
                comboBoxAddEditGroup.ItemsSource = WFAEntity.API.DatabaseRequest.GetGroups(objectMyDBContext);
                comboBoxAddEditGroup.Text = "{Binging Name}";
            }
        }
        private bool IsDataCorrect()
        {
            return (textBlockAddEditSurname.Text != string.Empty) ||
                (textBlockAddEditName.Text != string.Empty) ||
                (textBlockAddEditPatranomyc.Text != string.Empty);
        }
        private void ButtonAddEditGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.IsDataCorrect() == true)                    
                {
                    using (WFAEntity.API.MyDBContext objectMyDBContext =
                            new WFAEntity.API.MyDBContext())
                    {
                        WFAEntity.API.Student objectStudent = new WFAEntity.API.Student(
                            textBlockAddEditName.Text,
                            textBlockAddEditSurname.Text,
                            textBlockAddEditPatranomyc.Text,
                            (WFAEntity.API.Group)comboBoxAddEditGroup.SelectedItem
                            );
                        if (this.add_edit == true)
                        {
                            objectMyDBContext.Students.Add(objectStudent);
                        }
                        else
                        {
                            objectStudent.Id = WFAEntity.API.DatabaseRequest.GetStudentById(objectMyDBContext, this.id).Id;
                            WFAEntity.API.Student objectStudentFromDataBase = new WFAEntity.API.Student();
                            objectStudentFromDataBase = WFAEntity.API.DatabaseRequest.GetStudentById(objectMyDBContext, this.id);
                            objectMyDBContext.Entry(objectStudentFromDataBase).CurrentValues.SetValues(objectStudent);
                            objectMyDBContext.SaveChanges();
                        }
                        objectMyDBContext.SaveChanges();
                        this.DialogResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА (Студент)", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
