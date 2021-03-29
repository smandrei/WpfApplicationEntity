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
        private bool add_edit;
        private int id;
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
        private bool IsDataCorrcet()
        {
            return (textBlockAddEditSurname.Text != string.Empty) ||
                (textBlockAddEditName.Text != string.Empty) ||
                (textBlockAddEditPatranomyc.Text != string.Empty);
        }
        private void ButtonAddEditGroup_Click(object sender, RoutedEventArgs e)
        {
            int x = 0;
            try
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext =
                            new WFAEntity.API.MyDBContext())
                {
                    if (this.IsDataCorrcet() == true)
                    {
                        WFAEntity.API.Student objectStudent = new WFAEntity.API.Student();
                        objectStudent.Surname = textBlockAddEditSurname.Text;
                        objectStudent.Name = textBlockAddEditName.Text;
                        objectStudent.Patronymic = textBlockAddEditPatranomyc.Text;
                        WFAEntity.API.Group objectGroup = new WFAEntity.API.Group(); 
                        objectGroup = (WFAEntity.API.Group)comboBoxAddEditGroup.SelectedItem;
                        objectStudent.Group = objectGroup;
                        if (this.add_edit == true)
                        {
                            objectMyDBContext.Students.Add(objectStudent);
                            objectMyDBContext.SaveChanges();
                            /*WFAEntity.API.Student objectStudent = new WFAEntity.API.Student();
                            objectStudent.Surname = textBlockAddEditSurname.Text;
                            objectStudent.Name = textBlockAddEditName.Text;
                            objectStudent.Patronymic = textBlockAddEditPatranomyc.Text;
                            WFAEntity.API.Group tg = new WFAEntity.API.Group();
                            tg.Id = 4;
                            tg.Name = "------";
                            objectStudent.Group = tg;//(WFAEntity.API.Group)comboBoxAddEditGroup.SelectedItem;
                            objectMyDBContext.Students.Add(objectStudent);
                            objectMyDBContext.SaveChanges();*/
                        }
                        else
                        {
                            WFAEntity.API.Student objectStudentFromDataBase = new WFAEntity.API.Student();
                            objectStudentFromDataBase = WFAEntity.API.DatabaseRequest.GetStudentById(objectMyDBContext, this.id);
                            objectStudent.Id = objectStudentFromDataBase.Id;
                            objectMyDBContext.Entry(objectStudentFromDataBase).CurrentValues.SetValues(objectStudent);

                            WFAEntity.API.Group objectGroupFromDataBase = WFAEntity.API.DatabaseRequest.GetGroupById(objectMyDBContext, objectStudentFromDataBase.IdGroup);
                            //WFAEntity.API.Group newGroup = new WFAEntity.API.Group();
                            objectGroup.Id = objectGroupFromDataBase.Id;
                            objectMyDBContext.Entry(objectGroupFromDataBase).CurrentValues.SetValues(objectGroup);
                            objectMyDBContext.SaveChanges();
                            /*WFAEntity.API.Student objectStudent = new WFAEntity.API.Student();
                            objectStudent = WFAEntity.API.DatabaseRequest.GetStudentById(objectMyDBContext, this.id);
                            objectStudent.Surname = textBlockAddEditSurname.Text;
                            objectStudent.Name = textBlockAddEditName.Text;
                            objectStudent.Patronymic = textBlockAddEditPatranomyc.Text;
                            objectStudent.Group = (WFAEntity.API.Group)comboBoxAddEditGroup.SelectedItem;
                            objectMyDBContext.Students.Attach(objectStudent);
                            objectMyDBContext.Entry(objectStudent).State = System.Data.Entity.EntityState.Modified;
                            objectMyDBContext.SaveChanges();*/
                        }
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
