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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplicationEntity
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private enum SELECTED_TAB { GROUP, STUDENT }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext = new WFAEntity.API.MyDBContext())
                {
                    if (objectMyDBContext.Database.Exists() == false)
                    {
                        objectMyDBContext.Database.Create();
                        WFAEntity.API.User objectUser = new WFAEntity.API.User();
                        objectUser.Name = "user name";
                        objectUser.Login = "user";
                        objectUser.Password = "1111";
                        objectMyDBContext.Users.Add(objectUser);
                        objectMyDBContext.SaveChanges();
                        WFAEntity.API.DatabaseRequest.CreateDefaultDataBase();
                    }
                }
                //WFAEntity.API.DatabaseRequest.CreateDefaultDataBase();
                //WFAEntity.API.DatabaseRequest.ChangeDefaultDataBase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.ShowAll(SELECTED_TAB.GROUP);
        }
        #region Группа //------------------------------------------------------------
        private void addGroupButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.GroupWindow g = new Forms.GroupWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll(SELECTED_TAB.GROUP);
        }
        private void editGroupButton_Click(object sender, RoutedEventArgs e)
        {
            if (gropiesGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < gropiesGrid.SelectedItems.Count; i++)
                {
                    WFAEntity.API.Group objectGroup = gropiesGrid.SelectedItems[i] as WFAEntity.API.Group;
                    if (objectGroup != null)
                    //if (gropiesGrid.SelectedItems[i] is WFAEntity.API.Group students)
                    {
                        Forms.GroupWindow g = new Forms.GroupWindow(false, objectGroup.Id);
                        if (g.ShowDialog() == true)
                            this.ShowAll(SELECTED_TAB.GROUP);
                    }
                }
            }
            else
                MessageBox.Show("Выберите строку");
        }
        private void deleteGroupButton_Click(object sender, RoutedEventArgs e)
        {
            if (gropiesGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < gropiesGrid.SelectedItems.Count; i++)
                {
                    WFAEntity.API.Group objectGroup = gropiesGrid.SelectedItems[i] as WFAEntity.API.Group;
                    if (objectGroup != null)
                        try
                        {
                            using (WFAEntity.API.MyDBContext objectMyDBContext =
                                new WFAEntity.API.MyDBContext())
                            {
                                WFAEntity.API.Group group = WFAEntity.API.DatabaseRequest.GetGroupById(objectMyDBContext, objectGroup.Id);
                                objectMyDBContext.Groups.Attach(group);
                                objectMyDBContext.Groups.Remove(group);
                                objectMyDBContext.SaveChanges();
                            }
                            this.ShowAll(SELECTED_TAB.GROUP);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                }
            }
        }
        #endregion Группа
        #region Студент //------------------------------------------------------------
        private void addStudentButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.StudentAddEditWindow g = new Forms.StudentAddEditWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll(SELECTED_TAB.STUDENT);
        }
        private void editStudentButton_Click(object sender, RoutedEventArgs e)
        {
            if (studentsGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < studentsGrid.SelectedItems.Count; i++)
                {
                    WFAEntity.API.NewStudent objectStudent = studentsGrid.SelectedItems[i] as WFAEntity.API.NewStudent;
                    if (objectStudent != null)
                    {
                        Forms.StudentAddEditWindow g = new Forms.StudentAddEditWindow(false, objectStudent.Id);
                        if (g.ShowDialog() == true)
                            this.ShowAll(SELECTED_TAB.STUDENT);
                    }
                }
            }
            else
                MessageBox.Show("Выберите строку");
        }
        private void deleteStudentButton_Click(object sender, RoutedEventArgs e)
        {
            if (studentsGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < studentsGrid.SelectedItems.Count; i++)
                {
                    WFAEntity.API.Student objectStudent = studentsGrid.SelectedItems[i] as WFAEntity.API.Student;
                    if (objectStudent != null)
                        try
                        {
                            using (WFAEntity.API.MyDBContext objectMyDBContext =
                                new WFAEntity.API.MyDBContext())
                            {
                                WFAEntity.API.Student student = WFAEntity.API.DatabaseRequest.GetStudentById(objectMyDBContext, objectStudent.Id);
                                objectMyDBContext.Students.Attach(student);
                                objectMyDBContext.Students.Remove(student);
                                objectMyDBContext.SaveChanges();
                            }
                            this.ShowAll(SELECTED_TAB.STUDENT);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                }
            }
        }
        #endregion Студент
        #region Window //------------------------------------------------------------
        private void ShowAll(SELECTED_TAB tab)
        {
            try
            {
                using (WFAEntity.API.MyDBContext objectMyDBContext =
                    new WFAEntity.API.MyDBContext())
                {
                    switch (tab)
                    {
                        case SELECTED_TAB.GROUP:
                            gropiesGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetGroups(objectMyDBContext);
                            break;
                        case SELECTED_TAB.STUDENT:
                            studentsGrid.ItemsSource = WFAEntity.API.DatabaseRequest.GetStudentsWithGroups(objectMyDBContext);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОШИБКА ShowAll", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            GC.Collect();
        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TabItem selectedTab = e.AddedItems[0] as TabItem;
                switch (selectedTab.Name)
                {
                    case "groupiesTab":
                        this.ShowAll(SELECTED_TAB.GROUP);
                        break;
                    case "studentiesTab":
                        this.ShowAll(SELECTED_TAB.STUDENT);
                        break;
                    default:
                        return;
                }
            }
            catch//(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        #endregion Window //------------------------------------------------------------
    }
}
