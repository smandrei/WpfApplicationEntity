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
                int c = 0;
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
                    WFAEntity.API.Group phone = gropiesGrid.SelectedItems[i] as WFAEntity.API.Group;
                    if (phone != null)
                    //if (gropiesGrid.SelectedItems[i] is WFAEntity.API.Group group)
                    {
                        Forms.GroupWindow g = new Forms.GroupWindow(false, phone.Id);
                        if (g.ShowDialog() == true)
                            this.ShowAll(SELECTED_TAB.GROUP);
                    }
                }
            }
            else
                MessageBox.Show("Выберите строку");
        }
        #endregion Группа
        #region Студент //------------------------------------------------------------
        private void addStudentButton_Click(object sender, RoutedEventArgs e)
        {
            Forms.StudentAddEditWindow g = new Forms.StudentAddEditWindow(true);
            if (g.ShowDialog() == true)
                this.ShowAll(SELECTED_TAB.STUDENT);
        }

        private void deleteStudentButton_Click(object sender, RoutedEventArgs e)
        {
            //int x = 0;
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
        #endregion Студент
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
    }
}
